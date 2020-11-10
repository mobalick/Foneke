using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.VehicleMake.ViewModels;


namespace FoNeke.Web.Controllers.VehicleMake
{
    using FoNeke.Web.Models.Vehicles;

    public class VehicleMakeController : BaseController<VehicleMake, VehicleMakeViewModel>
    {
        public VehicleMakeController(IRepository<VehicleMake> repo, IViewModelBuilder<VehicleMake, VehicleMakeViewModel> builder) : base(repo, builder)
        {
        }
    }
}
