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
    public class VistaExplodidasController : Controller
    {
        private readonly IProductParentService _productParentService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public VistaExplodidasController(IProductParentService productParentService, IBrandService brandService, ICategoryService categoryService)
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
            var productParent = _productParentService.GetDetailed(id);
            return View(productParent);
        }

        public IActionResult IncluirArquivos(Guid id)
        {
            var productParent = _productParentService.GetDetailedWithFiles(id);
            return View(productParent);
        }

        public IActionResult Incluir()
        {
            ViewBag.Categories = _categoryService.GetSelectList();
            ViewBag.Brands = _brandService.GetSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Incluir(ProductParent productParent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetSelectList();
                ViewBag.Brands = _brandService.GetSelectList();
                return View(productParent);
            }

            var result  = _productParentService.Create(productParent);
            TempData.Put("Notification", result);

            return RedirectToAction("IncluirArquivos", new { id = productParent.Id });
        }

        public IActionResult Editar(Guid id)
        {
            ViewBag.Categories = _categoryService.GetSelectList();
            ViewBag.Brands = _brandService.GetSelectList();

            var category = _productParentService.Get(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Editar(ProductParent productParent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetSelectList();
                ViewBag.Brands = _brandService.GetSelectList();
                return View(productParent);
            }

            var result = _productParentService.Edit(productParent);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}