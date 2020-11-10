using EMM.FoNeke.Common.Models.Implementations.Base;

namespace FoNeke.Web.Controllers.Vehicle.ViewModels
{
    using FoNeke.Web.Models.Actions;
    using Models.Vehicles;
    using System;

    public class VehicleViewModel : ViewModel<Vehicle>
    {
        public string Id { get; set; }
        public DevicePosition LastPosition { get; set; }

        public DateTime? DateHistory { get; set; }
    }
}
