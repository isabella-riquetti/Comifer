using System;
using Comifer.ADM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class MarcasController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IProviderService _providerService;

        public MarcasController(IBrandService brandService, IProviderService providerService)
        {
            _brandService = brandService;
            _providerService = providerService;
        }

        public IActionResult Principal()
        {
            var brands = _brandService.GetAll();
            return View(brands);
        }

        public IActionResult DoFornecedor(Guid providerId, string providerName)
        {
            ViewBag.ProviderId = providerId;
            ViewBag.ProviderName = providerName;
            var brands = _brandService.GetByProviderId(providerId);
            return View(brands);
        }

        public IActionResult Detalhes(Guid id)
        {
            var brand = _brandService.GetDetailed(id);
            return View(brand);
        }

        public IActionResult Criar(Guid? providerId)
        {
            ViewBag.Providers = _providerService.GetSelectList();
            ViewBag.ProviderId = providerId;
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Data.Models.Brand categoria)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Providers = _providerService.GetSelectList();
                return View(categoria);
            }

            var result = _brandService.Create(categoria);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }

        public IActionResult Editar(Guid id)
        {
            var category = _brandService.Get(id);
            ViewBag.Providers = _providerService.GetSelectList();
            return View(category);
        }

        [HttpPost]
        public IActionResult Editar(Data.Models.Brand categoria)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Providers = _providerService.GetSelectList();
                return View(categoria);
            }

            var result = _brandService.Edit(categoria);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}