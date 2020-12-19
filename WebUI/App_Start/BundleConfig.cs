using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/HeadStyle.css",
                "~/Content/MyStyles.css", "~/Content/bootstrap.min.css"));
        }
    }
}