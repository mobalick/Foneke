using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Assurance.ViewModels;


namespace FoNeke.Web.Controllers.Assurance
{
    using FoNeke.Web.Models.Vehicles;

    public class AssuranceController : BaseController<Assurance, AssuranceViewModel>
    {
        public AssuranceController(IRepository<Assurance> repo, IViewModelBuilder<Assurance, AssuranceViewModel> builder) : base(repo, builder)
        {
        }
    }
}
