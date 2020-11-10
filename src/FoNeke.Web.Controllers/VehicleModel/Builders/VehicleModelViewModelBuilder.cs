using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.VehicleModel.ViewModels;

namespace FoNeke.Web.Controllers.VehicleModel.Builders
{
    using Models.Vehicles;
    public class VehicleModelViewModelBuilder : ViewModelBuilder<VehicleModel, VehicleModelViewModel>, IVehicleModelViewModelBuilder
    {
        public VehicleModelViewModelBuilder(IRepository<VehicleModel> entityRepository) : base(entityRepository)
        {
        }
    }
}
