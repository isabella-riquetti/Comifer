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

        public NotificationViewModel Edit(Data.Models.File image)
        {
            _unitOfWork.File.Edit(image);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Imagem editada com sucesso."
            };
        }

        public NotificationViewModel Create(Data.Models.File image)
        {
            image.Id = Guid.NewGuid();
            _unitOfWork.File.Add(image);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Imagem inserida com com sucesso."
            };
        }

        public NotificationViewModel UploadFiles(List<IFormFile> files, Guid referId, string tableName)
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
