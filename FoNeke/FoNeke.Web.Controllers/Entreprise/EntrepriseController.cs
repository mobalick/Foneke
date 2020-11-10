using System.Linq;
using System.Web.Mvc;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Entreprise.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace FoNeke.Web.Controllers.Entreprise
{
    using FoNeke.Web.Models.Directory;

    public class EntrepriseController : MasterController<Entreprise, EntrepriseViewModel>
    {
        public EntrepriseController(IRepository<Entreprise> repo, IViewModelBuilder<Entreprise, EntrepriseViewModel> builder) : base(repo, builder)
        {
        }


        [HttpPost]
        public override JsonResult GridGet([DataSourceRequest] DataSourceRequest request)
        {
            var idEntreprise = CurrentUser.Person.IdEntreprise;
            var query = Repository.GetAll().Where(u => u.EntrepriseName!= "AdminEntrePrise");

            if (!IsSuperAdmin)
                query = query.Where(d => d.Id == idEntreprise);

            return Json(query.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}
