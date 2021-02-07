using TaiheSystem.CBE.Api.Hostd.Authorization;
using TaiheSystem.CBE.Api.Model;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Common.Helpers;
using TaiheSystem.CBE.Api.Hostd.Extensions;
using Microsoft.Extensions.Logging;
using TaiheSystem.CBE.Api.Interfaces;
using System.Collections.Generic;

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Sys
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : BaseController
    {
        /// <summary>
        /// 日志管理接口
        /// </summary>
        private readonly ILogger<FileController> _logger;
        /// <summary>
        /// 会话管理接口
        /// </summary>
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// 文件接口
        /// </summary>
        private readonly IgnlFileService _gnlfileService;

        /// <summary>
        /// 数据关系接口
        /// </summary>
        private readonly ISysDataRelationService _dataRelationService;

        public FileController(ILogger<FileController> logger, TokenManager tokenManager, IgnlFileService gnlfileService, ISysDataRelationService dataRelationService)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _gnlfileService = gnlfileService;
            _dataRelationService = dataRelationService;
        }

        // GET: api/Download
        /// <summary>
        /// 文件获取
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="FileID">文件id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorization]
        public FileStreamResult DownLoad([FromServices] IWebHostEnvironment environment,string FileID)
        {
            //try
            //{
            var userSession = _tokenManager.GetSessionInfo();

            if (string.IsNullOrEmpty(FileID))
            {
                throw new Exception("文件ID不允许为空");
            }
            gnl_File file = _gnlfileService.GetId(FileID);
            if(file == null)
            {
                throw new Exception("文件不存在");
            }
            string filepath = file.AbsoluteFilePath;
            var stream = System.IO.File.OpenRead(filepath);
            string fileExt = file.FileExt;  // 这里可以写一个获取文件扩展名的方法，获取扩展名
                                        //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            var fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);

            _gnlfileService.Update(f => f.FileID.ToString() == FileID, f => new gnl_File { DownloadCount = file.DownloadCount + 1 });

            return File(stream, memi, fileName);
            //}
            //catch(Exception ex)
            //{
                //return toResponse(StatusCodeType.Error, ex.Message);
            //}
        }

        /// <summary>
        /// 上传图片,多文件，可以使用 postman 测试，
        /// 如果是单文件，可以 参数写 IFormFile file1
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult UploadFile([FromServices] IWebHostEnvironment environment)
        {
            var userSession = _tokenManager.GetSessionInfo();

            _gnlfileService.BeginTran();
            IFormFileCollection files = null;

            // 获取提交的文件
            files = Request.Form.Files;
            // 获取附带的数据
            var FileType = Request.Form["FileType"].ObjToString();
            var FileGroup = Request.Form["FileGroup"].ObjToString();
            if(FileType == "")
            {
                FileType = "Attach";
            }

            if (files == null || !files.Any()) { return toResponse(StatusCodeType.Error, "请选择上传的文件");}

            try
            {
                string filerootpath = string.IsNullOrEmpty(environment.WebRootPath) ? environment.ContentRootPath : environment.WebRootPath;
                List<gnl_File> filelist = new List<gnl_File>();
                foreach (var file in files)
                {

                    var gnl_File = FileHelper.CreateFile(filerootpath, file, file.FileName, FileType);
                    if (gnl_File == null)
                    {
                        return toResponse(StatusCodeType.Error, "文件上传失败");
                    }
                    gnl_File.FileGroup = FileGroup == "" ? Guid.NewGuid().ToString().ToUpper() : FileGroup.ToUpper();
                    gnl_File.CreateUserID = userSession.UserID;
                    gnl_File.CreateUserName = userSession.UserName;

                    filelist.Add(gnl_File);
                }

                if (_gnlfileService.Add(filelist) < filelist.Count())
                {
                    return toResponse(StatusCodeType.Error, "插入文件数据失败");
                }
                _gnlfileService.CommitTran();

                return toResponse(filelist);
            }
            catch (Exception ex)
            {
                _gnlfileService.RollbackTran();
                return toResponse(StatusCodeType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 异步上传图片,多文件，可以使用 postman 测试，
        /// 如果是单文件，可以 参数写 IFormFile file1
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorization]
        public IActionResult UploadAsync([FromServices] IWebHostEnvironment environment)
        {
            //获取当前登录用户信息
            var userSession = _tokenManager.GetSessionInfo();

            _gnlfileService.BeginTran();
            IFormFileCollection files = null;

            // 获取提交的文件
            files = Request.Form.Files;
            // 获取附带的数据
            var FileType = Request.Form["FileType"].ObjToString();
            var FileGroup = Request.Form["FileGroup"].ObjToString();
            if (FileType == "")
            {
                FileType = "Attach";
            }

            if (files == null || !files.Any()) { toResponse(StatusCodeType.Error, "请选择上传的文件"); }

            string filerootpath = string.IsNullOrEmpty(environment.WebRootPath) ? environment.ContentRootPath : environment.WebRootPath;
            try
            {
                List<gnl_File> filelist = new List<gnl_File>();
                foreach (var file in files)
                {

                    var gnl_File = FileHelper.CreateFileAsync(filerootpath, file, file.FileName, FileType);
                    if (gnl_File == null)
                    {
                        return toResponse(StatusCodeType.Error, "文件上传失败");
                    }
                    gnl_File.Result.FileGroup = FileGroup == "" ? Guid.NewGuid().ToString().ToUpper() : FileGroup.ToUpper();
                    gnl_File.Result.CreateUserID = userSession.UserID;
                    gnl_File.Result.CreateUserName = userSession.UserName;

                    filelist.Add(gnl_File.Result);
                }

                if (_gnlfileService.Add(filelist) < filelist.Count())
                {
                    return toResponse(StatusCodeType.Error, "插入文件数据失败");
                }
                _gnlfileService.CommitTran();

                return toResponse(filelist);
            }
            catch (Exception ex)
            {
                _gnlfileService.RollbackTran();
                return toResponse(StatusCodeType.Error, ex.Message);
            }
        }



        [HttpGet]
        [Route("/images/Down/Bmd")]
        [Authorization]
        public FileStreamResult DownBmd([FromServices] IWebHostEnvironment environment, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }
            // 前端 blob 接收，具体查看前端admin代码
            string filepath = Path.Combine(environment.WebRootPath, filename);
            var stream = System.IO.File.OpenRead(filepath);
            //string fileExt = ".bmd";
            //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            //var memi = provider.Mappings[fileExt];
            var fileName = Path.GetFileName(filepath);

            HttpContext.Response.Headers.Add("fileName", fileName);

            return File(stream, "application/octet-stream", fileName);
        }

        // POST: api/Img
        [HttpPost]
        public void Post([FromBody] object formdata)
        {
        }

        // PUT: api/Img/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
