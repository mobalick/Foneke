using System.Web;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Identity;
using FoNeke.Web.Controllers.Vehicle.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace FoNeke.Web.Controllers.Vehicle
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using FoNeke.Web.Models.Vehicles;
    using FoNeke.Web.Repositories.Interfaces;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class VehicleController : MasterController<Vehicle, VehicleViewModel>
    {
        public IDevicePositionRepository DevicePositionRepository { get; set; }

        public VehicleController(IRepository<Vehicle> repo, IViewModelBuilder<Vehicle, VehicleViewModel> builder) : base(repo, builder)
        {
        }

        public ActionResult History(string id)
        {
            return View(new VehicleViewModel {Id = id, DateHistory = DateTime.Now });
        }

        public JsonResult GetPositions([DataSourceRequest] DataSourceRequest request,string idVehicle,DateTime? time)
        {
            var vehicle = Repository.GetById(idVehicle);

            var positions = DevicePositionRepository.GetAll().Where(p => p.DeviceId == vehicle.Device.Id);

            return Json(positions.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public override JsonResult GridGet([DataSourceRequest] DataSourceRequest request)
        {
            var idEntreprise = CurrentUser.Person.IdEntreprise;
            
            var vehiclequery = Repository.GetAll();

            if (!IsSuperAdmin)
                vehiclequery = vehiclequery.Where(d => d.IdEntreprise == idEntreprise);

            var vehicles = vehiclequery
                .Select(v => new VehicleViewModel
               {
                   Id           = v.Id,
                   Item         = v,
                   LastPosition = DevicePositionRepository.GetAll().Where(d => d.Number == v.Device.PhoneNumber).OrderByDescending(p => p.Time).FirstOrDefault()
               });

           return Json(vehicles.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(VehicleViewModel viewModel)
        {
            viewModel.Item.IdEntreprise = CurrentUser.Person.IdEntreprise;

            if (ModelState.IsValid)
            {
                Repository.SaveOrUpdate(viewModel.Item);
                return RedirectToAction("Index", new { viewModel.Item.Id });
            }
            return View("Edit", viewModel);
        }
    }
}
