using System;

namespace Tarantino.RulesEngine
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class LabelAttribute : Attribute
	{
		private readonly string _value;

		public LabelAttribute(string value)
		{
			_value = value;
		}

		public string Value
		{
			get { return _value; }
		}
	}
}