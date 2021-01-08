using System;
using Comifer.ADM.Services;
using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class VistasExplodidasController : Controller
    {
        private readonly IProductParentService _productParentService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public VistasExplodidasController(IProductParentService productParentService, IBrandService brandService, ICategoryService categoryService)
        {
            _productParentService = productParentService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        public IActionResult Principal(Guid? idMarca, Guid? idCategoria)
        {
            ViewBag.CategoryId = idCategoria;
            ViewBag.Categories = _categoryService.GetSelectListWithAll();
            ViewBag.BrandId = idMarca;
            ViewBag.Brands = _brandService.GetSelectListWithAll();
            
            var productParents = _productParentService.GetAll(idMarca, idCategoria);
            return View(productParents);
        }

        public IActionResult Detalhes(Guid id)
        {
            var product = _productParentService.GetDetailed(id);
            return View(product);
        }

        public IActionResult Incluir()
        {
            ViewBag.Brands = _brandService.GetSelectList();
            ViewBag.Categories = _categoryService.GetSelectList();
            return View(new ProductParentViewModel());
        }

        [HttpPost]
        public IActionResult Incluir(ProductParentViewModel productParent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetSelectList();
                ViewBag.Categories = _categoryService.GetSelectList();
                return View(productParent);
            }

            var result = _productParentService.Create(productParent);
            TempData.Put("Notification", result);

            return RedirectToAction("Principal");
        }

        public IActionResult Editar(Guid id)
        {
            ViewBag.Brands = _brandService.GetSelectList();
            ViewBag.Categories = _categoryService.GetSelectList();

            var productParent = _productParentService.GetWithFiles(id);
            return View(productParent);
        }

        [HttpPost]
        public IActionResult Editar(ProductParentEditViewModel productParent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetSelectList();
                ViewBag.Categories = _categoryService.GetSelectList();
                return View(productParent);
            }

            var result = _productParentService.Edit(productParent);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}