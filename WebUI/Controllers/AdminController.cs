using System.Web.Mvc;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Abstract;
using TicketManager.WebUI.Models;
using System.Linq;
using TicketManager.WebUI.Infrastructure.Attributes;
using TicketManager.Logging;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        public ITicketRepository repository;

        private int _pageSize = 5;
        public int PageSize { 
            get { return _pageSize; }
            set
            {
                if (value < 1 || value > 50)
                    _pageSize = 5;
                else
                    _pageSize = value;
            }
        }

        public AdminController(ITicketRepository ticketRepository)
        {
            repository = ticketRepository;
            PageSize = 5;
        }

        //TODO Make it work with data in html tags
        public ActionResult GetTicketTable(int page = 1)
        {
            if (HttpContext.Request.Cookies["TicketOrderColumn"] == null || HttpContext.Request.Cookies["TicketOrderAsc"] == null)
            {
                HttpContext.Response.Cookies["TicketOrderColumn"].Value = "EventTime";
                HttpContext.Response.Cookies["TicketOrderAsc"].Value = "True";
            }
            TicketShowModel temp = new TicketShowModel()
            {
                Tickets = repository.GetSpecificPage(PageSize, page,
                    HttpContext.Request.Cookies["TicketOrderColumn"].Value , HttpContext.Request.Cookies["TicketOrderAsc"].Value),
                PageNow = page,
                TotalPages = (int)System.Math.Ceiling((decimal)((decimal)repository.GetSize() / (decimal)PageSize))
            };
            return PartialView( "_GetTicketTable" ,temp);
        }       

        public ActionResult ShowTickets(int page = 1)
        {
            TicketShowModel temp = new TicketShowModel()
            {
                Tickets = null,
                PageNow = page,
                TotalPages = (int)System.Math.Ceiling((decimal)((decimal)repository.GetSize() / (decimal)PageSize))
            };
            return View(temp);
        }

        [RolesAuthorize(Mykeys ="Admin")]
        public ViewResult ModifyTicket(int tickID)
        {
            Ticket tick = repository.Tickets.FirstOrDefault(p => p.TicketID == tickID);
            ModifyTicketModel temp = new ModifyTicketModel(tick);
            return View(temp);
        }

        [HttpPost]
        public ActionResult ModifyTicket(ModifyTicketModel tick)
        {
            if (ModelState.IsValid)
            {
                var tempTick = tick.GetTicket();
                if (!repository.SaveTicket(tempTick))
                {
                    tick.RowVersion = tempTick.RowVersion;
                    TempData["TicketModifiedError"] = "The ticket has been modified since you started editing it";
                    return View(tick);
                }
                return RedirectToAction("ShowTickets");
            }
            return View(tick);
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult DeleteTicket(int tickID)
        {
            repository.DeleteTicket(tickID);
            return RedirectToAction("ShowTickets");
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult Create()
        {
            return RedirectToAction("ModifyTicket", new { tickId = 0 });
        }

        public ActionResult SingleTicket(int id = 0)
        {
            if(id <= 0)
            {
                return RedirectToAction("ShowTickets");
            }

            return View("ShowTicket");
        }
    }
}