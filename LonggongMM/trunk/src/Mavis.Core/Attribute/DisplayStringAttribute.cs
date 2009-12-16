using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mavis.Core
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DisplayStringAttribute: Attribute
    {
        public string Value { get; private set; }
        public string ResourceKey { get; set; }

        public DisplayStringAttribute(string value)
        {
            Value = value;
        }

        public DisplayStringAttribute()
        {
            
        }
    }
}
