using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.City.ViewModels;

namespace FoNeke.Web.Controllers.City.Builders
{
    using FoNeke.Web.Models.Addressing;
    public class CityViewModelBuilder : ViewModelBuilder<City, CityViewModel>, ICityViewModelBuilder
    {
        public CityViewModelBuilder(IRepository<City> entityRepository) : base(entityRepository)
        {
        }
    }
}
