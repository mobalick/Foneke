using EMM.FoNeke.Common.Attributes;
using EMM.FoNeke.Common.Entities;

namespace FoNeke.Web.Models.Addressing
{

    public class GeoPlace : BaseEntity
    {
      [FormIgnore]
      public Location Location { get; set; }
   
      //public string Name { get; set; }

      [FormIgnore]
      public  string Code { get; set; }

      [FormIgnore]
      public int PK { get; set; }
    }
}
