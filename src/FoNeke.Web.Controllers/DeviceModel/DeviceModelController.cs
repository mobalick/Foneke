using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceModel.ViewModels;


namespace FoNeke.Web.Controllers.DeviceModel
{
    using FoNeke.Web.Models.Devices;

    public class DeviceModelController : BaseController<DeviceModel, DeviceModelViewModel>
    {
        public DeviceModelController(IRepository<DeviceModel> repo, IViewModelBuilder<DeviceModel, DeviceModelViewModel> builder) : base(repo, builder)
        {
        }
    }
}
