using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models.Devices;
using FoNeke.Web.Models.Directory;
using System;
using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Attributes;

namespace FoNeke.Web.Models.Vehicles
{
    public class Vehicle : BaseEntityEntreriseId
    {
        
        public DateTime? DateOfMaking { get; set; }
        public string DeviceCarLocation { get; set; }
        public string LicencePlate { get; set; }
        public string SerialNumber { get; set; }
        [Required]
        public virtual VehicleModel CarModel { get; set; }
        public virtual Device Device { get; set; }
        public virtual Gaz Gaz { get; set; }
        //public bool IsUseral { get; set; }
        
        //public virtual User UserOwner { get; set; }
        public virtual Assurance Assurance { get; set; }

        public override string Display => $"{LicencePlate} {CarModel?.CarMake?.Make} {CarModel?.ModelName}";
    } 
}
