using System.Web.Mvc;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Abstract;
using System.Linq;
using TicketManager.WebUI.Infrastructure.Attributes;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        public ITicketRepository repository;

        public AdminController(ITicketRepository ticketRepository)
        {
            repository = ticketRepository;
        }

        // TODO Add pagination
        public ActionResult ShowTickets()
        {
            return View(repository.Tickets);
        }

        [RolesAuthorize(Mykeys ="Admin")]
        public ViewResult ModifyTicket(int tickID)
        {
            Ticket tick = repository.Tickets.FirstOrDefault(p => p.TicketID == tickID);
            return View(tick);
        }

        [HttpPost]
        public ActionResult ModifyTicket(Ticket tick)
        {
            if (ModelState.IsValid)
            {
                repository.SaveTicket(tick);
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
        public ViewResult Create()
        {
            return View("ModifyTicket", new Ticket());
        }
    }
}