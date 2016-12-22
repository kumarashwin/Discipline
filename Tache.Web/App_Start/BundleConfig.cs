using System.Web;
using System.Web.Optimization;

namespace Tache.Web {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include("~/Scripts/Unobtrusive/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Bootstrap/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/farbtastic").Include("~/Scripts/Farbtastic/farbtastic.js"));

            bundles.Add(new ScriptBundle("~/bundles/activities").Include("~/Scripts/Activities/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include("~/Scripts/Chart/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/icono.min.css",
                      "~/farbtastic.css",
                      "~/Content/site.css"));
        }
    }
}
