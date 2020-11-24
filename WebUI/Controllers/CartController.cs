using System.Web.Mvc;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Abstract;
using System.Linq;
using System.Collections.Generic;
using TicketManager.WebUI.Models;

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
            int tempID = usersRepository.Users.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            // ASK IEnumerable vs IQueryable here
            IQueryable<UserCartInformation> tempInfo = cartRepository.CartInformation.Where(x =>
                x.UserID == tempID);
            IEnumerable<CartViewInfo> temp = tempInfo.
                Join(cartRepository.TicketInfo,
                p => p.TicketID, c => c.TicketID, (p, c) =>
                new CartViewInfo()
                {
                    TicketID = p.TicketID,
                    EventTime = c.EventTime,
                    Quantity = p.Quantity,  
                    TicketPrice = c.Price,
                    TicketName = c.TicketName
                });
            return View(temp);
        }

        public ActionResult RemoveLine(int tickId)
        {
            int tempID = usersRepository.Users.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            cartRepository.RemoveItemFromCart( tempID , tickId);
            return RedirectToAction("ShowCart");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutInfo tempInfo)
        {
            if(ModelState.IsValid)
            {
                cartRepository.CheckoutUser(usersRepository.Users.First(p => p.LoginInformation.Username == HttpContext.User.Identity.Name).UserID);
                return RedirectToAction("HomeScreen","Home");
            }
            return View(tempInfo);
        }
    }
}