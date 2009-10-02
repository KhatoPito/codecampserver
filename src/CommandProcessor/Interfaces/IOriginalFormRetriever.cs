using System;

namespace Tarantino.RulesEngine
{
	public interface IOriginalFormRetriever
	{
		T Retrieve<T>();
		object Retrieve(Type formType);
	}
}