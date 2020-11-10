using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Device.ViewModels;

namespace FoNeke.Web.Controllers.Device.Builders
{
    using Models.Devices;
    public class DeviceViewModelBuilder : ViewModelBuilder<Device, DeviceViewModel>, IDeviceViewModelBuilder
    {
        public DeviceViewModelBuilder(IRepository<Device> entityRepository) : base(entityRepository)
        {
        }
    }
}
