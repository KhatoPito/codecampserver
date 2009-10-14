using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Tarantino.RulesEngine.CommandProcessor;

namespace Tarantino.RulesEngine.Configuration
{
	public class ValidationDefinition<TMessage, TCommand> : ITargetTypeExpression<TMessage, TCommand>,
	                                                        IRuleExpression<TMessage, TCommand>,
	                                                        IRuleOptionsExpression<TCommand>
	{
		protected readonly IList<ValidationRuleInstance> _instances = new List<ValidationRuleInstance>();

		#region IRuleExpression<TMessage,TCommand> Members

		public ITargetTypeExpression<TMessage, TCommand> Rule<T>() where T : IValidationRule
		{
			var instance = new ValidationRuleInstance {ValidationRuleType = typeof (T)};
			_instances.Add(instance);
			return ForMessage();
		}

		public ITargetTypeExpression<TMessage, TCommand> Rule<T>(Expression<Func<TCommand, object>> toCheck)
			where T : IValidationRule
		{
			var instance = new ValidationRuleInstance {ValidationRuleType = typeof (T), ToCheckExpression = toCheck};

			FillUIExpression(toCheck, instance);

			_instances.Add(instance);

			instance.ArrayRule = toCheck.IsArray();

			return this;
		}


		public ITargetTypeExpression<TMessage, TCommand> Rule<T>(Expression<Func<TCommand, object>> toCheck,
		                                                         Expression<Func<TCommand, object>> toCompare)
			where T : ICrossReferencedValidationRule
		{
			var instance = new ValidationRuleInstance
			               	{
			               		ValidationRuleType = typeof (T),
			               		ToCheckExpression = toCheck,
			               		ToCompareExpression = toCompare
			               	};

			FillUIExpression(toCompare, instance);

			_instances.Add(instance);
			return this;
		}

		#endregion

		#region IRuleOptionsExpression<TCommand> Members

		public void Condition(Predicate<TCommand> condition)
		{
			_instances.ForEach(instance => instance.ShouldApply = condition);
		}

		#endregion

		#region ITargetTypeExpression<TMessage,TCommand> Members

		public void RefersTo(Expression<Func<TMessage, object>> uiProperty)
		{
			ValidationRuleInstance instance = _instances.Last();
			instance.UIAttributeExpression = uiProperty;
		}

		public ITypedTargetExpression<TMessage, TCommand, TTarget> ForType<TTarget>()
		{
			var helper = new TargetHelper<TTarget>(this);
			helper.TargetsMessage();
			return helper;
		}

		#endregion

		public ValidationRuleInstance[] GetInstances()
		{
			return _instances.ToArray();
		}

		private ITargetTypeExpression<TMessage, TCommand> ForMessage()
		{
			Expression<Func<TCommand, TCommand>> func = x => x;
			_instances.Last().ToCheckExpression = func;
			return this;
		}

		public IResultExpression<TMessage> Targets<TTarget>(Expression<Func<TCommand, TTarget>> property)
		{
			ValidationRuleInstance instance = _instances.Last();

			instance.ToCheckExpression = property;

			FillUIExpression(property, instance);

			return this;
		}

		private void FlagLastRuleAsArray()
		{
			_instances.Last().ArrayRule = true;
		}

		private static void FillUIExpression<TTarget>(Expression<Func<TCommand, TTarget>> toCheck,
		                                              ValidationRuleInstance instance)
		{
			MemberInfo member = toCheck.GetMember();

			if (member == null)
				return;

			PropertyInfo destinationMember = typeof (TMessage).GetProperty(member.Name);

			if (destinationMember == null)
				return;

			if ((member.GetMemberType() == typeof (DateTime?)) &&
			    (typeof (IDateAndTime).IsAssignableFrom(destinationMember.PropertyType)))
			{
				PropertyInfo dateProperty = destinationMember.PropertyType.GetProperty("Date",
				                                                                       BindingFlags.Public | BindingFlags.Instance);

				ParameterExpression instanceParameter = Expression.Parameter(typeof (TMessage), "target");
				MemberExpression memberExpr = Expression.Property(instanceParameter, destinationMember);
				MemberExpression dateGetterExpression = Expression.Property(memberExpr, dateProperty);

				Expression<Func<TMessage, object>> lambda = Expression.Lambda<Func<TMessage, object>>(dateGetterExpression,
				                                                                                      instanceParameter);

				instance.UIAttributeExpression = lambda;
			}
			else
			{
				ParameterExpression instanceParameter = Expression.Parameter(typeof (TMessage), "target");
				MemberExpression memberExpr = Expression.Property(instanceParameter, destinationMember);

				Expression<Func<TMessage, object>> lambda =
					Expression.Lambda<Func<TMessage, object>>(Expression.Convert(memberExpr, typeof (object)), instanceParameter);

				instance.UIAttributeExpression = lambda;
			}
		}

		private static void FillUIExpression(Expression<Func<TCommand, object>> toCheck, ValidationRuleInstance instance)
		{
			FillUIExpression<object>(toCheck, instance);
		}

		#region Nested type: TargetHelper

		private class TargetHelper<TTarget> : ITypedTargetExpression<TMessage, TCommand, TTarget>
		{
			private readonly ValidationDefinition<TMessage, TCommand> _validationDefinition;

			public TargetHelper(ValidationDefinition<TMessage, TCommand> validationDefinition)
			{
				_validationDefinition = validationDefinition;
			}

			#region ITypedTargetExpression<TMessage,TCommand,TTarget> Members

			public IResultExpression<TMessage> Targets(Expression<Func<TCommand, TTarget>> property)
			{
				_validationDefinition.Targets(property);
				return _validationDefinition;
			}

			public IResultExpression<TMessage> Targets(Expression<Func<TCommand, IEnumerable<TTarget>>> property)
			{
				_validationDefinition.Targets(property);
				_validationDefinition.FlagLastRuleAsArray();
				return _validationDefinition;
			}

			public void RefersTo(Expression<Func<TMessage, object>> uiProperty)
			{
				_validationDefinition.RefersTo(uiProperty);
			}

			#endregion

			public void TargetsMessage()
			{
				_validationDefinition.Targets(x => x);
			}
		}

		#endregion
	}

	public static class ExpressionExtension
	{
		public static bool IsArray(this LambdaExpression actionExpression)
		{
			return actionExpression.GetMember() != null && actionExpression.GetMember().GetMemberType().IsArray;
		}

		public static MemberInfo GetMember(this LambdaExpression actionExpression)
		{
			Expression body = actionExpression.Body;
			if (body.NodeType == ExpressionType.Convert)
				body = ((UnaryExpression) body).Operand;

			var memberExpr = body as MemberExpression;

			if (memberExpr != null)
				return memberExpr.Member;

			var callExpr = body as MethodCallExpression;

			if (callExpr != null)
				return callExpr.Method;

			return null;
		}
	}
}