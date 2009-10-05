using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tarantino.RulesEngine
{
	public static class UINameHelper
	{
		public static string BuildIdFrom<TModel>(Expression<Func<TModel, object>> expression)
		{
			return BuildIdFrom((Expression) expression);
		}

		//public static string BuildIdFrom(Expression expression, object value)
		//{
		//    if (value is Enumeration)
		//        value = ((Enumeration) value).Value;

		//    return string.Format("{0}_{1}", BuildIdFrom(expression), value);
		//}

		public static string BuildNameFrom<TModel>(Expression<Func<TModel, object>> expression)
		{
			string expressionText = BuildNameFrom((Expression) expression);
			return expressionText;
		}

		public static string BuildNameFrom(Expression expression)
		{
			Expression expressionToCheck = expression;
			var tokens = new List<string>();

			bool done = false;
			bool accessedMember = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:

						accessedMember = false;
						expressionToCheck = ((UnaryExpression) expressionToCheck).Operand;

						break;
					case ExpressionType.ArrayIndex:
						var binaryExpression = (BinaryExpression) expressionToCheck;

						Expression indexExpression = binaryExpression.Right;
						Delegate indexAction = Expression.Lambda(indexExpression).Compile();
						var value = (int) indexAction.DynamicInvoke();

						if (accessedMember)
						{
							tokens.Add(".");
						}

						tokens.Add(string.Format("[{0}]", value));

						accessedMember = false;
						expressionToCheck = binaryExpression.Left;

						break;
					case ExpressionType.Lambda:
						var lambdaExpression = (LambdaExpression) expressionToCheck;
						accessedMember = false;
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = (MemberExpression) expressionToCheck;

						if (accessedMember)
						{
							tokens.Add(".");
						}

						tokens.Add(memberExpression.Member.Name);

						if (memberExpression.Expression == null)
						{
							done = true;
						}
						else
						{
							accessedMember = true;
							expressionToCheck = memberExpression.Expression;
						}
						break;
					case ExpressionType.Call:
						var callExpression = (MethodCallExpression) expressionToCheck;

						Expression getItemArgumentExpression = callExpression.Arguments[0];
						Delegate itemAction = Expression.Lambda(getItemArgumentExpression).Compile();
						var itemValue = (int) itemAction.DynamicInvoke();

						if (accessedMember)
						{
							tokens.Add(".");
						}

						tokens.Add(string.Format("[{0}]", itemValue));

						accessedMember = false;
						expressionToCheck = callExpression.Object;

						break;
					default:
						done = true;
						break;
				}
			}

			tokens.Reverse();

			string result = string.Join(string.Empty, tokens.ToArray());

			return result;
		}

		public static int? ExtractIndexValueFrom(Expression expression)
		{
			Expression expressionToCheck = expression;

			bool done = false;
			int? indexValue = null;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:
						expressionToCheck = ((UnaryExpression) expressionToCheck).Operand;
						break;
					case ExpressionType.ArrayIndex:
						var binaryExpression = (BinaryExpression) expressionToCheck;

						Expression indexExpression = binaryExpression.Right;
						Delegate indexAction = Expression.Lambda(indexExpression).Compile();

						indexValue = (int) indexAction.DynamicInvoke();
						done = true;

						break;
					case ExpressionType.Lambda:
						var lambdaExpression = (LambdaExpression) expressionToCheck;
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = (MemberExpression) expressionToCheck;
						expressionToCheck = memberExpression.Expression;
						break;
					default:
						done = true;
						break;
				}
			}

			return indexValue;
		}

		public static string BuildIdFrom(Expression expression)
		{
			Expression expressionToCheck = expression;
			var tokens = new List<string>();

			bool done = false;
			bool accessedMember = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:

						accessedMember = false;
						expressionToCheck = ((UnaryExpression) expressionToCheck).Operand;

						break;
					case ExpressionType.ArrayIndex:
						var binaryExpression = (BinaryExpression) expressionToCheck;

						Expression indexExpression = binaryExpression.Right;
						Delegate indexAction = Expression.Lambda(indexExpression).Compile();
						var value = (int) indexAction.DynamicInvoke();

						if (accessedMember)
						{
							tokens.Add("_");
						}

						tokens.Add(string.Format("_{0}_", value));

						accessedMember = false;
						expressionToCheck = binaryExpression.Left;

						break;
					case ExpressionType.Lambda:
						var lambdaExpression = (LambdaExpression) expressionToCheck;
						accessedMember = false;
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = (MemberExpression) expressionToCheck;

						if (accessedMember)
						{
							tokens.Add("_");
						}

						tokens.Add(memberExpression.Member.Name);

						if (memberExpression.Expression == null)
						{
							done = true;
						}
						else
						{
							accessedMember = true;
							expressionToCheck = memberExpression.Expression;
						}
						break;
					default:
						done = true;
						break;
				}
			}

			tokens.Reverse();

			string result = string.Join(string.Empty, tokens.ToArray());

			return result;
		}

		public static string BuildLabelFrom(Expression expression)
		{
			Expression expressionToCheck = expression;
			var tokens = new List<string>();

			bool done = false;
			bool accessedMember = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:

						accessedMember = false;
						expressionToCheck = ((UnaryExpression) expressionToCheck).Operand;

						break;
					case ExpressionType.ArrayIndex:
						var binaryExpression = (BinaryExpression) expressionToCheck;

						accessedMember = false;
						expressionToCheck = binaryExpression.Left;

						break;
					case ExpressionType.Lambda:
						var lambdaExpression = (LambdaExpression) expressionToCheck;
						expressionToCheck = lambdaExpression.Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = (MemberExpression) expressionToCheck;

						if (! memberExpression.Type.IsArray)
						{
							string label = LabelBuilder.GetLabel(memberExpression.Member);

							if (! string.IsNullOrEmpty(label))
							{
								if (accessedMember)
								{
									tokens.Add(" ");
								}

								accessedMember = true;

								tokens.Add(label);
							}
						}

						if (memberExpression.Expression == null)
						{
							done = true;
						}
						else
						{
							expressionToCheck = memberExpression.Expression;
						}
						break;
					case ExpressionType.Call:
						var callExpression = (MethodCallExpression) expressionToCheck;

						if (accessedMember)
						{
							tokens.Add(" ");
						}

						tokens.Add(LabelBuilder.GetLabel(callExpression.Method));

						accessedMember = true;
						expressionToCheck = callExpression.Object;

						break;
					default:
						done = true;
						break;
				}
			}

			tokens.Reverse();

			string result = string.Join(string.Empty, tokens.ToArray());

			return result;
		}
	}
}