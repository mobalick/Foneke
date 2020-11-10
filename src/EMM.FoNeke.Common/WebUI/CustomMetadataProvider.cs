using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EMM.FoNeke.Common.Entities;

namespace EMM.FoNeke.Common.WebUI
{
    public class CustomMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
            Func<object> modelAccessor, Type modelType, string propertyName)
        {
            Attribute[] attrs = attributes as Attribute[] ?? attributes.ToArray();
            ModelMetadata mm = base.CreateMetadata(attrs, containerType, modelAccessor, modelType, propertyName);
            if (modelType.IsEnum && mm.TemplateHint == null)
            {
                mm.TemplateHint = @"Enum";
            }
            if (modelType.IsAssignableFrom(typeof (bool)) && mm.TemplateHint == null)
            {
                mm.TemplateHint = @"Bool";
            }
            if (modelType.IsAssignableFrom(typeof (BaseEntity)) && mm.TemplateHint == null)
            {
                mm.TemplateHint = @"BaseEntity";
            }

            return mm;
        }
    }
}