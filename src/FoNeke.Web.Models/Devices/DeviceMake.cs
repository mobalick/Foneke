
using EMM.FoNeke.Common.Entities;

namespace FoNeke.Web.Models.Devices
{
    public class DeviceMake : BaseEntity
    {
        public string Make { get; set; }
        public override string Display => Make;
    }
}
