using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaExport"), Explicit]
	public class SchemaExportTester
	{
		[Test, Category("SchemaExport")]
		public void ExportSchema()
		{
			new SchemaExport(getSessionBuilder().GetConfiguration())
				.Create(true, true);
		}

		private static HybridSessionBuilder getSessionBuilder()
		{
			return new HybridSessionBuilder();
		}
	}

	[TestFixture(Description = "SchemaDrop"), Explicit]
	public class SchemaImportTester
	{
		[Test, Category("SchemaDrop")]
		public void DropSchema()
		{
			new SchemaExport(getSessionBuilder().GetConfiguration()).Drop(true, true);
		}

		private static HybridSessionBuilder getSessionBuilder()
		{
			return new HybridSessionBuilder();
		}
	}

	[TestFixture]
	public class TestHelper
	{
		private static bool _databaseRecreated;

		[Test, Explicit("To output and apply the schema export script")]
		public void ExportSchema()
		{
			new SchemaExport(GetSessionBuilder().GetConfiguration())
				.Create(true, true);
		}

		[Test, Explicit("To drop the database")]
		public void DropSchema()
		{
			new SchemaExport(GetSessionBuilder().GetConfiguration()).Drop(true, true);
		}

		[Test, Explicit("To drop the database")]
		public void DeleteAllData()
		{
			DeleteAllObjects();
		}

		private static void RecreateDatabase()
		{
			ISessionBuilder sessionBuilder = GetSessionBuilder();
			var exporter = new SchemaExport(sessionBuilder.GetConfiguration());
			exporter.Execute(false, true, false, false);
		}

		private static ISessionBuilder GetSessionBuilder()
		{
			return new HybridSessionBuilder();
		}

		public static void DeleteAllObjects()
		{
			RecreateDatabase();
		}

		public static void EnsureDatabaseRecreated()
		{
			if (!_databaseRecreated)
			{
				RecreateDatabase();
				_databaseRecreated = true;
			}
		}
	}
}