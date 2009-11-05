using CodeCampServer.Infrastructure.DataAccess;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaExport"), Explicit]
	public class SchemaExportTester
	{
		[Test, Category("SchemaExport"),Explicit]
		public void ExportSchema()
		{
			new SchemaExport(ConfigurationFactory.Build())
				.Create(true, true);
		}
	}
}