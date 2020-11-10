namespace FoNeke.Web.Models.Addressing
{
	/// <summary>
	/// Département
	/// </summary>
	public class Departement : GeoPlace
	{
		public string Numero { get; set; }
		public Region Region { get; set; }
        public string RegionPK { get; set; }

	}
}