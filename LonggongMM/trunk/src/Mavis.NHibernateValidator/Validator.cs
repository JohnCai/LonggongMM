using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NHibernate.Validator.Engine;
using Mavis.Core;
using IValidator=Mavis.Core.IValidator;

namespace Mavis.NHibernateValidator
{
    public class Validator: IValidator
    {
        private static readonly ValidatorEngine _validator;
        private ValidatorEngine ValidatorEngine
        {
            get { return _validator; }
        }

        static Validator()
        {
            _validator = new ValidatorEngine();
        }

        #region Implementation of IValidator

        public bool IsValid(object value)
        {
            Check.Require(value != null);
            return ValidatorEngine.IsValid(value);
        }

        public ICollection<IValidationResult> ValidationResultsFor(object value)
        {
            Check.Require(value != null);
            var invalidValues = ValidatorEngine.Validate(value);
            return ParseValidationResultsFrom(invalidValues);
        }

        private static ICollection<IValidationResult> ParseValidationResultsFrom(IEnumerable<InvalidValue> invalidValues)
        {
            ICollection<IValidationResult> validationResults = new Collection<IValidationResult>();

            foreach (var invalidValue in invalidValues)
            {
                validationResults.Add(new ValidationResult(invalidValue));
            }
            return validationResults;
        }

        #endregion
    }
}