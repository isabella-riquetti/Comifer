using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DetailedBrandViewModel> GetAll()
        {
            var brands = _unitOfWork.Brand.Get()
                .Select(p => new DetailedBrandViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProductParents = p.ProductParents,
                    Provider = p.Provider,
                    ProviderId = p.ProviderId,
                    Products = p.Products,
                    SiteUrl = p.SiteUrl
                })
                .ToList();
            return brands;
        }

        public List<DetailedBrandViewModel> GetByProviderId(Guid providerId)
        {
            var brands = _unitOfWork.Brand.Get(b => b.ProviderId == providerId)
                .Select(p => new DetailedBrandViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProductParents = p.ProductParents,
                    Provider = p.Provider,
                    ProviderId = p.ProviderId,
                    Products = p.Products,
                    SiteUrl = p.SiteUrl
                })
                .ToList();
            return brands;
        }

        public DetailedBrandViewModel GetDetailed(Guid id)
        {
            var brand = _unitOfWork.Brand.Get(b => b.Id == id)
                .Select(p => new DetailedBrandViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Provider = p.Provider,
                    ProductParents = p.ProductParents,
                    ProviderId = p.ProviderId,
                    Products = p.Products,
                    SiteUrl = p.SiteUrl
                })
                .FirstOrDefault();
            return brand;
        }

        public Brand Get(Guid id)
        {
            var brand = _unitOfWork.Brand.FirstOrDefault(b => b.Id == id);
            return brand;
        }

        public NotificationViewModel Edit(Brand brand)
        {
            _unitOfWork.Brand.Edit(brand);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Marca editada com sucesso."
            };
        }

        public NotificationViewModel Create(Brand brand)
        {
            brand.Id = Guid.NewGuid();
            _unitOfWork.Brand.Add(brand);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Marca criada com sucesso."
            };
        }
    }
}
