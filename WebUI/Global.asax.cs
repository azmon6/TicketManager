using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using TicketManager.Domain.Concrete;
using System.Web.Optimization;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<EntityContext>(null);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
