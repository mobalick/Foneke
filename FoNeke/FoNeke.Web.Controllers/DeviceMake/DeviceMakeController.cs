using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceMake.ViewModels;


namespace FoNeke.Web.Controllers.DeviceMake
{
    using FoNeke.Web.Models.Devices;

    public class DeviceMakeController : BaseController<DeviceMake, DeviceMakeViewModel>
    {
        public DeviceMakeController(IRepository<DeviceMake> repo, IViewModelBuilder<DeviceMake, DeviceMakeViewModel> builder) : base(repo, builder)
        {
        }
    }
}
