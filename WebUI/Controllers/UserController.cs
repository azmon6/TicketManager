using System.Web.Mvc;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System.Linq;
using TicketManager.WebUI.Infrastructure.Attributes;

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
            User temp = repository.Users.FirstOrDefault(p => p.UserID == userID);
            return View(temp);
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
        
    }
}