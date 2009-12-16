using System;

namespace Mavis.Core
{
    /// <summary>
    /// Attribute to specify a field of a domain class whether it is Readonly or not when adding/editing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ParkingRuleAttribute: Attribute
    {
        public bool IsReadOnlyWhenAdding { get; private set; }

        public bool IsReadOnlyWhenEditing { get; private set; }

        public ParkingRuleAttribute(bool isReadOnlyWhenAdding, bool isReadOnlyWhenEditing)
        {
            IsReadOnlyWhenAdding = isReadOnlyWhenAdding;
            IsReadOnlyWhenEditing = isReadOnlyWhenEditing;
        }
    }
}