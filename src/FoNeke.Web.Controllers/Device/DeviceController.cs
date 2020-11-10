using System.Linq;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Device.ViewModels;


namespace FoNeke.Web.Controllers.Device
{
    using FoNeke.Web.Models.Devices;
    using FoNeke.Web.Repositories.Interfaces;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using System;
    using System.Web.Mvc;

    public class DeviceController : MasterController<Device, DeviceViewModel>
    {
        public IDevicePositionRepository DevicePositionRepository { get; set; }
        public IVehicleRepository VehicleRepository { get; set; }

        public DeviceController(IRepository<Device> repo, IViewModelBuilder<Device, DeviceViewModel> builder) : base(repo, builder)
        {
        }


        public JsonResult GetDevicePositions([DataSourceRequest] DataSourceRequest request, string vehicleId, DateTime? date)
        {
            var positions = DevicePositionRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(vehicleId))
            {
                var device = VehicleRepository.GetById(vehicleId).Device;
                positions = positions.Where(p => p.Number == device.PhoneNumber);
            }
            if (date.HasValue)
            {
                positions = positions.Where(p => p.Time >= date.Value.Date && p.Time <= date.Value.Date.AddDays(1).AddSeconds(-1));
            }

            positions = positions.OrderByDescending(d => d.Time);

            return Json(positions.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public override JsonResult GridGet([DataSourceRequest] DataSourceRequest request)
        {
            var idEntreprise = CurrentUser.Person.IdEntreprise;
            var query = Repository.GetAll();

            if (!IsSuperAdmin)
                query = query.Where(d => d.IdEntreprise == idEntreprise);

            return Json(query.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

    }
}
