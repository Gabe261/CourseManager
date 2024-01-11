using Microsoft.AspNetCore.Mvc;

namespace CourseManager.Controllers
{
    /// <summary>
    /// Base controller to handle the cookies
    /// </summary>
    public class BaseController : Controller
    {
        public void CookiesMsgUpdate()
        {
            const string COOKIEKEY = "firstVisitDate";

            var firstVisitDate = HttpContext.Request.Cookies.ContainsKey(COOKIEKEY) &&
                DateTime.TryParse(HttpContext.Request.Cookies[COOKIEKEY], out var parsedDate)
                ? parsedDate : DateTime.Now;

            var welcomeMessage = HttpContext.Request.Cookies.ContainsKey(COOKIEKEY)
               ? $"Welcome back! You first used this app on {firstVisitDate.ToShortDateString()}"
               : "Hey, Welcome to the Event Manager App";

            var co = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(3)
            };

            HttpContext.Response.Cookies.Append(COOKIEKEY, firstVisitDate.ToString(), co);

            ViewData["WelcomeMessage"] = welcomeMessage;
        }
    }
}
