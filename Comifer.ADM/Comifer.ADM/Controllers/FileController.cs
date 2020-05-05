using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comifer.ADM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public IActionResult UploadFile(List<IFormFile> files, Guid referId, string tableName, string controllerName)
        {
            var result = _fileService.UploadFiles(files, referId, tableName);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal", controllerName);
        }

        public IActionResult Remove(Guid referId, string controllerName)
        {
            var result = _fileService.Remove(referId);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal", controllerName);
        }

        public FileResult Download(Guid referId)
        {
            var file = _fileService.Get(referId);
            byte[] fileBytes = file.FileBytes;
            string fileName = file.FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}