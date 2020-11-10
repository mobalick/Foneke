using EMM.FoNeke.Common.Entities;

namespace FoNeke.Web.Models.Devices
{
    public class DeviceType : BaseEntity
    {
        public string DeviceTypeName { get; set; }

        public override string Display => DeviceTypeName;
    }
}