using EMM.FoNeke.Common.Models.Interfaces.Base;
using FoNeke.Web.Controllers.Vehicle.ViewModels;

namespace FoNeke.Web.Controllers.Vehicle.Builders
{
    public interface IVehicleViewModelBuilder : IViewModelBuilder<Models.Vehicles.Vehicle, VehicleViewModel>
    {
    }
}