using System.Linq.Expressions;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace UITestHelper
{
	public interface IInputWrapper
	{
		void SetInput(Form form, WatinDriver browserDriver);
		void AssertInputValueMatches(Form form, WatinDriver browserDriver);
	}
}