using System.ComponentModel;
using EMM.FoNeke.Common.Attributes;

namespace FoNeke.Web.Models.Enums
{
    public enum CivilityEnum
    {
        [Description("CivilityEnum_Mister")]
         Mister,
        [LocalizedDescription("CivilityEnum_Misses")]
        Misses,
        [LocalizedDescription("CivilityEnum_Miss")]
        Miss
    }
}