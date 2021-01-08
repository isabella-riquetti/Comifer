using Comifer.ADM.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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

        public IActionResult Principal(Guid? idFornecedor)
        {
            ViewBag.ProviderId = idFornecedor;
            ViewBag.Providers = _providerService.GetSelectListWithAll();

            var brands = _brandService.GetAll(idFornecedor);
            return View(brands);
        }

        public IActionResult Detalhes(Guid id)
        {
            var brand = _brandService.GetDetailed(id);
            return View(brand);
        }

        public IActionResult Incluir()
        {
            ViewBag.Providers = _providerService.GetSelectList();

            return View();
        }

        [HttpPost]
        public IActionResult Incluir(Data.Models.Brand brand)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Providers = _providerService.GetSelectList();
                return View(brand);
            }

            var result = _brandService.Create(brand);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }

        public IActionResult Editar(Guid id)
        {
            ViewBag.Providers = _providerService.GetSelectList();

            var category = _brandService.Get(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Editar(Data.Models.Brand brand)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Providers = _providerService.GetSelectList();
                return View(brand);
            }

            var result = _brandService.Edit(brand);
            TempData.Put("Notification", result);
            return RedirectToAction("Principal");
        }
    }
}