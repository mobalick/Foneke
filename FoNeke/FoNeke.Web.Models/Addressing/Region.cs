namespace FoNeke.Web.Models.Addressing
{
    public class Region : GeoPlace
    {
        public Country Country { get; set; }
        public string CountryPK { get; set; }
        public string Slug { get; set; }
    }
}
