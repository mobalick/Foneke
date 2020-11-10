using System;
using FoNeke.Web.Models.Directory;

namespace FoNeke.Web.Models.Vehicles
{
    public  class Assurance : Entreprise
    { 
        public string NomAssurance { get; set; }
        public DateTime? DateSuscription { get; set; }

        public override string Display => NomAssurance;
    }
}
