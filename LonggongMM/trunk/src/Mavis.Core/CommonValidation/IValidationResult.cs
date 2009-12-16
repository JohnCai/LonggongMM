using System;

namespace Mavis.Core
{
    public interface IValidationResult
    {
        Type ClassContext { get; }

        string PropertyName { get; }

        string Message { get; }
    }
}