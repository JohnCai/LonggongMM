using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mavis.Core
{
    /// <summary>
    /// support Validating, all non-private method should be virtual because of NHibernate feature.
    /// </summary>
    [Serializable]
    public class ValidatableObject: NotifierObject, IDataErrorInfo
    {
        private List<RuleBase> _rules = new List<RuleBase>();

        #region Implementation of IDataErrorInfo

        public virtual string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                columnName = (columnName ?? string.Empty).Trim();

                foreach (var brokenRule in GetBrokenRules(columnName))
                {
                    if (columnName == string.Empty || columnName == brokenRule.PropertyName)
                    {
                        result += brokenRule.Description;
                        result += Environment.NewLine;
                    }
                }

                result = result.Trim();

                if (result.Length == 0)
                {
                    result = null;
                }

                return result;
            }
        }

        public virtual string Error
        {
            get
            {
                string result = this[string.Empty];
                if (result != null && result.Trim().Length == 0)
                {
                    result = null;
                }
                return result;
            }
        }

        #endregion

        #region public properties and methods

        public virtual bool IsValid
        { 
            get
            {
                return string.IsNullOrEmpty(Error);
            }
        }

        /// <summary>
        /// Validates all rules on the domain object and returns a list of the broken rules.
        /// </summary>
        /// <returns></returns>
        public virtual ReadOnlyCollection<RuleBase> GetBrokenRules()
        {
            return GetBrokenRules(string.Empty);
        }

        /// <summary>
        /// Validate all rules on the domain object for a given property, returning a list the the broken rules
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public virtual ReadOnlyCollection<RuleBase> GetBrokenRules(string property)
        {
            property = (property ?? string.Empty).Trim();

            var broken = new List<RuleBase>();

            foreach (var rule in _rules)
            {
                if (rule.PropertyName == property || property == string.Empty)
                {
                    bool isRuleBroken = rule.ValidateRule(this);
                    if (isRuleBroken)
                    {
                        broken.Add(rule);
                    }
                }
            }

            return broken.AsReadOnly();
        }

        public virtual ValidatableObject AddRule(RuleBase rule)
        {
            _rules.Add(rule);
            
            return this;
        }

        public virtual ValidatableObject RemoveRule(RuleBase rule)
        {
            _rules.Remove(rule);

            return this;
        }

        #endregion
    }
}