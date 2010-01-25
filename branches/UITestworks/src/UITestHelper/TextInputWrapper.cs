using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace UITestHelper
{
	public abstract class BaseInputWrapper<TInputValue> : IInputWrapper
	{
		protected TInputValue _value;
		protected LambdaExpression _property;
		protected string _inputName;

		protected BaseInputWrapper(LambdaExpression property, TInputValue value)
		{
			_property = property;
			_value = value;
			_inputName = UINameHelper.BuildNameFrom(_property);
		}

		public abstract void AssertInputValueMatches(Form form, WatinDriver browserDriver);

		public abstract void SetInput(Form form, WatinDriver browserDriver);
	}

	public class TextInputWrapper : BaseInputWrapper<string>
	{
		public TextInputWrapper(LambdaExpression property, string value) : base(property, value) {}

		private TextField GetTextField(IElementsContainer container)
		{
			TextField textField = container.TextField(Find.ByName(_inputName));
			if (textField == null)
				throw new Exception(string.Format("Could not find field '{0}' on form.", _inputName));
			return textField;
		}

		public override void AssertInputValueMatches(Form form, WatinDriver browserDriver)
		{
			var inputElement = GetTextField(form);
			UITestExceptionFactory.AssertEquals(_value, inputElement.Value, "Asserting value for input '"+_inputName+"'.");
		}

		public override void SetInput(Form form, WatinDriver browserDriver)
		{
			var inputElement = GetTextField(form);
			inputElement.Value = _value;
		}
	}

	public class ScriptWrittenTextBoxInputWrapper : BaseInputWrapper<string>
	{
		public ScriptWrittenTextBoxInputWrapper(LambdaExpression property, string value) : base(property, value) {}

		public override void AssertInputValueMatches(Form form, WatinDriver browserDriver)
		{
			throw new NotImplementedException();
		}

		public override void SetInput(Form form, WatinDriver browserDriver)
		{
			browserDriver.IE.RunScript(("tinyMCE.execInstanceCommand('" + _inputName + "', 'mceSetContent', false, '" + _value + "')"));
		}
	}
}