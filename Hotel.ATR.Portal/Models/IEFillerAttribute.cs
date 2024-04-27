using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace Hotel.ATR.Portal.Models
{
    public class IEFillerAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();

            if(Regex.IsMatch(userAgent, "Mothilla"))
            {
                context.Result = new ContentResult
                {
                    Content = "Ваш браузер не поддерживает"
                };
            }
        }
    }
}
