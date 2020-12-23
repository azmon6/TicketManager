using System.Web.Mvc;

namespace TicketManager.WebUI.Controllers
{
    public class CookieController : Controller
    {
        public ActionResult TicketOrder(string column = "EventTime")
        {
            //TODO Simplify this if possible
            bool asc = false;
            if (HttpContext.Request.Cookies["TicketOrderColumn"] != null)
                if (HttpContext.Request.Cookies["TicketOrderColumn"].Value == column)
                    if (HttpContext.Request.Cookies["TicketOrderAsc"] != null)
                        if (HttpContext.Request.Cookies["TicketOrderAsc"].Value == "True")
                            asc = false;
                        else
                            asc = true;
            HttpContext.Response.Cookies["TicketOrderColumn"].Value = column;
            HttpContext.Response.Cookies["TicketOrderAsc"].Value = asc.ToString();
            return Json( new { col = column , direc = asc } , JsonRequestBehavior.AllowGet);
        }
    }
}