﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft;
using TA.Classified.Web.App_Start;
using System.Web.Optimization;

namespace TA.Classified.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bundleconfig.RegisterBundles(BundleTable.Bundles);
          
        }
    }
}
