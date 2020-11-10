using System.Linq;
using System.Web.Mvc;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Driver.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace FoNeke.Web.Controllers.Driver
{
    using FoNeke.Web.Models.Directory;

    public class DriverController : MasterController<Driver, DriverViewModel>
    {
        public DriverController(IRepository<Driver> repo, IViewModelBuilder<Driver, DriverViewModel> builder) : base(repo, builder)
        {
        }


        [HttpPost]
        public override JsonResult GridGet([DataSourceRequest] DataSourceRequest request)
        {
            var idEntreprise = CurrentUser.Person.IdEntreprise;
            var query = Repository.GetAll().Where(u => !u.Roles.Contains(RoleEnum.SuperAdmin));

            if (!IsSuperAdmin)
                query = query.Where(d => d.IdEntreprise == idEntreprise);

            return Json(query.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}
