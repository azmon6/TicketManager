using System.Web.Mvc;

namespace TicketManager.WebUI.Controllers
{
    public class CookieController : Controller
    {
        public string TicketOrder(string column = "EventTime", bool asc =true)
        {
            HttpContext.Response.Cookies["TicketOrderColumn"].Value = column;
            HttpContext.Response.Cookies["TicketOrderAsc"].Value = asc.ToString();
            return column;
        }
    }
}