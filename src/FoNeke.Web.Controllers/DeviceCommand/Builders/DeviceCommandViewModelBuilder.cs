using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceCommand.ViewModels;

namespace FoNeke.Web.Controllers.DeviceCommand.Builders
{
    using Models.Devices;
    public class DeviceCommandViewModelBuilder : ViewModelBuilder<DeviceCommand, DeviceCommandViewModel>, IDeviceCommandViewModelBuilder
    {
        public DeviceCommandViewModelBuilder(IRepository<DeviceCommand> entityRepository) : base(entityRepository)
        {
        }
    }
}
