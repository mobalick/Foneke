using System;
using System.ComponentModel.DataAnnotations;
using EMM.FoNeke.Common.Entities;


namespace FoNeke.Web.Models.Directory
{
    public class Entreprise : BaseEntity
    {
        [Required]
        public string EntrepriseName { get; set; }

        [Required]
        public string SirenSiret { get; set; }

        [Required]
        public Addressing.Address Address { get; set; }

        public override string Display => EntrepriseName;

    }
}
