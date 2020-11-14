using System.Web.Mvc;
using TicketManager.Domain.Entities;
using System.Linq;

namespace TicketManager.WebUI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult HomeScreen()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

    }
}