using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Entreprise.ViewModels;

namespace FoNeke.Web.Controllers.Entreprise.Builders
{
    using FoNeke.Web.Models.Directory;
    public class EntrepriseViewModelBuilder : ViewModelBuilder<Entreprise, EntrepriseViewModel>, IEntrepriseViewModelBuilder
    {
        public EntrepriseViewModelBuilder(IRepository<Entreprise> entityRepository) : base(entityRepository)
        {
        }
    }
}
