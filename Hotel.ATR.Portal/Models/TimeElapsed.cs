using Hotel.ATR.Portal.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Hotel.ATR.Portal.Models
{
    public class TimeElapsed : Attribute, IActionFilter
    {


        private Stopwatch stopwatch;

        private readonly ILogger<TimeElapsed> _logger;

        public TimeElapsed(ILogger<TimeElapsed> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();

            string action = "Index";

            string timeElipsed = stopwatch.ElapsedMilliseconds.ToString();

            _logger.LogInformation("окрытие страницы {action} произошло за {timeElipsed}", 
                action, timeElipsed);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = Stopwatch.StartNew();
        }

    }
}
