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

namespace TaiheSystem.CBE.Api.Hostd.Controllers.Sys
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : BaseController
    {
        // GET: api/Download
        /// <summary>
        /// 下载图片（支持中文字符）
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/images/Down/Pic")]
        public FileStreamResult DownImg([FromServices] IWebHostEnvironment environment)
        {
            //try
            //{
                string foldername = "";
                string filepath = Path.Combine(string.IsNullOrEmpty(environment.WebRootPath) ? environment.ContentRootPath : environment.WebRootPath, foldername, "测试下载中文名称的图片.png");
                var stream = System.IO.File.OpenRead(filepath);
                string fileExt = ".jpg";  // 这里可以写一个获取文件扩展名的方法，获取扩展名
                                          //获取文件的ContentType
                var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
                var memi = provider.Mappings[fileExt];
                var fileName = Path.GetFileName(filepath);


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
        [Route("/images/Upload/Pic")]
        public IActionResult UploadFle([FromServices] IWebHostEnvironment environment)
        {
            string path = string.Empty;
            string foldername = "images";
            IFormFileCollection files = null;


            // 获取提交的文件
            files = Request.Form.Files;
            // 获取附带的数据
            var max_ver = Request.Form["max_ver"].ObjToString();

            if (files == null || !files.Any()) { return toResponse(StatusCodeType.Error, "请选择上传的文件");}

            //格式限制
            var allowType = new string[] { "image/jpg","image/png", "image/jpeg"};

            string filerootpath = string.IsNullOrEmpty(environment.WebRootPath) ? environment.ContentRootPath : environment.WebRootPath;

            string folderpath = Path.Combine(filerootpath, foldername);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    foreach (var file in files)
                    {
                        //var file = files.FirstOrDefault();
                        string strpath = Path.Combine(foldername, DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                        path = Path.Combine(filerootpath, strpath);

                        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    return toResponse("保存成功");
                }
                else
                {
                    return toResponse(StatusCodeType.Error, "文件过大");
                }
            }
            else
            {
                return toResponse(StatusCodeType.Error, "文件格式错误");
            }
        }

        /// <summary>
        /// 异步上传图片,多文件，可以使用 postman 测试，
        /// 如果是单文件，可以 参数写 IFormFile file1
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/images/UploadAsync/Pic")]
        public async Task<ApiResult<string>> UploadFleAsync([FromServices] IWebHostEnvironment environment)
        {
            var data = new ApiResult<string>();
            string path = string.Empty;
            string foldername = "images";
            IFormFileCollection files = null;


            // 获取提交的文件
            files = Request.Form.Files;
            // 获取附带的数据
            var max_ver = Request.Form["max_ver"].ObjToString();


            if (files == null || !files.Any()) { data.Message = "请选择上传的文件。"; return data; }
            //格式限制
            var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };

            string filerootpath = string.IsNullOrEmpty(environment.WebRootPath) ? environment.ContentRootPath : environment.WebRootPath;

            string folderpath = Path.Combine(filerootpath, foldername);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    //foreach (var file in files)
                    var file = files.FirstOrDefault();
                    string strpath = Path.Combine(foldername, DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                    path = Path.Combine(filerootpath, strpath);

                    using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(stream);
                    }

                    data = new ApiResult<string>()
                    {
                        Data = strpath,
                        Message = "上传成功",
                        Success = true,
                    };
                    return data;
                }
                else
                {
                    data.Message = "文件过大";
                    return data;
                }
            }
            else

            {
                data.Message = "文件格式错误";
                return data;
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
