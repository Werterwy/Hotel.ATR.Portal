using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Globalization;
using System.Linq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotel.ATR.Portal.Controllers
{
   // [Route("main")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*private HttpContextAccessor _httpContext;*/

        public HomeController(ILogger<HomeController> logger/*, HttpContextAccessor httpContextAccessor *//*IRepository repo*/)
        {
            _logger = logger;
        /*    _httpContext = httpContextAccessor;*/

        }

        public IActionResult Index()
        {

            HttpContext.Session.SetString("Iin", "031127000066");
            //var data2 = _httpContext.HttpContext.Request.Cookies["Iin"];


            var sessionData = HttpContext.Session.GetString("Iin");


            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.Now.AddSeconds(100);
            Response.Cookies.Append("Iin", "031127000066", options);

            string value = Request.Cookies["Iin"];

            User user = new User();
            user.email = "ok@ok.com";

            _logger.LogError("У пользователя {email} возникла ошибка {errorMessage}", user.email, "Ошибка пользователя");


            Stopwatch sw = new Stopwatch();

            sw.Start();
            /// вызов чужого сервиса
            Thread.Sleep(1000);

            sw.Stop();

            //var data = sw.ElapsedMilliseconds;

            _logger.LogInformation("Сервис отработал за {ElapsedMilliseconds}", sw.ElapsedMilliseconds);

            _logger.LogInformation("logging Information");
            _logger.LogCritical("Logging Critical");
            _logger.LogDebug("Logging Debug");
            _logger.LogError("Logging Error");

            //var log = new LoggerConfiguration().WriteTo.Email(
            //    fromEmail: "app@example.com",
            //    toEmail: "support@example.com",
            //    mailServer: "smtp.example.com").CreateLogger();



            return View();
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password, string ReturnUrl)
        {

            if(login == "admin" &&  password == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));


                if(string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(ReturnUrl);
                }

               /* Task.Delay(100).Wait();
                return Redirect(ReturnUrl);*/
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}