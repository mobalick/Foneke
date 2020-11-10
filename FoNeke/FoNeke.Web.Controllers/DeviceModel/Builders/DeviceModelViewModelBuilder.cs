using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceModel.ViewModels;

namespace FoNeke.Web.Controllers.DeviceModel.Builders
{
    using Models.Devices;
    public class DeviceModelViewModelBuilder : ViewModelBuilder<DeviceModel, DeviceModelViewModel>, IDeviceModelViewModelBuilder
    {
        public DeviceModelViewModelBuilder(IRepository<DeviceModel> entityRepository) : base(entityRepository)
        {
        }
    }
}
