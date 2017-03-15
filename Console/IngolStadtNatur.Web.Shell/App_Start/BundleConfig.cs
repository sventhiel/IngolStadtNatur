﻿using System.Web.Optimization;

namespace IngolStadtNatur.Web.Shell
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.*",
                "~/Scripts/mvcfoolproof.unobtrusive.min.js"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/locales/bootstrap-datepicker.de.min.js",
                "~/Scripts/lightslider.min.js",
                "~/Scripts/bootstrap3-typeahead.min.js",
                "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/leaflet").Include(
                "~/Scripts/leaflet.js",
                "~/Scripts/leaflet-manager.js",
                "~/Scripts/leaflet.geojsoncss.min.js",
                "~/Scripts/habitats.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/lightslider.min.css",
                "~/Content/bootstrap-datepicker.min.css",
                "~/Content/leaflet.css",
                "~/Content/font-awesome.min.css",
                "~/Content/Site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
