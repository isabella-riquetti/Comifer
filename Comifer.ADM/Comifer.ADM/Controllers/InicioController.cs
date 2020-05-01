using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Comifer.ADM.Models;
using Comifer.ADM.ViewModels;
using Comifer.ADM.Services;

namespace Comifer.ADM.Controllers
{
    public class InicioController : Controller
    {
        private readonly ICustomerService _customerService;

        public InicioController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Principal()
        {
            var aux = _customerService.GetCustomersCount();
            var viewModel = new DashboardViewModel()
            {
                Customer = new DashboardItemViewModel()
                {
                    CurrentValue = aux,
                    Growth = 100
                },
                ProductParent = new DashboardItemViewModel()
                {
                    CurrentValue = 10,
                    Growth = 85
                },
                Product = new DashboardItemViewModel()
                {
                    CurrentValue = 10,
                    Growth = 57
                },
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
