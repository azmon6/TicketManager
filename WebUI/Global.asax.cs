using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using TicketManager.Domain.Concrete;
using System.Web.Optimization;
using TicketManager.Logging;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<EntityContext>(null);
            Database.SetInitializer<MyLoggingServices>(null);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
