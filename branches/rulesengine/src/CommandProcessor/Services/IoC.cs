using System;
using System.Collections.Generic;

namespace Tarantino.RulesEngine
{
	public class IoC
	{
		public static IIoC Current { get; set; }
	}

	public interface IIoC
	{
		Func<Type, IEnumerable<object>> GetAllInstances { get; }
		Func<Type, object> GetInstance { get; }
	}
}