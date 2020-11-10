using EMM.FoNeke.Common.Attributes;
using EMM.FoNeke.Common.Models.Interfaces.Base;

namespace EMM.FoNeke.Common.Models.Implementations.Base
{
    public class ViewModel<TEntity> : IViewModel<TEntity> where TEntity : class, new()
    {
        private TEntity _item;

        [FormIgnore]
        public TEntity Item
        {
            get => _item ?? new TEntity();
            set => _item = value;
        }

        public bool IsCreate { get; set; }
    }
}