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
            double test = 50.0;
            test = test / 0;
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
    }
}