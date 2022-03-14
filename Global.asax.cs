using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DB_FirstEntity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["totalvisitor"] = 0;//10000
        }
        protected void Session_Start(object sender,EventArgs arg)
        {
            Application.Lock();// To avoid deadlock
            Application["totalvisitor"] = Convert.ToInt32(Application["totalvisitor"]) + 1;
            Session["UserName"] = null; //Registering session key with current seesion
            Session["LoginTime"] = null;
            Application.UnLock();

        }
        //Application_End()
        //Session_Start()
        //Application_Error()
        //Session_End()
    }
}
