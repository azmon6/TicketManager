

using System.Linq;
using TicketManager.Domain.Concrete;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Infrastructure.RolesAunth
{
    public class RolePermissionManager
    {
        private EntityContext context = new EntityContext();
        public RolePermissionManager() { }

        //Using string[] even tho Users have one Role
        //for future easily adding feature
        public string[] ResolveRoleName(string loginName)
        {
            User foundUser = context.Users.FirstOrDefault(x => context.LoginInformations.Where(y => (y.LoginID == x.LoginID)).ToList().Count() == 1);
            if(foundUser == null)
            {
                return new string[0];
            }

            return new string[] { foundUser.Role };
        }
    }
}