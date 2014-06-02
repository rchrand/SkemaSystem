using System.Web;
using System.Web.Optimization;

namespace SkemaSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/jquery-{version}.js",
                                                                      "~/Scripts/jquery.validate*",
                                                                      "~/Scripts/modernizr-*",
                                                                      "~/Scripts/jquery.sticky.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/backend").Include("~/Scripts/jquery.ui.position.js",
                                                                      "~/Scripts/jquery.contextMenu.js"));

            bundles.Add(new LessBundle("~/Content/less").Include("~/Content/*.less"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
