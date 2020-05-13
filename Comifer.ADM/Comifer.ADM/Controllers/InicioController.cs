using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Comifer.ADM.Models;
using Comifer.ADM.ViewModels;
using Comifer.ADM.Services;

namespace Comifer.ADM.Controllers
{
    public class InicioController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IProductParentService _productParentService;

        public InicioController(ICustomerService customerService, IProductService productService, IProductParentService productParentService)
        {
            _customerService = customerService;
            _productService = productService;
            _productParentService = productParentService;
        }

        public IActionResult Principal()
        {
            var viewModel = new DashboardViewModel()
            {
                Customers = _customerService.GetCountAndGrowth(),
                ProductParents = _productParentService.GetCount(),
                Products = _productService.GetCount(),
                FilledProducts = _productService.GetFilledCount()
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
