using System.Diagnostics;
using System.Web.Mvc;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.DeviceType.ViewModels;


namespace FoNeke.Web.Controllers.DeviceType
{
    using FoNeke.Web.Models.Devices;

    public class DeviceTypeController : BaseController<DeviceType, DeviceTypeViewModel>
    {
        public DeviceTypeController(IRepository<DeviceType> repo, IViewModelBuilder<DeviceType, DeviceTypeViewModel> builder) : base(repo, builder)
        {
           
        }

    }
}
