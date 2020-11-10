using System.Diagnostics;
using System.Web.Mvc;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.City.ViewModels;


namespace FoNeke.Web.Controllers.City
{
    using FoNeke.Web.Models.Addressing;

    public class CityController : BaseController<City, CityViewModel>
    {
        public CityController(IRepository<City> repo, IViewModelBuilder<City, CityViewModel> builder) : base(repo, builder)
        {
           
        }

    }
}
