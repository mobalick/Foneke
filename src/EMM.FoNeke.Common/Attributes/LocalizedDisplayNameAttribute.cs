using System;
using System.ComponentModel;
using EMM.FoNeke.Common.Extentions;

namespace EMM.FoNeke.Common.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string key)
            : base(key.ToLocalized())
        {
        }
    }
}