using Microsoft.AspNetCore.Mvc;

namespace Confectionery.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
