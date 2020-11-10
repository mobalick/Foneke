using EMM.FoNeke.Common.Attributes;
using FoNeke.Web.Models.Enums;

namespace FoNeke.Web.Models.Addressing
{
    public class Country : GeoPlace
    {
        public  Continent Continent { get; set; }
         [FormIgnore]
        public string ContinentPK { get; set; }
        public SideEnum ContinentSide { get; set; }
        public string Abreviation { get; set; }      
        public int Population { get; set; }

        public override string Display
        {
            get { return Name; }
        }
    }
}
