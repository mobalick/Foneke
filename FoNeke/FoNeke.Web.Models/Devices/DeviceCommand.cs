using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models.Enums;

namespace FoNeke.Web.Models.Devices
{
    public class DeviceCommand  : BaseEntity
    {
        public TypeCommandEnum TypeCommand { get; set; }
        public string CodeCommand { get; set; }
        public override string Display => CodeCommand;
    }
}