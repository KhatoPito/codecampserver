using System;

namespace Tarantino.RulesEngine
{
	public interface IDateAndTime
	{
		string Date { get; set; }
		string Hour { get; set; }
		string Minute { get; set; }
		bool IsEmpty();
		DateTime? GetValue();
	}
}