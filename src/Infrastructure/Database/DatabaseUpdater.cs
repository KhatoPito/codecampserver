using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate.Tool.hbm2ddl;

namespace CodeCampServer.Infrastructure.Database
{
	public class DatabaseUpdater
	{
		public static void Update()
		{
			new SchemaUpdate(new HybridSessionBuilder().GetConfiguration()).Execute(false, true);			
		}
	}
}