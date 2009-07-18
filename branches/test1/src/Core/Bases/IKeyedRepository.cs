using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface IKeyedRepository<T> : IRepository<T> where T : PersistentObject
    {
        T GetByKey(string key);
    }
}