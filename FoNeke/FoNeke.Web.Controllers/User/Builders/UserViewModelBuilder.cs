using EMM.FoNeke.Common.Models.Implementations.Base;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Web.Controllers.User.ViewModels;
using FoNeke.Web.Repositories.Interfaces;

namespace FoNeke.Web.Controllers.User.Builders
{
    using Models.Directory;
    public class UserViewModelBuilder : ViewModelBuilder<User, UserViewModel>, IUserViewModelBuilder
    {
        public IEntrepriseRepository EntrepriseRepository { get; set; }

        public UserViewModelBuilder(IRepository<User> entityRepository) : base(entityRepository)
        {
        }

        public override UserViewModel Build(User entity)
        {
            var model = base.Build(entity);

            if (!string.IsNullOrWhiteSpace(model.Item.IdEntreprise))
            {
                model.Item.Entreprise = EntrepriseRepository.GetById(model.Item.IdEntreprise);
            }
            return model;
        }
    }
}
