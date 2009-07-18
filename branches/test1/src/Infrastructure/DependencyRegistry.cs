using July09v31.Core.Domain;
using July09v31.Infrastructure.DataAccess.Impl;
using StructureMap.Configuration.DSL;

namespace July09v31.Infrastructure
{
    public class DependencyRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType(typeof(IRepository<>)).TheDefaultIsConcreteType(typeof(RepositoryBase<>));
            ForRequestedType(typeof(IKeyedRepository<>)).TheDefaultIsConcreteType(typeof(KeyedRepository<>));
            ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();

        }
    }
}