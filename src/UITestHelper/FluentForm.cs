using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace UITestHelper
{

	public static class WatinFluentFormExtensions
	{
		public static FluentForm<TForm> WithTextInput<TForm>(this FluentForm<TForm> form, Expression<Func<TForm, string>> property, string text)
		{
			return form.WithInput(new TextInputWrapper(property, text));
		}
	}

	public static class CCSSpecificWatinFluentFormExtensions
	{
		public static FluentForm<TForm> WithTextBoxInput<TForm>(this FluentForm<TForm> form, Expression<Func<TForm, string>> property, string text)
		{
			return form.WithInput(new ScriptWrittenTextBoxInputWrapper(property, text));
		}

		public static FluentForm<TForm> WithDateInput<TForm>(this FluentForm<TForm> form, Expression<Func<TForm, DateTime>> property, DateTime date)
		{
			return form.WithInput(new TextInputWrapper(property, date.ToShortDateString()));
		}
	}

	public class FluentForm<TFormType>
	{
		private readonly WatinDriver _browserDriver;
		private readonly Form _form;
		protected readonly LinkedList<IInputWrapper> _inputWrappers = new LinkedList<IInputWrapper>();

		public FluentForm(WatinDriver browserDriver, IElementsContainer container)
		{
			_browserDriver = browserDriver;
			_form = container.Forms[0];
		}

		public LinkedList<IInputWrapper> InputWrappers
		{
			get {
				return _inputWrappers;
			}
		}

		public FluentForm<TFormType> WithInput(IInputWrapper wrapper)
		{
			_inputWrappers.AddLast(wrapper);
			return this;
		}

		public void Submit()
		{
			ExecuteCapturableExceptionAction(
				() =>
             		{
             			SetInputs();
             			_browserDriver.ClickButton("Submit");
                   	});
		}

		private void SetInputs()
		{
			_inputWrappers.ForEach(x => x.SetInput(_form, _browserDriver));
		}

		private void ExecuteCapturableExceptionAction(Action action)
		{
			try
			{
				action();
			} catch (Exception)
			{
				_browserDriver.CaptureScreenShot(_browserDriver.GetTestname());
				throw;
			}
		}


		//		private readonly LinkedList<IInputWrapper> _assertWrappers = new LinkedList<IInputWrapper>();
		//		public FluentForm<TFormType> VerifyDisabledFields(params Expression<Func<TFormType, object>>[] fields)
		//		{
		//			foreach (var field in fields)
		//			{
		//				_assertWrappers.AddLast(new DisabledField(field));
		//			}
		//			return this;
		//		}
		//		public FluentForm<TFormType> VerifyValue(Expression<Func<TFormType, object>> field, object value)
		//		{
		//			_assertWrappers.AddLast(new VerifyField(field,value));
		//			return this;
		//		}
		//
		//		public FluentForm<TFormType> VerifyDisabledRadioButton(params Expression<Func<TFormType, bool?>>[] fields)
		//		{
		//			foreach (var field in fields)
		//			{
		//				_assertWrappers.AddLast(new DisabledField(field));
		//			}
		//			return this;
		//		}
		//
		//		public FluentForm<TFormType> ShouldHaveValidationSummary()
		//		{
		//			_assertWrappers.AddLast(new FormShouldHaveValidationSummary());
		//			return this;
		//		}
		//
		//		public FluentForm<TFormType> ShouldHaveValidationMessage(string message)
		//		{
		//			_assertWrappers.AddLast(new ValidationMessage(message));
		//			return this;
		//		}

	}
}