using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.VehicleModel.ViewModels;


namespace FoNeke.Web.Controllers.VehicleModel
{
    using FoNeke.Web.Models.Vehicles;

    public class VehicleModelController : BaseController<VehicleModel, VehicleModelViewModel>
    {
        public VehicleModelController(IRepository<VehicleModel> repo, IViewModelBuilder<VehicleModel, VehicleModelViewModel> builder) : base(repo, builder)
        {
        }
    }
}
