using CodeCampServer.Core.Domain;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using CommandProcessor;
using StructureMap.Configuration.DSL;
using Tarantino.RulesEngine.CommandProcessor;
using Tarantino.RulesEngine.Mvc;

namespace CodeCampServer.Infrastructure
{
	public class IfrastructureRegistry : Registry
	{
		public IfrastructureRegistry()
		{
			ForRequestedType(typeof(IRepository<>)).TheDefaultIsConcreteType(typeof(RepositoryBase<>));
			ForRequestedType(typeof(IKeyedRepository<>)).TheDefaultIsConcreteType(typeof(KeyedRepository<>));
			ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
			ForRequestedType<IRulesEngine>().TheDefaultIsConcreteType<RulesEngine>();
			ForRequestedType<IWebContext>().TheDefaultIsConcreteType<WebContext>();
		}
	}
}