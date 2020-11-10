using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Assurance.ViewModels;

namespace FoNeke.Web.Controllers.Assurance.Builders
{
    using Models.Vehicles;
    public class AssuranceViewModelBuilder : ViewModelBuilder<Assurance, AssuranceViewModel>, IAssuranceViewModelBuilder
    {
        public AssuranceViewModelBuilder(IRepository<Assurance> entityRepository) : base(entityRepository)
        {
        }
    }
}
