using System;
using System.Web.Mvc;
using Castle.MicroKernel;
using EMM.FoNeke.Common.Entities;
using EMM.FoNeke.Common.Repositories;

namespace EMM.FoNeke.Common.Models
{
    public class EntityModelBinder : DefaultModelBinder
    {
        private readonly IKernel _kernel;

        public EntityModelBinder(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var name = $"{bindingContext.ModelName}.Id";

            if (!bindingContext.ModelType.IsSubclassOf(typeof (BaseEntity)) || 
                bindingContext.ValueProvider.GetValue(name) == null || 
                string.IsNullOrWhiteSpace(bindingContext.ValueProvider.GetValue(name).AttemptedValue))

                return base.BindModel(controllerContext, bindingContext);

            Type[] typeArgs = { bindingContext.ModelType };
            var type = typeof(IRepository<>).MakeGenericType(typeArgs);
            dynamic repo = _kernel.Resolve(type);
            
            var model = repo.GetById(bindingContext.ValueProvider.GetValue(name).AttemptedValue);
            bindingContext.ModelMetadata.Model = model ?? bindingContext.ModelMetadata.Model;

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}