using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Data.Models.File> GetAllByReferId(Guid referId)
        {
            var images = _unitOfWork.File.Get().ToList();
            return images;
        }

        public Data.Models.File Get(Guid id)
        {
            var image = _unitOfWork.File.FirstOrDefault(b => b.Id == id);
            return image;
        }

        public List<ViewModels.FileInfo> GetFileInfoByReferId(Guid referId)
        {
            var fileInfos = new List<ViewModels.FileInfo>();
            var files = _unitOfWork.File.Get(i => i.ReferId == referId).Select(i => new { i.FileName, i.MIME, i.FileBytes, i.Id }).ToList();
            foreach (var file in files)
            {
                fileInfos.Add(new ViewModels.FileInfo()
                {
                    Id = file.Id,
                    MIME = file.MIME,
                    FileName = file.FileName,
                    Base64File = Convert.ToBase64String(file.FileBytes)
                });
            }
            return fileInfos;
        }

        public NotificationViewModel UploadFiles(List<IFormFile> files, Guid referId, string tableName)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        var byteArray = memoryStream.ToArray();
                        var newFile = new Data.Models.File()
                        {
                            Id = Guid.NewGuid(),
                            ReferId = referId,
                            TableName = tableName,
                            Type = "Arquivo",
                            FileName = file.FileName,
                            MIME = file.ContentType,
                            Priority = 0,
                            FileBytes = byteArray
                        };
                        _unitOfWork.File.Add(newFile);
                    }
                }

                _unitOfWork.Commit();

                return new NotificationViewModel()
                {
                    Message = "Arquivo incluso com sucesso.",
                    Title = "Sucesso!",
                    Status = true
                };
            }
            return null;
        }

        public NotificationViewModel Remove(Guid id)
        {
            var file = Get(id);
            _unitOfWork.File.Delete(file);
            _unitOfWork.Commit();

            return new NotificationViewModel()
            {
                Message = "Arquivo removido com sucesso.",
                Title = "Sucesso!",
                Status = true
            };
        }
    }
}
