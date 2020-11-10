

using EMM.FoNeke.Common.Entities;

namespace FoNeke.Web.Models.Vehicles
{
    public  class VehicleMake : BaseEntity
    {
        public string Make { get; set; }

        public override string Display => Make;
    }
    
}
