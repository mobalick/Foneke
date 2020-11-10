using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Attributes;
using FoNeke.Web.Models.Enums;

namespace FoNeke.Web.Models.Addressing
{
    public class Address : GeoPlace
    {
        [LocalizedDisplayName("Address_City")]
        public virtual City City { get; set; }

        [Required]
        [LocalizedDisplayName("Address_AddressLine")]
        public string AddressLine { get; set; }

        [LocalizedDisplayName("Address_AddressLine2")]
        public string AddressLine2 { get; set; }

        [LocalizedDisplayName("Address_TypeStreet")]
        public TypeStreetEnum TypeStreet { get; set; }

    }
}
