using System.Collections.Generic;

namespace Mavis.Core
{
    public interface IValidator
    {
        bool IsValid(object value);
        ICollection<IValidationResult> ValidationResultsFor(object value);
    }
}