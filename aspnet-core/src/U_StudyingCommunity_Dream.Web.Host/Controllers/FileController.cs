﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using U_StudyingCommunity_Dream.Dtos;

namespace U_StudyingCommunity_Dream.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : AbpController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JsonResult> UploadFilesAsync(IFormFile[] file)
        {
            var date = Request;
            var files = Request.Form.Files;
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var filePath = string.Empty;
            var returnUrl = string.Empty;
            var fileName = string.Empty;
            long fileSize = 0;
            string fileExt = string.Empty;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    fileName = formFile.FileName.Substring(0, formFile.FileName.IndexOf('.'));
                    fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    fileSize = formFile.Length; //获得文件大小，以字节为单位
                    var uid = Guid.NewGuid().ToString();
                    string newFileName = uid + fileExt; //随机生成新的文件名
                    var fileDire = webRootPath + "/docfiles/";
                    if (!Directory.Exists(fileDire))
                    {
                        Directory.CreateDirectory(fileDire);
                    }
                    filePath = fileDire + newFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    returnUrl = "/docfiles/" + newFileName;
                }
            }
            var apiResult = new APIResult();
            apiResult.Code = 0;
            apiResult.Msg = "上传文件成功";
            apiResult.Data = new { name = fileName, size = fileSize, ext = fileExt, url = returnUrl };
            return Json(apiResult);

        }
    }
}