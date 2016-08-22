using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PeopleSearch.Server.Server.Config
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            //Note: app.js must be loaded first by the bundler
            // it is assumed that explicitly adding first, then including the ClientApps directory will ensure this happens
            var appBundler = new Bundle("~/bundles/app");
            appBundler.Include("~/ClientAppsTs/app.js");

            bundles.Add(appBundler);
        }
    }
}