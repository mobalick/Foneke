using EMM.FoNeke.Common.Entities;
using EMM.FoNeke.Common.Models.Interfaces.Base;
using EMM.FoNeke.Common.Repositories;

namespace EMM.FoNeke.Common.Models.Implementations.Base
{
    public class ViewModelBuilder<TEntity, TViewModel> : IViewModelBuilder<TEntity, TViewModel>
        where TViewModel : IViewModel<TEntity>, new()
        where TEntity : BaseEntity, new()
    {
        public readonly IRepository<TEntity> EntityRepository;

        public ViewModelBuilder(IRepository<TEntity> entityRepository)
        {
            EntityRepository = entityRepository;
        }

        public virtual TViewModel Build(TEntity entity, TViewModel viewModel)
        {
            viewModel.Item = entity;
            viewModel.IsCreate = string.IsNullOrWhiteSpace(entity.Id);
            return viewModel;
        }

        public virtual TViewModel Build(TEntity entity)
        {
            return new TViewModel {Item = entity, IsCreate = string.IsNullOrWhiteSpace(entity.Id)};
        }

        public virtual TViewModel Build(string id)
        {
            return Build(EntityRepository.GetById(id));
        }
    }
}