namespace EMM.FoNeke.Common.Models.Interfaces.Base
{
    public interface IViewModel<TEntity>
    {
        TEntity Item { get; set; }
        bool IsCreate { get; set; }

    }
}