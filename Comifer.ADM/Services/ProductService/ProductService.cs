using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<DetailedProductViewModel> GetAll(Guid? productParentId, Guid? brandId, string text)
        {
            var products = _unitOfWork.Product.Get();

            if (brandId != null)
                products = products.Where(b => b.BrandId == brandId);
            if (productParentId != null)
                products = products.Where(b => b.ProductParentId == productParentId);
            if (!string.IsNullOrEmpty(text))
                products = products.Where(b => b.Code.Contains(text) || b.Name.Contains(text));

            var formatedProducts = products.Select(p => new DetailedProductViewModel()
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

            var groups = formatedProducts.Where(p => p.ProductGroupId != null).Select(p => p.ProductGroupId).Distinct().ToList();
            var compatibles = _unitOfWork.Product
                .Get(pg => groups.Contains(pg.ProductGroupId) && pg.IsMainInGroup)
                .GroupBy(pg => pg.ProductGroupId.Value)
                .ToDictionary(pg => pg.Key, pg => pg.FirstOrDefault()?.Code);

            formatedProducts.ForEach(p => p.ProductMainInGroupCode = (p.ProductGroupId == null ? null : compatibles[p.ProductGroupId.Value]));
            return formatedProducts;
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
                    Cost = p.Cost,
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

            product.CostValue = product.Cost?.ToString(CultureInfo.InvariantCulture);
            product.PriceValue = product.Price?.ToString(CultureInfo.InvariantCulture);
            product.WeightValue = product.Weight?.ToString(CultureInfo.InvariantCulture);

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
            existingProduct.Supply = product.Supply;
            existingProduct.Cost = Convert.ToDecimal(product.CostValue, CultureInfo.InvariantCulture);
            existingProduct.Price = Convert.ToDecimal(product.PriceValue, CultureInfo.InvariantCulture);
            existingProduct.Weight = Convert.ToDecimal(product.WeightValue, CultureInfo.InvariantCulture);
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
                IsMainInGroup = product.IsMainInGroup
            };

            newProduct.Cost = Convert.ToDecimal(product.CostValue, CultureInfo.InvariantCulture);
            newProduct.Price = Convert.ToDecimal(product.PriceValue, CultureInfo.InvariantCulture);
            newProduct.Weight = Convert.ToDecimal(product.WeightValue, CultureInfo.InvariantCulture);

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
                else if (newProduct.IsMainInGroup && compatibleProduct.IsMainInGroup)
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

        public ProductFastEditViewModel GetFast(Guid id)
        {
            var product = _unitOfWork.Product.Get(b => b.Id == id)
                .Select(p => new ProductFastEditViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    BrandName = p.Brand.Name,
                    ProductParent = p.ProductParent
                })
                .FirstOrDefault();
            product.WeightValue = product.Weight?.ToString(CultureInfo.InvariantCulture);

            var files = _unitOfWork.File.Get(f => f.ReferId == id);
            product.HasPicture = files.Any();

            return product;
        }

        public NotificationViewModel EditFast(ProductFastEditViewModel product)
        {
            var existingProduct = _unitOfWork.Product.Get(p => p.Id == product.Id).FirstOrDefault();

            if (product.Weight != null)
                existingProduct.Weight = Convert.ToDecimal(product.WeightValue, CultureInfo.InvariantCulture);

            if (existingProduct.Supply == 0 && product.Supply != null)
                existingProduct.Supply = product.Supply.Value;

            if (product.SupplyChange != null)
                existingProduct.Supply += product.SupplyChange.Value;

            if (existingProduct.Supply < 0)
            {
                return new NotificationViewModel()
                {
                    Status = false,
                    Title = "Erro!",
                    Message = "O estoque não pode ser negativo.",
                    Focus = "#search"
                };
            }

            _unitOfWork.Product.Edit(existingProduct);
            _unitOfWork.Commit();

            _fileService.UploadFiles(product.Files, existingProduct.Id, "Product");
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Produto editado com sucesso.",
                Focus = "#search"
            };
        }
    }
}
