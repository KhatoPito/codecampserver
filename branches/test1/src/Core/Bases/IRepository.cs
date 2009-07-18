using System;
using July09v31.Core.Domain.Model;

namespace July09v31.Core.Domain
{
    public interface IRepository<T> where T : PersistentObject
    {
        T GetById(Guid id);
        void Save(T entity);
        T[] GetAll();
        void Delete(T entity);
    }
}