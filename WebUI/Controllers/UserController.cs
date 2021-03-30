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
        public ActionResult EditUser(int userToEdit)
        {
            return View(repository.GetUser(userToEdit));
        }

        [HttpPost]
        public ActionResult EditUser(User userToEdit)
        {   
            if(ModelState.IsValid)
            {
                repository.ModifyUser(userToEdit);
                return RedirectToAction("ViewUsers");
            }
            return View(userToEdit);
        }
        

        public ActionResult ShowProfile()
        {
            if(HttpContext.User.Identity.Name == "")
            {
                return RedirectToAction("HomeScreen", "Home");
            }
            ProfileInfo userProfile = new ProfileInfo();
            User currentUser = repository.GetUser(HttpContext.User.Identity.Name);
            MyRoleProvider tempRoleProvider = new MyRoleProvider();
            userProfile.Roles = tempRoleProvider.GetRolesForUser(HttpContext.User.Identity.Name);
            userProfile.DisplayName = currentUser.Name;
            userProfile.UserTransactions = repository.GetUserTransactions(currentUser.UserID);
            userProfile.UserID = currentUser.UserID;
            return View(userProfile);
        }
    }
}