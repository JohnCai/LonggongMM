using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mavis.Core
{
    public static class ReflectionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            if (memberExpression != null)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo != null) return propertyInfo.Name;
            }
            return string.Empty;
        }
    }
}