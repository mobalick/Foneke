namespace EMM.FoNeke.Common.Models.Interfaces.Base
{
    public interface IViewModelBuilder<in TEntity, TViewModel>
    {
        TViewModel Build(TEntity entity, TViewModel viewModel);
        TViewModel Build(TEntity entity);


        TViewModel Build(string id);
    }
}