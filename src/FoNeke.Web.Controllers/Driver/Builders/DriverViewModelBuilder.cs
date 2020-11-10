using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Driver.ViewModels;

namespace FoNeke.Web.Controllers.Driver.Builders
{
    using Models.Directory;
    public class DriverViewModelBuilder : ViewModelBuilder<Driver, DriverViewModel>, IDriverViewModelBuilder
    {
        public DriverViewModelBuilder(IRepository<Driver> entityRepository) : base(entityRepository)
        {
        }
    }
}
