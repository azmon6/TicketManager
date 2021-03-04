using TicketManager.WebUI.Infrastructure.Attributes;
using System.Web.Mvc;
using TicketManager.Domain.Abstract;
using TicketManager.Logging;
using System.Linq;
using Newtonsoft.Json;

namespace TicketManager.WebUI.Controllers
{
    public class ServerController : Controller
    {
        public IServerRepository serverRepository;

        public ServerController(IServerRepository serve)
        {
            serverRepository = serve;
        }


        public JsonResult GetLogs(int startFrom = 0, int howMany = 1)
        {
            var temp = MyLoggingServices.GetLogs(startFrom, howMany).Select(x => new { 
                LogID = x.LogID,
                LogMessage = x.LogMessage,
                LogType = x.LogType,
                Object = x.Object,
                Priority = x.Priority,
                TimeMade = x.TimeMade.ToString("dd/MM/yyyy HH:mm:ss")
            });
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllLogs()
        {
            return Json(MyLoggingServices.GetAllLogs(), JsonRequestBehavior.AllowGet);
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult ServerStats()
        {
            return View();
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult ServerLog()
        {
            MyLoggingServices.TestLog();
            return View();
        }


        public JsonResult GetFinancialStat()
        {
            return Json(serverRepository.GetMoney().Select(x => new { Date = x.DateMade, Money = x.PricePaid }).ToList() , JsonRequestBehavior.AllowGet);
        }
    }
}