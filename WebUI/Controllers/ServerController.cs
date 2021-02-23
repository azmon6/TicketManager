using TicketManager.WebUI.Infrastructure.Attributes;
using System.Web.Mvc;
using TicketManager.Domain.Abstract;
using System.Linq;

namespace TicketManager.WebUI.Controllers
{
    public class ServerController : Controller
    {

        public IServerRepository serverRepository;

        public ServerController(IServerRepository serve)
        {
            serverRepository = serve;
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult ServerStats()
        {
            return View();
        }


        public JsonResult GetFinancialStat()
        {
            return Json(serverRepository.GetMoney().Select(x => new { Date = x.DateMade, Money = x.PricePaid }).ToList() , JsonRequestBehavior.AllowGet);
        }
    }
}