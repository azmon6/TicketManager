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

        public ActionResult BuyTicket(int tickID, int tempPage = 1)
        {

            if(!this.User.Identity.IsAuthenticated)
            {
                return Json(new { redirectToUrl = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);
            }

            User tempUser = cartRepository.UserInfo.FirstOrDefault(p => p.LoginInformation.Username == HttpContext.User.Identity.Name);
            cartRepository.AddTicketToUser(tempUser.UserID, tickID);
            return new EmptyResult();
        }

        public ActionResult ShowCart()
        {
            //TODO Move Functionality to Models/Entities
            if(HttpContext.User.Identity.Name == "")
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            int tempID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
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
                }).ToList();
            return View(temp);
        }

        public ActionResult GetSideCart()
        {
            if (HttpContext.User.Identity.Name == "")
            {
                IEnumerable<CartViewInfo> end = null;
                return PartialView("_GetSideCart", end );
            }
            int tempID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
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
                }).ToList();
            return PartialView("_GetSideCart",temp);
        }
        
        public ActionResult RemoveLine(int tickId, bool ajax = false)
        {
            int tempID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            cartRepository.RemoveItemFromCart( tempID , tickId);
            if(ajax == true)
            {
                return new EmptyResult();
            }
            return RedirectToAction("ShowCart");
        }

        public ActionResult Checkout()
        {

            int tempID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            IEnumerable<UserCartInformation> tempInfo = cartRepository.CartInformation.Where(x =>
                x.UserID == tempID).ToList();

            //TODO Move to Repository
            bool isEverythingAvailable = true;
            string returnedMessage = "";
            string resultMessage = "";
            foreach(var tempLine in tempInfo)
            {
                returnedMessage = cartRepository.SubtractTicket(tempLine);
                if(returnedMessage != "")
                {
                    resultMessage = resultMessage + returnedMessage;
                    isEverythingAvailable = false;
                }
            }

            if(!isEverythingAvailable)
            {
                TempData["UnavailableTicketMessage"] = resultMessage;
                return RedirectToAction("ShowCart");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutInfo tempInfo)
        {
            if(ModelState.IsValid)
            {
                cartRepository.CheckoutUser(cartRepository.UserInfo.First(p => p.LoginInformation.Username == HttpContext.User.Identity.Name).UserID);
                return RedirectToAction("HomeScreen","Home");
            }
            return View(tempInfo);
        }

        public ActionResult CartIcon()
        {
            return PartialView();
        }

        public ActionResult ClearCart()
        {
            int tempID = cartRepository.UserInfo.First(x => x.LoginInformation.Username == HttpContext.User.Identity.Name).UserID;
            cartRepository.ClearCart(tempID);
            return RedirectToAction("ShowCart");
        }

        public ActionResult RefreshOldCarts(string returnUrl = null)
        {
            cartRepository.RefreshOldCarts();
            return RedirectToAction("HomeScreen", "Home");
        }
    }
}