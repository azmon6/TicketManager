using System.Web.Mvc;
using TicketManager.WebUI.Models;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System.Linq;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ITicketRepository repository;

        public HomeController(ITicketRepository ticketRepository)
        {
            repository = ticketRepository;
        }

        public ActionResult ShowTickets()
        {
            return View(repository.Tickets);
        }

        public ViewResult NewTicket(int tickID)
        {
            Ticket tick = repository.Tickets.FirstOrDefault(p => p.TicketID == tickID);
            return View(tick);
        }

        [HttpPost]
        public ActionResult NewTicket(Ticket tick)
        {
            if (ModelState.IsValid)
            {
                repository.SaveTicket(tick);
                return RedirectToAction("ShowTickets");
            }
            return View(tick);
        }

        public ViewResult Create()
        {
            return View("NewTicket", new Ticket());
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteTicket(int tickID)
        {
            repository.DeleteTicket(tickID);
            return RedirectToAction("ShowTickets");
        }

        public ActionResult NotFound()
        {
            return View();
        }

    }
}