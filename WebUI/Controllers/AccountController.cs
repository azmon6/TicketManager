using System.Web.Mvc;
using System.Web;
using TicketManager.WebUI.Models;
using TicketManager.Domain.Abstract;
using System.Web.Security;

namespace TicketManager.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IUsersRepository repository;

        public AccountController(IUsersRepository rep)
        {
            repository = rep;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpInformation Info)
        {
            if (ModelState.IsValid)
            {
                if(repository.AddUser(Info.getUser(), Info.GetLogin()))
                {
                    FormsAuthentication.SetAuthCookie(Info.GetLogin().Username, false);
                    return RedirectToAction("HomeScreen", "Home");
                }
                else
                {
                    ModelState.AddModelError("Username", "This username already exists!");
                    return View(Info);
                }
            }
            return View(Info);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo info, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if(repository.IsValidUser(info.Username,info.Password))
                {
                    FormsAuthentication.SetAuthCookie(info.Username, false);
                    return Redirect(returnUrl ?? Url.Action("HomeScreen", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("HomeScreen", "Home"));
        }

    }
}