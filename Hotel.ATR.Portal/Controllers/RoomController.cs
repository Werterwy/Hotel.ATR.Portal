using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.ATR.Portal.Controllers
{
    public class RoomController : Controller
    {
        private IWebHostEnvironment webHost;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IWebHostEnvironment webHost, ILogger<RoomController> logger)
        {
            this.webHost = webHost;
            this._logger = logger;
        }

        [Authorize]
        public IActionResult Index(int page, int counter)
        {

            _logger.LogInformation("logging Information");
            _logger.LogCritical("Logging Critical");
            _logger.LogDebug("Logging Debug");
            _logger.LogError("Logging Error");

            var user = new User() { email = "ok@ok.kz", name = "yevgeniy" };

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
