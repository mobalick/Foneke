using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Vehicle.ViewModels;

namespace FoNeke.Web.Controllers.Vehicle.Builders
{
    using Models.Vehicles;
    public class VehicleViewModelBuilder : ViewModelBuilder<Vehicle, VehicleViewModel>, IVehicleViewModelBuilder
    {
        public VehicleViewModelBuilder(IRepository<Vehicle> entityRepository) : base(entityRepository)
        {
        }
    }
}
