using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Hotel.ATR.Portal.Controllers
{
    public class RoomController : Controller
    {
        private IWebHostEnvironment webHost;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IWebHostEnvironment webHost, ILogger<RoomController> logger)
        {
            this.webHost = webHost;
            _logger = logger;
        }

        //[Authorize]
        public async Task<IActionResult> Index(int page, int counter)
        {

            List<Room> rooms = new List<Room>();

            string Jwt = GenerateJSONWebToken();


            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt);
                using (var responce = await httpClient
                    .GetAsync("http://localhost:5031/api/Room"))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();

                    rooms = JsonConvert.DeserializeObject<List<Room>>(apiResponce);
                }
            }

            _logger.LogInformation("logging Information");
            _logger.LogCritical("Logging Critical");
            _logger.LogDebug("Logging Debug");
            _logger.LogError("Logging Error");

           /* var user = new User() { email = "ok@ok.kz", name = "yevgeniy" };

            ViewBag.User = user;
            ViewData["user"]= user;
            TempData["user"] = user;*/


            return View();
        }


        public IActionResult RenderPartial()
        {
            return View();
        }

        public IActionResult RoomList()
        {
            List<Room> data = null;

            return View(data);
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

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4c53ce9de0ab7c9ce2f72f2b1447aa73"));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "John Doc",
                audience: "1516239022",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
