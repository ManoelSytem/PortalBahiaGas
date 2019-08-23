using System.Web;
using System.Web.Optimization;

namespace PortalBahiaGas
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.masknumber.js",
                        "~/Scripts/jquery.maskMoney.js",
                        "~/Scripts/jquery.maskedinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/locales/bootstrap-datetimepicker.pt-BR.js",
                      "~/Scripts/locales/lazyload.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/modalazyload.css"));
        }
    }
}
