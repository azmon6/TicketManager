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
            //TO DO JSON Serialization
            var temp = MyLoggingServices.GetLogs(startFrom, howMany).Select(x => new { 
                x.LogID,
                x.LogMessage,
                x.LogType,
                x.Object,
                x.Priority,
                TimeMade = x.TimeMade.ToString("dd/MM/yyyy HH:mm:ss")
            });
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllLogs()
        {
            var temp = MyLoggingServices.GetAllLogs().Select(x => new {
                x.LogID,
                x.LogMessage,
                x.LogType,
                x.Object,
                x.Priority,
                TimeMade = x.TimeMade.ToString("dd/MM/yyyy HH:mm:ss")
            });
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult ServerStats()
        {
            return View();
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult ServerLog()
        {
            //TODO REMOVE
            MyLoggingServices.TestLog();
            return View();
        }


        public JsonResult GetFinancialStat()
        {
            var temp = serverRepository.Transactions.Select(x => new { Date = x.DateMade, Money = x.PricePaid }).ToList();
            var result = temp.Select(x => new { Date = x.Date.ToString("dd/MM/yyyy"), Money = x.Money }).ToList();
            return Json(result , JsonRequestBehavior.AllowGet);
        }
    }
}