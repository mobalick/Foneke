using System;
using System.Web.Mvc;

namespace EMM.FoNeke.Common.Attributes
{
    [AttributeUsage(
        AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct |
        AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property |
        AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter |
        AttributeTargets.Delegate | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter)]
    public class FormIgnoreAttribute : Attribute, IMetadataAware
    {
        // http://blog.thekfactor.info/posts/custom-metadata-attributes-in-asp-net-mvc/
        // private readonly int _customValue;

        //public FormIgnoreAttribute(int customValue)
        //{
        //    _customValue = customValue;
        //}

        //public int CustomValue
        //{
        //    get { return _customValue; }
        //}

        //public override void Process(ModelMetadata modelMetaData)
        //{
        //    modelMetaData.AdditionalValues.Add("FormIgnore", true);
        //}
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("FormIgnore", true);
        }
    }
}