using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.Model;

namespace TaiheSystem.CBE.Api.Common.Helpers
{
    public class FileHelper
    {
        public static string filerootpath = AppSettings.Configuration["FileSettiong:FileRootPath"]; //文件存储路径
        public static string iploadsize = AppSettings.Configuration["FileSettiong:UploadSize"]; //上传大小
        public static string allowtype = AppSettings.Configuration["FileSettiong:AllowType"]; //上传文件类型

        /// <summary>
        /// 保存文件至设置路径
        /// </summary>
        /// <param name="WebRootPath">接口服务所在路径</param>
        /// <param name="file">文件流</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="FileGroup">文件分类</param>
        /// <returns></returns>
        public static gnl_File CreateFile(string WebRootPath, IFormFile file, string FileName, string FileType = "Attach")
        {
            try
            {
                gnl_File gnlfile = new gnl_File();

                if (filerootpath == "")
                {
                    filerootpath = WebRootPath;
                }

                Guid fileid = Guid.NewGuid();

                string flodpath = Path.Combine(FileType, DateTime.Now.ToString("yyyyMM"));
                string folderpath = Path.Combine(filerootpath, flodpath);
                string ext = Path.GetExtension(FileName);
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }

                string path = Path.Combine(folderpath, fileid.ToString());

                using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    file.CopyTo(stream);
                }

                gnlfile.FileID = fileid.ToString().ToUpper(); //文件id
                gnlfile.FileName = FileName; //文件名
                gnlfile.FileExt = ext;   //文件后缀名
                gnlfile.FileRootPath = filerootpath; //存放路径
                gnlfile.AbsoluteFilePath = path;  //绝对路径
                gnlfile.RelativeFilePath = Path.Combine(flodpath, fileid.ToString());  //相对路径
                gnlfile.FileSize = (int)file.Length;  //文件大小
                gnlfile.FileType = FileType;

                return gnlfile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 异步保存文件至设置路径
        /// </summary>
        /// <param name="WebRootPath">接口服务所在路径</param>
        /// <param name="file">文件流</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="FileGroup">文件分类</param>
        /// <returns></returns>
        public static async Task<gnl_File> CreateFileAsync(string WebRootPath, IFormFile file, string FileName, string FileType = "Attach")
        {
            try
            {
                gnl_File gnlfile = new gnl_File();
                if (filerootpath == "")
                {
                    filerootpath = WebRootPath;
                }

                string flodpath = Path.Combine(FileType, DateTime.Now.ToString("yyyyMMdd"));
                string folderpath = Path.Combine(filerootpath, flodpath);
                string ext = Path.GetExtension(FileName);
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }
                Guid fileid = Guid.NewGuid();
                string path = Path.Combine(folderpath, fileid.ToString());

                using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(stream);
                }

                gnlfile.FileID = fileid.ToString().ToUpper(); //文件id
                gnlfile.FileName = FileName; //文件名
                gnlfile.FileExt = ext;   //文件后缀名
                gnlfile.FileRootPath = filerootpath; //存放路径
                gnlfile.AbsoluteFilePath = path;  //绝对路径
                gnlfile.RelativeFilePath = Path.Combine(flodpath, fileid.ToString());  //相对路径
                gnlfile.FileSize = (int)file.Length;  //文件大小

                return gnlfile;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
    }
}
