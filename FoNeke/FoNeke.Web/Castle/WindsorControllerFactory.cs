using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace FoNeke.Web.Castle
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, $"You betta get outta my site : the path '{requestContext.HttpContext.Request.Path}' is missing in action.");
            }
            //if (controllerType==typeof(HomeController)||controllerType==typeof(AccountController))
            //{
            //    return base.GetControllerInstance(requestContext,controllerType);
            //}
            return (IController) _kernel.Resolve(controllerType);
        }
    }
}