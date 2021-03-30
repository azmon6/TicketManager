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

        public ActionResult GetTicketTable(int page = 1)
        {
            if (HttpContext.Request.Cookies["TicketOrderColumn"] == null || HttpContext.Request.Cookies["TicketOrderAsc"] == null)
            {
                HttpContext.Response.Cookies["TicketOrderColumn"].Value = "EventTime";
                HttpContext.Response.Cookies["TicketOrderAsc"].Value = "True";
            }
            TicketShowModel ticketShowModel = new TicketShowModel()
            {
                Tickets = repository.GetSpecificPage(PageSize, page,
                    HttpContext.Request.Cookies["TicketOrderColumn"].Value , HttpContext.Request.Cookies["TicketOrderAsc"].Value),
                PageNow = page,
                TotalPages = (int)System.Math.Ceiling((decimal)((decimal)repository.GetSize() / (decimal)PageSize))
            };
            return PartialView( "_GetTicketTable" , ticketShowModel);
        }       

        public ActionResult ShowTickets(int page = 1)
        {
            TicketShowModel ticketShowModel = new TicketShowModel()
            {
                Tickets = null,
                PageNow = page,
                TotalPages = (int)System.Math.Ceiling((decimal)((decimal)repository.GetSize() / (decimal)PageSize))
            };
            return View(ticketShowModel);
        }

        [RolesAuthorize(Mykeys ="Admin")]
        public ViewResult ModifyTicket(int ticketToModifyID = 0)
        {
            Ticket tick = repository.Tickets.FirstOrDefault(p => p.TicketID == ticketToModifyID);
            return View(new ModifyTicketModel(tick));
        }

        [HttpPost]
        public ActionResult ModifyTicket(ModifyTicketModel tick)
        {
            if(tick.StartingDateAvailable > tick.EndDateAvailable)
            {
                ModelState.AddModelError("EventEndBuyDate", "End of Buy Time must be after Start of Buy Time. ");
            }

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
        public ActionResult DeleteTicket(int ticketToDeleteID)
        {
            repository.DeleteTicket(ticketToDeleteID);
            return RedirectToAction("ShowTickets");
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult Create()
        {
            return RedirectToAction("ModifyTicket", new { tickId = 0 });
        }

        public ActionResult SingleTicket(int ID = 0)
        {
            if(ID <= 0)
            {
                return RedirectToAction("ShowTickets");
            }

            Ticket tick = repository.Tickets.FirstOrDefault(p => p.TicketID == ID);

            ShowTicketModel ticketToShow = new ShowTicketModel();
            ticketToShow.TicketToShow = tick;
            ticketToShow.OtherTicketsByOrg = repository.Tickets.Where(x => (x.Organizer == tick.Organizer && x.TicketID != ID));

            return View("ShowTicket",ticketToShow);
        }
    }
}