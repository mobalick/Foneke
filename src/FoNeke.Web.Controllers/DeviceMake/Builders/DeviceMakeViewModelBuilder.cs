using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceMake.ViewModels;

namespace FoNeke.Web.Controllers.DeviceMake.Builders
{
    using Models.Devices;
    public class DeviceMakeViewModelBuilder : ViewModelBuilder<DeviceMake, DeviceMakeViewModel>, IDeviceMakeViewModelBuilder
    {
        public DeviceMakeViewModelBuilder(IRepository<DeviceMake> entityRepository) : base(entityRepository)
        {
        }
    }
}
