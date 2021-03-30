using System.Web.Mvc;
using TicketManager.WebUI.Infrastructure.RolesAunth;

namespace TicketManager.WebUI.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult TestUserRole(string tempName)
        {
            MyRoleProvider tempRoleProvider = new MyRoleProvider();
            return PartialView(tempRoleProvider.GetRolesForUser(tempName));
        }
    }
}