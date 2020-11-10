using Castle.Windsor;
using Castle.Windsor.Installer;
using EMM.FoNeke.Common.Models;
using EMM.FoNeke.Common.WebUI;
using FoNeke.Web.Castle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FoNeke.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            BootstrapContainer();
            ModelBinders.Binders.DefaultBinder = new EntityModelBinder(_container.Kernel);
        }


        private static void BootstrapContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            ModelMetadataProviders.Current = new CustomMetadataProvider();
            
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();
                Response.Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : "www.google.com");
            }
        }
    }
}
