using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate.Tool.hbm2ddl;

namespace CodeCampServer.Infrastructure.Database
{
	public class DatabaseCreator
	{
		public static void Create()
		{
			new SchemaUpdate(new HybridSessionBuilder().GetConfiguration()).Execute(false, true);			
		}
	}
}