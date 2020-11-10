
using EMM.FoNeke.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Attributes;


namespace FoNeke.Web.Models.Devices
{
    public  class Device :BaseEntityEntreriseId
    {
        public DateTime? DateOfPurchase { get; set; }
        public DateTime? DateOfInstall { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DeviceModel DeviceModel { get; set; }
        public DeviceType  DeviceType { get; set; }
        public override string Display => DeviceModel?.ModelName + "" + DeviceModel?.DeviceMake?.Make;
    }
}
