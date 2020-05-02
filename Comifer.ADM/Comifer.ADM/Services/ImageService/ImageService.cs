using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Image> GetAllByReferId(Guid referId)
        {
            var images = _unitOfWork.Image.Get().ToList();
            return images;
        }

        public Image Get(Guid id)
        {
            var image = _unitOfWork.Image.FirstOrDefault(b => b.Id == id);
            return image;
        }

        public NotificationViewModel Edit(Image image)
        {
            _unitOfWork.Image.Edit(image);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Imagem editada com sucesso."
            };
        }

        public NotificationViewModel Create(Image image)
        {
            image.Id = Guid.NewGuid();
            _unitOfWork.Image.Add(image);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Imagem inserida com com sucesso."
            };
        }
    }
}
