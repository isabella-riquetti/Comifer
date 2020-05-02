using System;
using Comifer.ADM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriasController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Principal()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        public IActionResult Detalhes(Guid id)
        {
            var category = _categoryService.GetDetailed(id);
            return View(category);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Data.Models.Category categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var result = _categoryService.Create(categoria);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }

        public IActionResult Editar(Guid id)
        {
            var category = _categoryService.Get(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Editar(Data.Models.Category categoria)
        {
            if(!ModelState.IsValid)
            {
                return View(categoria);
            }

            var result = _categoryService.Edit(categoria);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}