using EMM.FoNeke.Common.Attributes;

namespace FoNeke.Web.Models.Addressing
{
    public class City : GeoPlace
    {
        public Country Country { get; set; }
        public Region Region { get; set; }

        [FormIgnore]
        public string RegionPK { get; set; }

        [FormIgnore]
        public string CountryPK { get; set; }

        public string ZipCode { get; set; }

        [FormIgnore]
        public int Population { get; set; }

        [FormIgnore]
        public string Slug { get; set; }

        [FormIgnore]
        public string Commentaire { get; set; }

        public override string Display
        {
            get { return string.Format("{0} {1} - {2}", Name,ZipCode, ""/*Country.Display*/); }
        }
    }
}
