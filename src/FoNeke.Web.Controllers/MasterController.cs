using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Entities;
using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.Identity;
using FoNeke.Web.Models.Directory;
using FoNeke.Web.Repositories.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using FoNeke.Web.Models;
using EMM.FoNeke.Common.Extentions;

namespace FoNeke.Web.Controllers
{
    [Authorize] 
    public class MasterController<T, TViewModel> : BaseController<T,TViewModel> where TViewModel : ViewModel<T>, new()
        where T : BaseEntity, new()
    {
        private ApplicationUserManager _userManager;
        public bool IsSuperAdmin { get; set; }
        public IEntrepriseRepository EntrepriseRepository { get; set; }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationUser CurrentUser {
            get { return UserManager.FindById(User.Identity.GetUserId()); }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            SetIsSuperAdmin();

            //if (!Request.IsAjaxRequest() && User.Identity.IsAuthenticated)
            //    ViewBag.NewNotificationCount = NotificationRepository.GetReceivedByUser(CurrentUser.User.Id).Count(n => !n.IsRead);

        }


        public MasterController(IRepository<T> repo, IViewModelBuilder<T, TViewModel> builder) : base(repo, builder)
        {
            //ViewData["IsSuperAdmin"] = CurrentUser.Person.Roles.Any(r => r == RoleEnum.SuperAdmin);
        }
        
        private void SetIsSuperAdmin()
        {
            IsSuperAdmin = CurrentUser.Roles.Any(r => r == "SuperAdmin");
            ViewData["IsSuperAdmin"] = IsSuperAdmin;
            ViewData["CurrentEntrepriseId"] = CurrentUser.Person.IdEntreprise;
        }

        public override ActionResult Create()
        {
            var vm = ViewModelBuilder.Build(new T());
            SetEntreprise(vm.Item);
            ViewBag.IsModal = !string.IsNullOrWhiteSpace(this.GetQueryString("IsModal"));
            return View("Edit", vm);
        }


        public override ActionResult Edit(string id)
        {
            T entity = string.IsNullOrWhiteSpace(id) ? new T() : Repository.GetSingle(e => e.Id == id);
            TViewModel vm = ViewModelBuilder.Build(entity);
            SetEntreprise(vm.Item);

            ViewBag.IsModal = !string.IsNullOrWhiteSpace(this.GetQueryString("IsModal"));
            return View(vm);

        }

        public override ActionResult Create(TViewModel viewModel)
        {
            SetIdEntreprise(viewModel.Item);
            return base.Create(viewModel);
        }

        public override ActionResult Edit(TViewModel viewModel)
        {
            SetIdEntreprise(viewModel.Item);
            return base.Edit(viewModel);
        }

        public void SetIdEntreprise(T entity)
        {
            var item = entity as BaseEntityEntreriseId;

            if (item != null && item.IdEntreprise == null)
            {
                item.IdEntreprise = item.Entreprise != null
                    ? item.Entreprise.Id
                    : CurrentUser.Person.IdEntreprise;
            }

        }

        public void SetEntreprise(T entity) {
            var item = entity as BaseEntityEntreriseId;
            if (item != null)
            {
                var idEntreprise = item.IdEntreprise ?? CurrentUser.Person.IdEntreprise;

                item.IdEntreprise = idEntreprise;
                item.Entreprise = EntrepriseRepository.GetById(idEntreprise);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
    }
}
