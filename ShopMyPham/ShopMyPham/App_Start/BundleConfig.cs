using System.Web;
using System.Web.Optimization;

namespace ShopMyPham
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
            /////////////////////////
            //css
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/css").Include(
                        "~/Areas/Admin/Assets/vendors/bootstrap/dist/css/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/font-awesome/css").Include(
                        "~/Areas/Admin/Assets/vendors/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/build/css").Include(
                        "~/Areas/Admin/Assets/build/css/custom.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable/css").Include(
                        "~/Areas/Admin/Assets/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css"));
            //js
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                      "~/Areas/Admin/Assets/vendors/jquery/dist/jquery.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap/js").Include(
                      "~/Areas/Admin/Assets/vendors/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/build/js").Include(
                      "~/Areas/Admin/Assets/build/js/custom.min.js"));
        }
    }
}
