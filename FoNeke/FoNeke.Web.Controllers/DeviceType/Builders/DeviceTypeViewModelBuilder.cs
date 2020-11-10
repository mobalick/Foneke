using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceType.ViewModels;

namespace FoNeke.Web.Controllers.DeviceType.Builders
{
    using Models.Devices;
    public class DeviceTypeViewModelBuilder : ViewModelBuilder<DeviceType, DeviceTypeViewModel>, IDeviceTypeViewModelBuilder
    {
        public DeviceTypeViewModelBuilder(IRepository<DeviceType> entityRepository) : base(entityRepository)
        {
        }
    }
}
