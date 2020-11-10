using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.VehicleMake.ViewModels;

namespace FoNeke.Web.Controllers.VehicleMake.Builders
{
    using Models.Vehicles;
    public class VehicleMakeViewModelBuilder : ViewModelBuilder<VehicleMake, VehicleMakeViewModel>, IVehicleMakeViewModelBuilder
    {
        public VehicleMakeViewModelBuilder(IRepository<VehicleMake> entityRepository) : base(entityRepository)
        {
        }
    }
}
