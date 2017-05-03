using System.Web;
using System.Web.Optimization;

namespace Cesgranrio.CorretorDeProvas.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",/*fundamental para ajax do XPagedList*/
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/corretordeprovas.validate*",/*validação de pontuação*/
                        "~/Scripts/corretordeprovas.util.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //lightbox para imagens
            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                        "~/Scripts/lightbox-2.6.js"));
            bundles.Add(new StyleBundle("~/Content/lightbox").Include(
                        "~/Content/lightbox.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/tema/bootstrap-checkbox-radio-switch.js",
                      "~/Scripts/tema/bootstrap-notify.js",
                      "~/Scripts/tema/light-bootstrap-dashboard.js"
                      )
            );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/tema/animate.css",/*tema*/
                      "~/Content/pagedlist.css",/*paginador*/
                      "~/Content/tema/light-bootstrap-dashboard.css",/*tema*/
                      "~/Content/site.css"));
        }
    }
}
