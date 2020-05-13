using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IPromotionService _promotionService;

        public ProductService(IUnitOfWork unitOfWork, IFileService fileService, IPromotionService promotionService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _promotionService = promotionService;
        }

        public DashboardItemViewModel GetCount()
        {
            var productsRegistered = _unitOfWork.Product.Get().Count();

            return new DashboardItemViewModel()
            {
                CurrentValue = productsRegistered * 1.0m,
                Growth = null
            };
        }

        public DashboardItemViewModel GetFilledCount()
        {
            var filledProductsRegistered = _unitOfWork.Product.Get(p => p.Price != null && p.Price != 0
            && p.Weight != null && p.Weight != 0
            && p.Supply != 0).Count();

            return new DashboardItemViewModel()
            {
                CurrentValue = filledProductsRegistered * 1.0m,
                Growth = null
            };
        }

        public List<DetailedProductViewModel> GetAll(Guid? productParentId, Guid? brandId)
        {
            var products = _unitOfWork.Product
                .Get(b => (brandId == null || b.BrandId == brandId)
                && (productParentId == null || b.ProductParentId == productParentId))
                .Select(p => new DetailedProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    ProductGroupId = p.ProductGroupId,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    ProductParentId = p.ProductParentId,
                    ProductParent = p.ProductParent
                })
                .OrderBy(b => b.Name)
                .ToList();

            var groups = products.Where(p => p.ProductGroupId != null).Select(p => p.ProductGroupId).Distinct().ToList();
            var compatibles = _unitOfWork.Product
                .Get(pg => groups.Contains(pg.ProductGroupId) && pg.IsMainInGroup)
                .GroupBy(pg => pg.ProductGroupId.Value)
                .ToDictionary(pg => pg.Key, pg => pg.FirstOrDefault()?.Code);

            products.ForEach(p => p.ProductMainInGroupCode = (p.ProductGroupId == null ? null : compatibles[p.ProductGroupId.Value]));
            return products;
        }

        public DetailedProductViewModel GetDetailed(Guid id)
        {
            var product = _unitOfWork.Product.Get(b => b.Id == id)
                .Select(p => new DetailedProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    ProductGroupId = p.ProductGroupId,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    ProductParentId = p.ProductParentId,
                    ProductParent = p.ProductParent,
                    Cost = p.Cost
                })
                .FirstOrDefault();

            product.Compatibility = GetCompatibleProductDetails(product);
            product.FilesInfo = _fileService.GetFileInfoByReferId(product.Id);
            product.PromotionInfos = GetPromotions(product);
            return product;
        }

        private List<BasicPromotionInfo> GetPromotions(DetailedProductViewModel product)
        {
            var promotions = _unitOfWork.Promotion
                .Get(p => p.ProductId == product.Id)
                .OrderByDescending(p => p.ExpiresOn)
                .Select(p => new BasicPromotionInfo()
                {
                    ExpiresOn = p.ExpiresOn,
                    Percentage = p.Percentage,
                    Value = p.Value
                })
                .ToList();
            return promotions;
        }

        private List<BasicProdutInfo> GetCompatibleProductDetails(DetailedProductViewModel product)
        {
            if (product.ProductGroupId != null)
            {
                var productsInGroup = _unitOfWork.Product.Get(p => p.ProductGroupId == product.ProductGroupId && p.Id != product.Id);
                var compatible = productsInGroup
                    .Select(p => new BasicProdutInfo()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Code = p.Code,
                        IsMainInGroup = p.IsMainInGroup
                    })
                    .OrderBy(b => b.Name)
                    .ToList();

                return compatible;
            }
            return new List<BasicProdutInfo>();
        }

        public ProductEditViewModel Get(Guid id)
        {
            var product = _unitOfWork.Product.Get(b => b.Id == id)
                .Select(p => new ProductEditViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    ProductGroupId = p.ProductGroupId,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    ProductParentId = p.ProductParentId,
                    ProductParent = p.ProductParent,
                    IsMainInGroup = p.IsMainInGroup
                })
                .FirstOrDefault();

            if (product.ProductGroupId != null)
            {
                var productsInGroup = _unitOfWork.Product.Get(p => p.ProductGroupId == product.ProductGroupId && p.Id != product.Id);
                GetMainProductInfo(product, productsInGroup);
                GetCompatibleProducts(product, productsInGroup);
            }

            var files = _fileService.GetFileInfoByReferId(product.Id);
            product.FilesInfo = files;

            return product;
        }

        private void GetCompatibleProducts(ProductEditViewModel product, IQueryable<Product> productsInGroup)
        {
            var compatible = productsInGroup
                .Select(p => new BasicProdutInfo()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    IsMainInGroup = p.IsMainInGroup
                })
                .OrderBy(b => b.Name)
                .ToList();
            product.Compatibility = compatible;
        }

        private void GetMainProductInfo(ProductEditViewModel product, IQueryable<Product> productsInGroup)
        {
            if (!product.IsMainInGroup)
            {
                var main = productsInGroup.Where(p => p.IsMainInGroup).FirstOrDefault();
                product.BrandOfMainProductInGroupId = main?.BrandId;
                product.ProductInGroupId = main?.Id;
            }
        }

        public NotificationViewModel Edit(ProductEditViewModel product)
        {
            var existingProduct = _unitOfWork.Product.Get(p => p.Id == product.Id).FirstOrDefault();

            existingProduct.Name = product.Name;
            existingProduct.Code = product.Code;
            existingProduct.BrandId = product.BrandId;
            existingProduct.ProductParentId = product.ProductParentId;
            existingProduct.ProductGroupId = product.ProductGroupId;
            existingProduct.Weight = product.Weight;
            existingProduct.Supply = product.Supply;
            existingProduct.Cost = product.Cost;
            existingProduct.Price = product.Price;
            existingProduct.IsMainInGroup = product.IsMainInGroup;

            CompatibilityAjuster(existingProduct, product.ProductInGroupId);

            _unitOfWork.Product.Edit(existingProduct);
            _unitOfWork.Commit();

            _fileService.UploadFiles(product.Files, existingProduct.Id, "Product");
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Produto editado com sucesso."
            };
        }

        public NotificationViewModel Create(ProductViewModel product)
        {
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                BrandId = product.BrandId,
                ProductGroupId = product.ProductGroupId,
                ProductParentId = product.ProductParentId,
                Name = product.Name,
                Code = product.Code,
                Supply = product.Supply,
                Weight = product.Weight,
                Cost = product.Cost,
                Price = product.Price,
                IsMainInGroup = product.IsMainInGroup
            };

            CompatibilityAjuster(newProduct, product.ProductInGroupId);

            _unitOfWork.Product.Add(newProduct);
            _unitOfWork.Commit();
            _fileService.UploadFiles(product.Files, newProduct.Id, "Product");

            _promotionService.Create(newProduct.Id, 25);

            var result = new NotificationViewModel()
            {
                Message = "Produto criado com sucesso.",
                Status = true,
                Title = "Sucesso!"
            };
            return result;
        }

        private void CompatibilityAjuster(Product newProduct, Guid? productInGroupId)
        {
            if (productInGroupId != null)
            {
                var compatibleProduct = _unitOfWork.Product.Get(p => p.Id == productInGroupId).FirstOrDefault();
                if (compatibleProduct.ProductGroupId == null)
                {
                    newProduct.ProductGroupId = Guid.NewGuid();
                    compatibleProduct.ProductGroupId = newProduct.ProductGroupId;
                    compatibleProduct.IsMainInGroup = !newProduct.IsMainInGroup;
                    _unitOfWork.Product.Edit(compatibleProduct);
                }
                else if(newProduct.IsMainInGroup && compatibleProduct.IsMainInGroup)
                {
                    newProduct.ProductGroupId = compatibleProduct.ProductGroupId;

                    compatibleProduct.IsMainInGroup = false;
                    _unitOfWork.Product.Edit(compatibleProduct);
                }
                else
                {
                    newProduct.ProductGroupId = compatibleProduct.ProductGroupId;

                    if (newProduct.IsMainInGroup)
                    {
                        var mainInGroup = _unitOfWork.Product
                            .Get(p => p.ProductGroupId == compatibleProduct.ProductGroupId
                            && p.Id != newProduct.Id
                            && p.IsMainInGroup).FirstOrDefault();
                        mainInGroup.IsMainInGroup = false;
                        _unitOfWork.Product.Edit(mainInGroup);
                    }
                }
            }
            else if (!newProduct.IsMainInGroup)
            {
                newProduct.ProductGroupId = null;
            }
        }

        public List<SelectListItem> GetSelectList(Guid brandId, Guid? id)
        {
            var result = _unitOfWork.Product.Get(p => p.BrandId == brandId && p.IsMainInGroup && (id == null || p.Id != id)).ToSelectList(p => p.Id.ToString(), p => p.Code);
            return result;
        }
    }
}
