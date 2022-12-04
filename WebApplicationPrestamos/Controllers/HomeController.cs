using Microsoft.AspNetCore.Mvc;

namespace WebApplicationPrestamos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
