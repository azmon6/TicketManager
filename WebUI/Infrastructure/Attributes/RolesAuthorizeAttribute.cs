using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TicketManager.WebUI.Infrastructure.RolesAunth;

namespace TicketManager.WebUI.Infrastructure.Attributes
{
    public class RolesAuthorizeAttribute : AuthorizeAttribute
    {
        private string _myKeys = "";
        public string Mykeys
        {
            get { return _myKeys; }
            set
            {
                Roles = value;
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //TODO Split Roles so it works for multiple
            if(httpContext.User.IsInRole("Admin"))
            {
                return true;
            }    
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result != null)
            {

                if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "action", "Login" },
                            { "controller", "Account" },
                            { "returnUrl", filterContext.HttpContext.Request.Url.ToString() }
                        }
                    );
                    filterContext.Controller.TempData["error"] = "You don't have permission.";
                }
            }
        }
    }
}