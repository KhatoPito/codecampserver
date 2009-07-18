using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface IMapper<TModel, TMessage> where TModel : PersistentObject, new()
    {
        TMessage Map(TModel model);
        TMessage2 Map<TMessage2>(TModel model);
        TModel Map(TMessage message);
    }
}