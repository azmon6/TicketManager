using System.Web.Mvc;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Abstract;
using System.Linq;

namespace TicketManager.WebUI.Controllers
{
    public class CartController : Controller
    {

        public ITicketRepository tickRepository;
        public IUsersRepository usersRepository;
        public ICartRepository cartRepository;

        public CartController(ITicketRepository ticket, IUsersRepository users, ICartRepository cart)
        {
            tickRepository = ticket;
            usersRepository = users;
            cartRepository = cart;
        }

        public ActionResult BuyTicket(int tickID)
        {
            if(!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            User tempUser = usersRepository.Users.FirstOrDefault(p => p.LoginInformation.Username == HttpContext.User.Identity.Name);
            cartRepository.AddTicketToUser(tempUser.UserID, tickID);
            return RedirectToAction("ShowTickets", "Admin");
        }

        public ActionResult ShowCart()
        {
            return View(cartRepository.CartInformation);
        }
    }
}