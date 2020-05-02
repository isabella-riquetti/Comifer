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
            var viewModel = new DashboardViewModel()
            {
                Customers = new DashboardItemViewModel()
                {
                    CurrentValue = 1,
                    Growth = 100
                },
                Orders = new DashboardItemViewModel()
                {
                    CurrentValue = 16,
                    Growth = 50
                },
                ProductParents = new DashboardItemViewModel()
                {
                    CurrentValue = 10
                },
                Products = new DashboardItemViewModel()
                {
                    CurrentValue = 10
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
