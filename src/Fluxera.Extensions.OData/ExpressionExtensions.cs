namespace Fluxera.Extensions.OData
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using Fluxera.Guards;

	internal static class ExpressionExtensions
	{
		public static Expression<Func<T, object>> ConvertSelector<T, TResult>(
			this Expression<Func<T, TResult>> selector)
			where T : class
		{
			Expression expression = selector.GetMemberInfo();
			ParameterExpression param = Expression.Parameter(typeof(T), "x");
			Expression body = null;

			if(expression is MemberExpression memberExpression)
			{
				MemberExpression field = Expression.PropertyOrField(param, memberExpression.Member.Name);
				body = field;
				if(field.Type.GetTypeInfo().IsValueType)
				{
					body = Expression.Convert(field, typeof(object));
				}
			}
			else if(expression is NewExpression newExpression)
			{
				body = newExpression;
			}

			Guard.Against.Null(body, nameof(body));

			Expression<Func<T, object>> orderExpression = Expression.Lambda<Func<T, object>>(body, param);
			return (Expression<Func<T, object>>)orderExpression.Unquote();
		}

		private static Expression GetMemberInfo(this Expression method)
		{
			LambdaExpression lambda = method as LambdaExpression;
			Guard.Against.Null(lambda, nameof(lambda));

			Expression expression = null;

			if(lambda.Body.NodeType == ExpressionType.Convert)
			{
				expression = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
			}
			else if(lambda.Body.NodeType == ExpressionType.MemberAccess)
			{
				expression = lambda.Body as MemberExpression;
			}
			else if(lambda.Body.NodeType == ExpressionType.New)
			{
				expression = lambda.Body as NewExpression;
			}

			Guard.Against.Null(expression, nameof(expression));

			return expression;
		}

		private static Expression Unquote(this Expression quote)
		{
			if(quote.NodeType == ExpressionType.Quote)
			{
				return ((UnaryExpression)quote).Operand.Unquote();
			}

			return quote;
		}
	}
}
