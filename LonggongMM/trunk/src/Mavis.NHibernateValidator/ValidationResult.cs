using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mavis.Core;
using NHibernate.Validator.Engine;

namespace Mavis.NHibernateValidator
{
    public class ValidationResult : IValidationResult
    {
        public ValidationResult(InvalidValue invalidValue)
        {
            ClassContext = invalidValue.EntityType;
            PropertyName = invalidValue.PropertyName;
            Message = invalidValue.Message;
            InvalidValue = invalidValue;
        }

        #region Implementation of IValidationResult

        public Type ClassContext { get; protected set; }
        public string PropertyName { get; protected set; }
        public string Message { get; protected set; }

        #endregion

        public virtual InvalidValue InvalidValue { get; protected set; }
    }
}
