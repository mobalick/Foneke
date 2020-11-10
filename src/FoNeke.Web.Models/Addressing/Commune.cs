namespace FoNeke.Web.Models.Addressing
{
	public class Commune : GeoPlace
	{
		public string CodeInsee { get; set; }
		public string DepartementPK { get; set; }
        public Departement Departement { get; set; }


	}
}