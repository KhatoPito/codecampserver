using CodeCampServer.DependencyResolution;
using NHibernate;
using NHibernate.Cfg;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public class ConfigBasedSessionSource : ISessionSource
	{
		private readonly ISessionFactory _sessionFactory;

		public ConfigBasedSessionSource(Configuration configuration)
		{
			_sessionFactory = configuration.BuildSessionFactory();
		}

		public ISession CreateSession()
		{
			var interceptor = DependencyRegistrar.Resolve<ChangeAuditInfoInterceptor>();

			return _sessionFactory.OpenSession(interceptor);
		}
	}
}