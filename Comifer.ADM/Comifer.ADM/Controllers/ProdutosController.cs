using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Comifer.ADM.Services;
using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductParentService _productParentService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public ProdutosController(IProductService productService, IProductParentService productParentService, 
            IBrandService brandService, ICategoryService categoryService)
        {
            _productService = productService;
            _productParentService = productParentService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        public IActionResult Principal(Guid? idVistaExplodida, Guid? idMarca)
        {
            ViewBag.BrandId = idMarca;
            ViewBag.Brands = _brandService.GetSelectListWithAll();
            ViewBag.ProductId = idVistaExplodida;
            ViewBag.ProductParents = _productParentService.GetSelectListWithAll();

            var products = _productService.GetAll(idVistaExplodida, idMarca);
            return View(products);
        }

        public IActionResult Detalhes(Guid id)
        {
            var product = _productService.GetDetailed(id);
            return View(product);
        }

        public IActionResult Incluir()
        {
            ViewBag.Brands = _brandService.GetSelectList();
            ViewBag.ProductParents = _productParentService.GetSelectList();
            return View(new ProductViewModel());
        }

        [HttpPost]
        public IActionResult Incluir(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetSelectList();
                ViewBag.ProductParents = _productParentService.GetSelectList();
                return View(product);
            }

            var result = _productService.Create(product);
            TempData.Put("Notification", result);

            return RedirectToAction("Principal");
        }

        public IActionResult Editar(Guid id)
        {
            ViewBag.Brands = _brandService.GetSelectList();
            ViewBag.ProductParents = _productParentService.GetSelectList();

            var product = _productService.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Editar(ProductEditViewModel product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetSelectList();
                ViewBag.ProductParents = _productParentService.GetSelectList();
                return View(product);
            }

            var result = _productService.Edit(product);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}