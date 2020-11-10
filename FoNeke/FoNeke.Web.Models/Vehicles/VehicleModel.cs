using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Entities;

namespace FoNeke.Web.Models.Vehicles
{
    public class VehicleModel :BaseEntity
    {
        public virtual string ModelName { get; set; }

        [Required]
        public virtual VehicleMake CarMake { get; set; }

        public override string Display => $"{CarMake?.Make} {ModelName}";
    }
}
