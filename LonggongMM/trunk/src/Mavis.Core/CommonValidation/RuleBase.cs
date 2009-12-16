using System;

namespace Mavis.Core
{
    public abstract class RuleBase
    {
        protected RuleBase(string propertyName, string brokenDescription)
        {
            PropertyName = propertyName;
            Description = brokenDescription;
        }

        public string PropertyName { get; set; }
        public abstract string Description { get; set; }

        public abstract bool ValidateRule(object domainObject);
        
    }
}