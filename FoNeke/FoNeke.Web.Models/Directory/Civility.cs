using EMM.FoNeke.Common.Entities;
using FoNeke.Web.Models.Enums;

namespace FoNeke.Web.Models.Directory
{
    /// <summary>
    ///  Civilité d'une personne. Ex : Mr, Mme, Mlle, Dr, Pr, ...
    /// </summary>
    public class Civility : BaseEntity
    {
        public GenderEnum Gender { get; set; }
        public string Abreviation { get; set; }
        public string Libelle { get; set; }
    }

}