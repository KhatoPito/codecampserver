using System;

namespace Tarantino.RulesEngine
{
	public interface ISerializer
	{
		string Serialize(object obj);
		object Deserialize(Type type, string str);
	}
}