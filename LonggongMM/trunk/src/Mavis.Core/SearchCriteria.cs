using System;
using System.Linq.Expressions;

namespace Mavis.Core
{
    public class SearchCriteria<T>
    {
        public SearchCriteria(Expression<Func<T, object>> propertyExpression, object propertyValue, Operator @operator)
        {
            PropertyExpression = propertyExpression;
            PropertyValue = propertyValue;
            Operator = @operator;
        }

        public SearchCriteria(string propertyName, string associationPath, object propertyValue, Operator @operator)
        {
            PropertyName = propertyName;
            AssociationPath = associationPath;
            PropertyValue = propertyValue;
            Operator = @operator;
        }

        public Expression<Func<T, object>> PropertyExpression { get; set; }
        public object PropertyValue { get; set; }
        public Operator Operator { get; set; }

        public string PropertyName { get; set; }
        public string AssociationPath { get; set; }
    }

    public enum Operator
    {
        Equal,
        Like,
        GreaterThan,
        LessThan,
        LessEqual,
        GreaterEqual
    }
}