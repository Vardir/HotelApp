using System;
using System.Reflection;
using System.Linq.Expressions;

namespace Hotel.Core.Expressions
{
    public static class ExpressionHelpers
    {
        public static T GetPropertyValue<T>(this Expression<Func<T>> expression)
        {
            return expression.Compile().Invoke();
        }
        public static void SetPropertyValue<T>(this Expression<Func<T>> expression, T value)
        {
            var lambdaExpression = (expression as LambdaExpression).Body as MemberExpression;
            // var type = lambda.Body.NodeType;
            var propertyInfo = (PropertyInfo)lambdaExpression.Member;
            var target = Expression.Lambda(lambdaExpression.Expression).Compile().DynamicInvoke();
            propertyInfo.SetValue(target, value);
        }
    }
}
