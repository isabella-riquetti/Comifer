using Microsoft.AspNetCore.Mvc;

namespace Comifer.ADM.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}