using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using EMM.FoNeke.Common.Entities;
using EMM.FoNeke.Common.Extentions;
using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;

namespace EMM.FoNeke.Common.Controllers
{
    [Authorize] 
    public class BaseController<T, TViewModel> : Controller
        where TViewModel : ViewModel<T>, new()
        where T : BaseEntity, new()
    {
        public BaseController(IRepository<T> repo, IViewModelBuilder<T, TViewModel> builder)
        {
            Repository = repo;
            ViewModelBuilder = builder;
        }

        public IRepository<T> Repository { get; set; }
        public IViewModelBuilder<T, TViewModel> ViewModelBuilder { get; set; }

        public virtual ActionResult Index()
        {
            return View();
        }


        public virtual ActionResult Cartouche()
        {
            return View();
        }


        public virtual ActionResult Read(string id)
        {
            //var entity = Repository.GetSingle(e => e.Id == id);
            TViewModel vm = ViewModelBuilder.Build(id);
            return View(vm);
        }

         
        public virtual ActionResult Create()
        {
            var entity = new T() ;
            var vm = ViewModelBuilder.Build(entity);
            ViewBag.IsModal = !string.IsNullOrWhiteSpace(this.GetQueryString("IsModal"));
            return View("Edit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Repository.SaveOrUpdate(viewModel.Item);
               return RedirectToAction("Index", new { viewModel.Item.Id });
            }
            return View("Edit", viewModel);
        }

        public virtual ActionResult Edit(string id)
        {
            T entity = string.IsNullOrWhiteSpace(id) ? new T() : Repository.GetSingle(e => e.Id == id);
            TViewModel vm = ViewModelBuilder.Build(entity);

            ViewBag.IsModal = !string.IsNullOrWhiteSpace(this.GetQueryString("IsModal"));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Repository.SaveOrUpdate(viewModel.Item);
                return RedirectToAction("Edit", new {viewModel.Item.Id});
            }
            return View(viewModel);
        }



        public virtual ActionResult Delete(string id)
        {
            Repository.Delete(id);
            return View("Index");
        }


        public virtual JsonResult SearchByDisplay(string term)
        {
            return
                Json(
                    Repository.GetAll()
                        .Where(v => v.DisplaySaved != null && v.DisplaySaved.ToLowerInvariant().Contains(term.ToLowerInvariant())),
                    JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult GridDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<T> dtos)
        {
            T[] enumerable = dtos as T[] ?? dtos.ToArray();
            foreach (T dto in enumerable)
            {
                Repository.Delete(dto);
            }
            return Json(enumerable.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public virtual JsonResult GridGet([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Repository.GetAll().ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        
    }
}