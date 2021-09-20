using System;
using Microsoft.AspNetCore.Mvc;
using DocApi.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocApi.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {

        public static IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        static String[] uploadableFiles = { "application/pdf", "text/html", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" , "text/plain", "application/xml" };



        // POST api/upload
        [HttpPost]
        public IActionResult PostUploadDoc([FromForm] Upload formFile)
        {
            try
            {
                if (formFile.files != null && formFile.files.Length > 0)
                {
                    if(uploadableFiles.Contains(formFile.files.ContentType))
                    {
                        string path = _webHostEnvironment.WebRootPath + "\\uploads\\";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        using FileStream fileStream = System.IO.File.Create(path + formFile.files.FileName);
                        formFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return Ok("Uploaded successfully");
                    }
                }

                return BadRequest();
                
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
