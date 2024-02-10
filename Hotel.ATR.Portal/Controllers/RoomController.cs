using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR.Portal.Controllers
{
    public class RoomController : Controller
    {
        private IWebHostEnvironment webHost;

        public RoomController(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        public IActionResult Index(int page, int counter)
        {
            var user = new User() { email = "reter@.kz", name = "orazbay" };

            ViewBag.User = user;
            ViewData["user"]= user;
            TempData["user"] = user;


            return View(user);
        }

        public IActionResult RenderPartial()
        {
            return View();
        }

        public IActionResult RoomList()
        {
            return View();
        }

        public IActionResult RoomDetails()
        {
            return View();
        }

/*User user*/
        [HttpPost]
        public IActionResult SubcribeNewsletter(IFormFile userFile)   
        {
            var data = Request.Form["email"];

            string path = Path.Combine(webHost.WebRootPath, userFile.FileName);
            using(var stream = new FileStream(path, FileMode.Create))
            {
                userFile.CopyTo(stream);

            }
           // return View("Index");

            return RedirectToAction("Index");

        }
    }
}
