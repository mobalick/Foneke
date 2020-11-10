using EMM.FoNeke.Common.Models.Interfaces.Base;
using FoNeke.Web.Controllers.DeviceCommand.ViewModels;

namespace FoNeke.Web.Controllers.DeviceCommand.Builders
{
    public interface IDeviceCommandViewModelBuilder : IViewModelBuilder<Models.Devices.DeviceCommand, DeviceCommandViewModel>
    {
    }
}