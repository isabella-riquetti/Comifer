﻿using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DetailedCategoryViewModel> GetAll()
        {
            var categories = _unitOfWork.Category.Get()
                .Select(c => new DetailedCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products,
                    ProductParents = c.ProductParents
                })
                .ToList();
            return categories;
        }

        public DetailedCategoryViewModel GetDetailed(Guid id)
        {
            var category = _unitOfWork.Category.Get(c => c.Id == id)
                .Select(c => new DetailedCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products,
                    ProductParents = c.ProductParents
                })
                .FirstOrDefault();
            return category;
        }

        public Category Get(Guid id)
        {
            var category = _unitOfWork.Category.FirstOrDefault(c => c.Id == id);
            return category;
        }

        public NotificationViewModel Edit(Category category)
        {
            _unitOfWork.Category.Edit(category);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Categoria editada com sucesso."
            };
        }

        public NotificationViewModel Create(Category category)
        {
            category.Id = Guid.NewGuid();
            _unitOfWork.Category.Add(category);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Categoria criada com sucesso."
            };
        }
    }
}