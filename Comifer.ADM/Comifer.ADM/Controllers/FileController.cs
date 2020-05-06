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

        public IActionResult Remove(Guid id, Guid referId, string controllerName)
        {
            var result = _fileService.Remove(id);
            TempData.Put("Notification", result);
            return RedirectToAction("Editar", controllerName, new { id = referId });
        }

        public FileResult Download(Guid id)
        {
            var file = _fileService.Get(id);
            byte[] fileBytes = file.FileBytes;
            string fileName = file.FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}