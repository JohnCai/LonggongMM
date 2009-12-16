using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Mavis.Core;

namespace Mavis.MVVM
{
    public static class ObservableHelper
    {
        public static PropertyChangedEventArgs CreateArgs<T>(Expression<Func<T, object>> propertyExpression)
        {
            return new PropertyChangedEventArgs(ReflectionHelper.GetPropertyName<T>(propertyExpression));
        }

        
    }
}