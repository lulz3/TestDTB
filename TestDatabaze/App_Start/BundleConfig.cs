using System.Web;
using System.Web.Optimization;

namespace TestDatabaze
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* First Way */
            //bundles.Add(new StyleBundle("~/Content/css").Include("~/assets/css/bootstrap.css", "~/assets/css/font-awesome.css", "~/assets/css/style.css", "~/assets/js/dataTables/dataTables.bootstrap.css"));
            /* Second Way */
            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/assets/css", "*.css").Include("~/assets/js/dataTables/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/Content/jquery").IncludeDirectory("~/assets/js", "*.js", true));

            //bundles.Add(new ScriptBundle("~/Content/jquery").Include("~/Scripts/jquery-{version}.js"));
            //bundles.Add(new ScriptBundle("~/Content/modernizr").Include("~/Scripts/modernizr-*"));
            //bundles.Add(new ScriptBundle("~/Content/jqueryval").Include("~/Scripts/jquery.validate*"));




            /*
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
           */
        }
    }
}
