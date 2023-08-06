using DongHoShop.Common;
using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Assets/client/js/jquery.min.js"));

            //bundles.Add(new ScriptBundle("~/js/plugins").Include(
            //    "~/Assets/admin/libs/jquery-ui/jquery-ui.js",
            //    "~/Assets/client/mustache/mustache.js",
            //    "~/Assets/client/numeral/numeral.min.js",
            //    "~/Assets/client/jquery-validation/dist/jquery.validate.js",
            //    "~/Assets/client/jquery-validation/dist/additional-methods.js"
            //    //"~/Assets/client/js/sweetalert.min.js",
            //    //"~/Assets/client/js/common.js"
            //   ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/admin/libs/jquery-ui/jquery-ui.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/custom.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/font/font.css")
                );
            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));
        }
    }
}
