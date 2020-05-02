using System;
using Comifer.ADM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly IProviderService _providerService;

        public FornecedoresController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        public IActionResult Principal()
        {
            var categories = _providerService.GetAll();
            return View(categories);
        }

        public IActionResult Detalhes(Guid id)
        {
            var category = _providerService.GetDetailed(id);
            return View(category);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Data.Models.Provider categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var result = _providerService.Create(categoria);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }

        public IActionResult Editar(Guid id)
        {
            var category = _providerService.Get(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Editar(Data.Models.Provider categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var result = _providerService.Edit(categoria);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}