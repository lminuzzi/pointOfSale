using System.Web;
using System.Web.Optimization;

namespace PointOfSale
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymoneymask").Include(
                        "~/Scripts/jquery.moneymask.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerytablesorter").Include(
                        "~/Scripts/tablesorter/jquery.tablesorter.combined.js"));

            bundles.Add(new ScriptBundle("~/bundles/cart").Include(
                        "~/Scripts/Order/cart.js"));

            bundles.Add(new StyleBundle("~/Content/cart").Include(
                      "~/Content/Order/cart.css"));

            bundles.Add(new StyleBundle("~/Content/carousel").Include(
                      "~/Content/carousel.css"));
        }
    }
}
