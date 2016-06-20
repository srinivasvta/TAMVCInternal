using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Optimization;


namespace TA.Classified.Web.App_Start
{
    public class Bundleconfig
    {

              public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/Css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
    }

