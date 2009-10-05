using System;

//using NHibernate;

namespace Tarantino.RulesEngine
{
	public interface IUnitOfWork : IDisposable
	{
		void Begin();
		void RollBack();
		void Commit();
	}
}