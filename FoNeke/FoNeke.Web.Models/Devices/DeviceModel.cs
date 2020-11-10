using EMM.FoNeke.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Attributes;


namespace FoNeke.Web.Models.Devices 
{
    public class DeviceModel : BaseEntity
    {
        [Required]

        public string ModelName { get; set; }

        [Required]
        public virtual DeviceMake DeviceMake { get; set; }

        [FormIgnore]
        public List<DeviceCommand> DeviceCommands { get; set; }
        public override string Display => $"{DeviceMake?.Display} {ModelName}";
    }
}
