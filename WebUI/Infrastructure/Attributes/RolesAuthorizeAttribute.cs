
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            //TODO This causes an error which prevents entering page
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result != null)
            {

                var temp = filterContext.HttpContext.User.Identity;

                if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "action", "Login" },
                            { "controller", "Account" }
                        }
                    );
                }
            }
        }
    }
}