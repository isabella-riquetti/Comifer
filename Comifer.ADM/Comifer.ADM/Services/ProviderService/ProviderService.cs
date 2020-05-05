using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Comifer.ADM.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DetailedProviderViewModel> GetAll()
        {
            var providers = _unitOfWork.Provider.Get()
                .Select(b => new DetailedProviderViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    DeliveryTime = b.DeliveryTime,
                    Brands = b.Brands
                })
                .ToList();
            return providers;
        }

        public DetailedProviderViewModel GetDetailed(Guid id)
        {
            var provider = _unitOfWork.Provider.Get()
                .Select(b => new DetailedProviderViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    DeliveryTime = b.DeliveryTime,
                    Brands = b.Brands
                })
                .FirstOrDefault();
            return provider;
        }

        public Provider Get(Guid id)
        {
            var category = _unitOfWork.Provider.FirstOrDefault(b => b.Id == id);
            return category;
        }

        public NotificationViewModel Edit(Provider provider)
        {
            _unitOfWork.Provider.Edit(provider);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Fornecedor editado com sucesso."
            };
        }

        public NotificationViewModel Create(Provider provider)
        {
            provider.Id = Guid.NewGuid();
            _unitOfWork.Provider.Add(provider);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Fornecedor criado com sucesso."
            };
        }

        public List<SelectListItem> GetSelectList()
        {
            var result = _unitOfWork.Provider.Get().ToSelectList(p => p.Id.ToString(), p => p.Name);
            return result;
        }

        public List<SelectListItem> GetSelectListWithAll()
        {
            var result = _unitOfWork.Provider.Get().ToSelectListAndAll(p => p.Id.ToString(), p => p.Name);
            return result;
        }
    }
}
