using EMM.FoNeke.Common.Entities;
using System;
using EMM.FoNeke.Common.Attributes;
using FoNeke.Web.Models.Devices;
using FoNeke.Web.Models.Addressing;

namespace FoNeke.Web.Models.Actions
{
    public class DevicePosition : BaseEntity
    {
        public DevicePosition()
        {
           
        }

        [LocalizedDisplayName("DevicePosition_Latitude")]
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Speed { get; set; }
        public DateTime? Time { get; set; }
        public string Url { get; set; }
        public string Number { get; set; }
        public string DeviceId { get; set; }

        public Address Address { get; set; }

    }
}
