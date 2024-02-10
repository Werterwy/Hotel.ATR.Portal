using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR.Portal.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RenderPartial()
        {
            return View();
        }
    }
}
