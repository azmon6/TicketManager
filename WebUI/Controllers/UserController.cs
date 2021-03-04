using System.Web.Mvc;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using TicketManager.WebUI.Models;
using System.Linq;
using TicketManager.WebUI.Infrastructure.Attributes;
using TicketManager.WebUI.Infrastructure.RolesAunth;

namespace TicketManager.WebUI.Controllers
{
    public class UserController : Controller
    {
        public IUsersRepository repository;

        public UserController(IUsersRepository repo)
        {
            repository = repo;
        }

        public ActionResult ViewUsers()
        {
            return View(repository.Users);
        }

        [RolesAuthorize(Mykeys = "Admin")]
        public ActionResult EditUser(int userID)
        {
            return View(repository.GetUser(userID));
        }

        [HttpPost]
        public ActionResult EditUser(User tempUser)
        {   
            if(ModelState.IsValid)
            {
                repository.ModifyUser(tempUser);
                return RedirectToAction("ViewUsers");
            }
            return View(tempUser);
        }
        

        public ActionResult ShowProfile()
        {
            if(HttpContext.User.Identity.Name == "")
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            ProfileInfo tempInfo = new ProfileInfo();
            User tempUser = repository.GetUser(HttpContext.User.Identity.Name);
            MyRoleProvider tempRoleProvider = new MyRoleProvider();
            tempInfo.Roles = tempRoleProvider.GetRolesForUser(HttpContext.User.Identity.Name);
            tempInfo.DisplayName = tempUser.Name;
            tempInfo.UserTransactions = repository.GetUserTransactions(tempUser.UserID);
            return View(tempInfo);
        }
    }
}