using System;
using System.Collections.Generic;
using System.Linq;
using Comifer.ADM.Services;
using Comifer.ADM.ViewModels;
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

        public IActionResult Principal(Guid? idVistaExplodida, Guid? idMarca, string text = "", bool fromSearch = false)
        {
            ViewBag.BrandId = idMarca;
            ViewBag.Brands = _brandService.GetSelectListWithAll();
            ViewBag.ProductId = idVistaExplodida;
            ViewBag.ProductParents = _productParentService.GetSelectListWithAll();
            ViewBag.Text = text;

            if (idMarca == null && idVistaExplodida == null && String.IsNullOrEmpty(text))
            {
                if (ModelState.ContainsKey("idMarca"))
                    ModelState["idMarca"].Errors.Clear();
                if (ModelState.ContainsKey("idVistaExplodida"))
                    ModelState["idVistaExplodida"].Errors.Clear();
                if (ModelState.ContainsKey("idVistaExplodida"))
                    ModelState["text"].Errors.Clear();

                ModelState.AddModelError("idMarca", "É necessário informar ao menos um filtro para busca");
                ModelState.AddModelError("idVistaExplodida", "É necessário informar ao menos um filtro para busca");
                ModelState.AddModelError("text", "É necessário informar ao menos um filtro para busca");

                return View(new List<DetailedProductViewModel>());
            }
            else
            {
                if (ModelState.ContainsKey("idMarca"))
                    ModelState["idMarca"].Errors.Clear();
                if (ModelState.ContainsKey("idVistaExplodida"))
                    ModelState["idVistaExplodida"].Errors.Clear();
                if (ModelState.ContainsKey("idVistaExplodida"))
                    ModelState["text"].Errors.Clear();

                var products = _productService.GetAll(idVistaExplodida, idMarca, text);

                if (fromSearch)
                {
                    var exactCodes = products.Where(p => p.Code == text).ToList();
                    if (exactCodes.Count == 1)
                    {
                        var exactCodeId = exactCodes.FirstOrDefault().Id;
                        return RedirectToAction("EditarRapido", new { id = exactCodeId });
                    }
                }

                return View(products);
            }
        }

        public ActionResult Search(string search)
        {
            return RedirectToAction("Principal", new { text = search, fromSearch = true });
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
            if (product.BrandOfMainProductInGroupId != null && product.ProductInGroupId == null)
            {
                ModelState.AddModelError("ProductInGroupId", "Para incluir o produto em um grupo de compatibilidade é necessário selecionar um produto.");
            }
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

        public IActionResult EditarRapido(Guid id)
        {
            var product = _productService.GetFast(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditarRapido(ProductFastEditViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var result = _productService.EditFast(product);
            TempData.Put("Notification", result);
            if (!result.Status)
            {
                return View(product);
            }
            return RedirectToAction("Principal");
        }

        [HttpGet]
        public JsonResult GetSelectList(Guid brandId, Guid? id)
        {
            var result = _productService.GetSelectList(brandId, id);

            return Json(result);
        }
    }
}