using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceCommand.ViewModels;


namespace FoNeke.Web.Controllers.DeviceCommand
{
    using FoNeke.Web.Models.Devices;

    public class DeviceCommandController : BaseController<DeviceCommand, DeviceCommandViewModel>
    {
        public DeviceCommandController(IRepository<DeviceCommand> repo, IViewModelBuilder<DeviceCommand, DeviceCommandViewModel> builder) : base(repo, builder)
        {
        }
    }
}
