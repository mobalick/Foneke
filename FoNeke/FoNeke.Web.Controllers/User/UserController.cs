using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using EMM.FoNeke.Common.Controllers;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.User.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;


namespace FoNeke.Web.Controllers.User
{
    using FoNeke.Web.Models.Directory;

    public class UserController : MasterController<User, UserViewModel>
    {
        public UserController(IRepository<User> repo, IViewModelBuilder<User, UserViewModel> builder) : base(repo, builder)
        {
            
        }


        public override ActionResult Create(UserViewModel viewModel)
        {
            if (viewModel.Item.Entreprise != null)
            {
                viewModel.Item.IdEntreprise = viewModel.Item.Entreprise != null
                    ? viewModel.Item.Entreprise.Id
                    : CurrentUser.Person.IdEntreprise;
            }

            if (ModelState.IsValid)
            {
                var user = Repository.SaveOrUpdate(viewModel.Item);
                var lien = $"{Request.Url.Authority}/Account/Login?usermail={viewModel.Item.Email}";
                var message = $"Bonjour,<br/> Veuillez completer la création de votre compte utilisateur FoNeke via <a href='{lien}'>ce lien</a>";
                SendAsync("Creation utilisateur FoNeke", message, user.Result.Email);

                return RedirectToAction("Index", new { viewModel.Item.Id });
            }
            return View("Edit", viewModel);
        }


        public void SendAsync(string subject, string body, string to)
        {
            using (var message = new MailMessage("foneke@outlook.fr", to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                using (var client = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.office365.com",
                    EnableSsl = true,

                    Credentials = new NetworkCredential("foneke@outlook.fr", "Prd2V6NQ42RTM6S", "outlook.com"),
                })
                {
                     client.Send(message);
                }
            }
        }

        [HttpPost]
        public override JsonResult GridGet([DataSourceRequest] DataSourceRequest request)
        {
            var idEntreprise = CurrentUser.Person.IdEntreprise;
            var query = Repository.GetAll().Where(u=> !u.Roles.Contains(RoleEnum.SuperAdmin));

            if (!IsSuperAdmin)
                query = query.Where(d => d.IdEntreprise == idEntreprise);

            return Json(query.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}


//S0ulqu@r1anS