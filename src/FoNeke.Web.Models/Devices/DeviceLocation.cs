
using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models.Addressing;
using System;

namespace FoNeke.Web.Models.Devices
{
    public class DeviceLocation : BaseEntity
    {
        public decimal? Lon { get; set; }
        public decimal? Lat { get; set; }
        public DateTime? DateLocation { get; set; }
        public virtual Address Addresse { get; set; }
        public decimal? Speed { get; set; }

        public virtual Device Device { get; set; }
    }
}
