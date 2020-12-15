using System.Web.Mvc;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Abstract;
using TicketManager.WebUI.Models;
using System.Linq;
using TicketManager.WebUI.Infrastructure.Attributes;

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
            PageSize = 2;
        }

        public ActionResult ShowTickets(int page = 1)
        {
            TicketShowModel temp = new TicketShowModel()
            {
                Tickets = repository.GetSpecificPage(PageSize, page),
                PageNow = page,
                TotalPages = (int)System.Math.Ceiling( (decimal)((decimal)repository.GetSize() / (decimal)PageSize))
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
                repository.SaveTicket(tick.GetTicket());
                return RedirectToAction("ShowTickets");
            }
            return View(tick);
        }

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
    }
}