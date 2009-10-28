using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using CodeCampServer.Core;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.DataAccess;
using CodeCampServer.UnitTests;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class IntegrationTestBase : TestBase
	{
		[SetUp]
		public virtual void Setup()
		{
			injectedInstances.Clear();
			_unitOfWork = new UnitOfWork(GetSessionSource());
			_unitOfWork.Begin();
		}

		[TearDown]
		public virtual void TearDown()
		{
			_unitOfWork.Dispose();
			_unitOfWork = null;
		}

		protected virtual ISession GetSession()
		{
			var interceptor = ObjectFactory.GetInstance<ChangeAuditInfoInterceptor>();
			return TestHelper.GetSessionFactory().OpenSession(interceptor);
		}

		protected virtual void CommitChanges()
		{
			UnitOfWork.Commit();
		}

		private readonly IDictionary<Type, Object> injectedInstances = new Dictionary<Type, Object>();
		private IUnitOfWork _unitOfWork;

		protected IUnitOfWork UnitOfWork
		{
			get { return _unitOfWork; }
		}

		protected void InjectInstance<T>(T instance)
		{
			Type type = typeof(T);
			injectedInstances.Add(type, instance);
		}

		protected TPluginType GetInstance<TPluginType>()
		{
			ExplicitArgsExpression expression = ObjectFactory.With(_unitOfWork);
			injectedInstances.Keys.ForEach(type => { expression = expression.With(type, injectedInstances[type]); });
			return expression.GetInstance<TPluginType>();
		}

		protected object GetInstance(Type pluginType)
		{
			return ObjectFactory.With(_unitOfWork).GetInstance(pluginType);
		}

		protected virtual ISessionSource GetSessionSource()
		{
			return new TestSessionSource(TestHelper.GetSessionFactory());
		}

		/// <summary>
		/// Checks for equality and that all properties' values are equal.
		/// </summary>
		/// <param name="obj1"></param>
		/// <param name="obj2"></param>
		protected static void AssertObjectsMatch(object obj1, object obj2)
		{
			Assert.AreNotSame(obj1, obj2);
			Assert.AreEqual(obj1, obj2);

			PropertyInfo[] infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var info in infos)
			{
				object value1 = info.GetValue(obj1, null);
				object value2 = info.GetValue(obj2, null);
				Assert.AreEqual(value1, value2, string.Format("Property {0} doesn't match", info.Name));
			}
		}

		protected static int CountRowsInTable(ISession session, string table)
		{
			var sql = string.Format("select count(*) from {0}", table);
			var cmd = new SqlCommand(sql, (SqlConnection)session.Connection);
			return (int)cmd.ExecuteScalar();
		}

		public class TestSessionSource : ISessionSource
		{
			private readonly ISessionFactory _sessionFactory;
			private ISession _session;

			public TestSessionSource(ISessionFactory sessionFactory)
			{
				_sessionFactory = sessionFactory;
			}

			public ISession CreateSession()
			{
				if ((_session == null) || (!_session.IsOpen))
				{
					_session = _sessionFactory.OpenSession();
				}
				return _session;
			}
		}
	}
}