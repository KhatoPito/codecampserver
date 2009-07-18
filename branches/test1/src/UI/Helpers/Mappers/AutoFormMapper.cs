using AutoMapper;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;

namespace July09v31.UI.Helpers.Mappers
{
    public abstract class AutoFormMapper<TModel, TForm> : Mapper<TModel, TForm> where TModel : PersistentObject, new()
    {
        protected AutoFormMapper(IRepository<TModel> repository) : base(repository) { }

        public override K Map<T, K>(T model)
        {

            return Mapper.Map<T, K>(model);
        }
    }
}