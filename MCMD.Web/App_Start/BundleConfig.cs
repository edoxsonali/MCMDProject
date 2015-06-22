using System.Web;
using System.Web.Optimization;

namespace MCMD.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                           "~/Scripts/bootstrap-modal.js"
                        ));


            //bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
            //            "~/Scripts/jquery-{version}.js",
            //    // needed for drag/move events in fullcalendar
            //            "~/Scripts/jquery-ui-{version}.js",
            //            "~/Scripts/bootstrap.js",
            //            "~/Scripts/bootstrap-modal.js"
            //            ));

                

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/Content/template_content/assets/scripts").Include(
                      "~/Content/template_content/assets/scripts/app.js",
                      "~/Content/template_content/assets/scripts/calendar.js",
                       "~/Content/template_content/assets/scripts/charts.js",
                       "~/Content/template_content/assets/scripts/coming-soon.js",
                       "~/Content/template_content/assets/scripts/contact-us.js",
                       "~/Content/template_content/assets/scripts/form-components.js",
                       "~/Content/template_content/assets/scripts/form-editable.js",
                       "~/Content/template_content/assets/scripts/form-fileupload.js",
                       "~/Content/template_content/assets/scripts/form-image-crop.js",
                       "~/Content/template_content/assets/scripts/form-fileupload.js",
                       "~/Content/template_content/assets/scripts/form-samples.js",
                       "~/Content/template_content/assets/scripts/form-wizard.js",
                       "~/Content/template_content/assets/scripts/gallery.js",
                       "~/Content/template_content/assets/scripts/inbox.js",
                       "~/Content/template_content/assets/scripts/index.js",
                       "~/Content/template_content/assets/scripts/lock.js",
                       "~/Content/template_content/assets/scripts/login-soft.js",
                       "~/Content/template_content/assets/scripts/login.js",
                       "~/Content/template_content/assets/scripts/maps-google.js",
                       "~/Content/template_content/assets/scripts/maps-vector.js",
                       "~/Content/template_content/assets/scripts/portlet-draggable.js",
                       "~/Content/template_content/assets/scripts/search.js",
                       "~/Content/template_content/assets/scripts/table-advanced.js",
                       "~/Content/template_content/assets/scripts/table-editable.js",
                       "~/Content/template_content/assets/scripts/table-managed.js",
                       "~/Content/template_content/assets/scripts/tasks.js",
                       "~/Content/template_content/assets/scripts/ui-general.js",
                       "~/Content/template_content/assets/scripts/ui-jqueryui.js",
                       "~/Content/template_content/assets/scripts/ui-modals.js",
                       "~/Content/template_content/assets/scripts/ui-nestable.js",
                       "~/Content/template_content/assets/scripts/ui-sliders.js",
                       "~/Content/template_content/assets/scripts/ui-tree.js"
                      ));

            bundles.Add(new ScriptBundle("~/Content/template_content/assets/plugins").Include(
                      "~/Content/template_content/assets/plugins/excanvas.min.js",
                      "~/Content/template_content/assets/plugins/jquery-1.10.1.min.js",
                      "~/Content/template_content/assets/plugins/jquery-migrate-1.2.1.min.js",
                      "~/Content/template_content/assets/plugins/jquery.blockui.min.js",
                      "~/Content/template_content/assets/plugins/jquery.bootpag.min.js",
                      "~/Content/template_content/assets/plugins/jquery.input-ip-address-control-1.0.min.js",
                      "~/Content/template_content/assets/plugins/jquery.mockjax.js",
                      "~/Content/template_content/assets/plugins/jquery.pulsate.min.js",
                      "~/Content/template_content/assets/plugins/jquery.sparkline.min.js",
                      "~/Content/template_content/assets/plugins/moment.min.js",
                      "~/Content/template_content/assets/plugins/respond.min.js"
                      ));

            //bundles.Add(new ScriptBundle("~/Content/template_content/assets/plugins/bootstrap-timepicker/js/").Include(
            //     "~/Content/template_content/assets/plugins/bootstrap-timepicker/js/bootstrap-timepicker.js"

            //       ));

            //bundles.Add(new StyleBundle("~/Content/template_content/assets/plugins/bootstrap-timepicker/compiled/css").Include(
            //      "~/Content/template_content/assets/plugins/bootstrap-timepicker/compiled/timepicker.css"       

            //        ));

            //bundles.Add(new ScriptBundle("~/Content/template_content/assets/plugins/clockface/js/").Include(
            //  "~/Content/template_content/assets/plugins/clockface/js/clockface.js"

            //    ));

            //bundles.Add(new StyleBundle("~/Content/template_content/assets/plugins/bootstrap-timepicker/compiled/css").Include(
            //      "~/Content/template_content/assets/plugins/bootstrap-timepicker/compiled/timepicker.css"

            //        ));



            //     bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/template_content/assets/css").Include("~/Content/template_content/assets/css/style.css"));

            //   bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/theme/css/style.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/template_content/assets/css/css").Include(
                      "~/Content/template_content/assets/css/style-responsive.css",
                      "~/Content/template_content/assets/css/style-non-responsive.css",
                      "~/Content/template_content/assets/css/style-metro.css",
                      "~/Content/template_content/assets/css/print.css",
                      "~/Content/template_content/assets/css/animate.css"));

            bundles.Add(new StyleBundle("~/Content/template_content/assets/css/pages/css").Include(
                     "~/Content/template_content/assets/css/pages/about-us.css",
                     "~/Content/template_content/assets/css/pages/blog.css",
                     "~/Content/template_content/assets/css/pages/coming-soon.css",
                     "~/Content/template_content/assets/css/pages/email.css",
                     "~/Content/template_content/assets/css/pages/error.css",
                       "~/Content/template_content/assets/css/pages/image-crop.css",
                       "~/Content/template_content/assets/css/pages/inbox.css",
                       "~/Content/template_content/assets/css/pages/invoice.css",
                       "~/Content/template_content/assets/css/pages/lock.css",
                       "~/Content/template_content/assets/css/pages/login-soft.css",
                       "~/Content/template_content/assets/css/pages/login.css",
                       "~/Content/template_content/assets/css/pages/pricing-tables.css",
                       "~/Content/template_content/assets/css/pages/profile.css",
                       "~/Content/template_content/assets/css/pages/promo.css",
                       "~/Content/template_content/assets/css/pages/search.css",
                       "~/Content/template_content/assets/css/pages/tasks.css",
                       "~/Content/template_content/assets/css/pages/timeline.css"
                     ));



        }
    }
}
