using EMM.FoNeke.Common.Models.Interfaces.Base;
using FoNeke.Web.Controllers.Device.ViewModels;

namespace FoNeke.Web.Controllers.Device.Builders
{
    public interface IDeviceViewModelBuilder : IViewModelBuilder<Models.Devices.Device, DeviceViewModel>
    {
    }
}