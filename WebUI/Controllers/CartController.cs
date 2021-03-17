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
        public ICartRepository cartRepository;

        public CartController(ICartRepository cart)
        {
            cartRepository = cart;
        }

        public ActionResult BuyTicket(int ticketToBuy, int tempPage = 1)
        {

            if(!this.User.Identity.IsAuthenticated)
            {
                return Json(new { redirectToUrl = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);
            }

            User currentUser = cartRepository.UserInfo.FirstOrDefault(p => p.LoginInformation.Username == HttpContext.User.Identity.Name);
            cartRepository.AddTicketToUser(currentUser.UserID, ticketToBuy);
            return new EmptyResult();
        }

        public ActionResult ShowCart()
        {
            //TODO Move Functionality to Models/Entities
            if(HttpContext.User.Identity.Name == "")
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            int currentUserID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            IQueryable<ShoppingCarts> currentCustomerCart = cartRepository.CartInfo.Where(x =>
                x.UserID == currentUserID);
            IEnumerable<CartViewInfo> cartItems = currentCustomerCart.
                Join(cartRepository.TicketInfo,
                p => p.TicketID, c => c.TicketID, (p, c) =>
                new CartViewInfo()
                {
                    TicketID = p.TicketID,
                    EventTime = c.TimeOfEvent,
                    Quantity = p.Quantity,
                    TicketPrice = c.Price,
                    TicketName = c.TicketName
                }).ToList();
            ShowCartInfo result = new ShowCartInfo();
            result.userCartItems = cartItems;
            return View(result);
        }

        [HttpPost]
        public ActionResult ShowCart(ShowCartInfo model)
        {
            return View(model);
        }

        public ActionResult GetSideCart()
        {
            if (HttpContext.User.Identity.Name == "")
            {
                IEnumerable<CartViewInfo> end = null;
                return PartialView("_GetSideCart", end );
            }
            int currentUserID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            IQueryable<ShoppingCarts> customerCart = cartRepository.CartInfo.Where(x =>
                x.UserID == currentUserID);
            IEnumerable<CartViewInfo> temp = customerCart.
                Join(cartRepository.TicketInfo,
                p => p.TicketID, c => c.TicketID, (p, c) =>
                new CartViewInfo()
                {
                    TicketID = p.TicketID,
                    EventTime = c.TimeOfEvent,
                    Quantity = p.Quantity,
                    TicketPrice = c.Price,
                    TicketName = c.TicketName
                }).ToList();
            return PartialView("_GetSideCart",temp);
        }
        
        public ActionResult RemoveLine(int ticketToRemoveID, bool ajax = false)
        {
            int customerID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            cartRepository.RemoveItemFromCart(customerID, ticketToRemoveID);
            if(ajax == true)
            {
                return new EmptyResult();
            }
            return RedirectToAction("ShowCart");
        }

        public ActionResult Checkout()
        {
            int customerID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            if (cartRepository.IsUserAllTicketsReserved(customerID))
            {
                return RedirectToAction("ShowCart");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutInfo checkInfo)
        {
            if(ModelState.IsValid)
            {
                cartRepository.CheckoutUser(cartRepository.UserInfo.First(p => p.LoginInformation.Username == HttpContext.User.Identity.Name).UserID);
                return RedirectToAction("HomeScreen","Home");
            }
            return View(checkInfo);
        }

        public ActionResult CartIcon()
        {
            return PartialView();
        }

        public ActionResult ClearCart()
        {
            int customerID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            cartRepository.ClearCart(customerID);
            return RedirectToAction("ShowCart");
        }

        public ActionResult RefreshOldCarts(string returnUrl = null)
        {
            //TODO ReturnURL FIX
            cartRepository.RefreshOldCarts();
            return RedirectToAction("HomeScreen", "Home");
        }

        public ActionResult CheckoutCheck()
        {
            int customerID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            return Json(new { redirectToUrl = Url.Action("Checkout", "Cart"), unavailableTickets = cartRepository.ReserveTickets(customerID) } , JsonRequestBehavior.AllowGet);
        }
    }
}