
using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models;
using FoNeke.Web.Models.Directory;
using FoNeke.Web.Models.Vehicles;
using System;

namespace FoNeke.Web.Models.Actions
{
    public class UserHasVehicle : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
