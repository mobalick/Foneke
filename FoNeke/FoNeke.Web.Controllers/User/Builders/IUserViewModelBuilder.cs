using EMM.FoNeke.Common.Models.Interfaces.Base;
using FoNeke.Web.Controllers.User.ViewModels;

namespace FoNeke.Web.Controllers.User.Builders
{
    public interface IUserViewModelBuilder : IViewModelBuilder<Models.Directory.User, UserViewModel>
    {
    }
}