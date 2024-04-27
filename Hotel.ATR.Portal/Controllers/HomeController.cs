using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Globalization;
using System.Linq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hotel.ATR.Portal.Controllers
{
    // [Route("main")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*private HttpContextAccessor _httpContext;*/
        private readonly IStringLocalizer<HomeController> _local;

        public HomeController(ILogger<HomeController> logger/*, HttpContextAccessor httpContext*/, IStringLocalizer<HomeController> local)
        {
            _logger = logger;
        /*    _httpContext = httpContext;*/
            _local = local;
        }

        /*        [Route("IndexNew")]*/

        //[IEFillerAttribute]
        /* [ServiceFilter(typeof(TimeElapsed))]*/
   /*     [ServiceFilter(typeof(CatchError))]*/
        public IActionResult Index(string culture, string cultureIU)
        {

            //throw new Exception("моя ошибка");

            Thread.Sleep(100);
            // Executing 

            ViewBag.AboutUs = _local["aboutus"];

            GetCulture(culture);

            if (!string.IsNullOrWhiteSpace(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);

            }

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

            // Executed
        }

        public IActionResult AboutUs()
        {

            string key = "IIN";
            string value = "880111300392";

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append(key, value);
            Response.Cookies.Append("key_2", value);
            Response.Cookies.Append("key_3", value);



            return View();
        }

        public IActionResult Errort()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            // Executing
            return View();

            //Executed
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



        [HttpPost]
        public async Task<IActionResult> Login(string login, string password, string ReturnUrl)
        {

            if (login == "admin" && password == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(claimsIdentity));

               // HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (string.IsNullOrEmpty(ReturnUrl))
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
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public string GetCulture(string code = "")
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);

                ViewBag.Culture = string.Format("CurrentCulture: {0}, CurrentUICulture: {1}", CultureInfo.CurrentCulture,
                    CultureInfo.CurrentUICulture);
            }
            return "";
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetUser()
        {
            User user = new User();
            user.email = "gersen.e.a@gmail.com";
            user.name = "Yevgeniy Gersen";

            return Json(user);
        }
    }
}