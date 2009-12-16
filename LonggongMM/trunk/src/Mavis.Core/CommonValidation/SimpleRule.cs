using System;

namespace Mavis.Core
{
    public delegate bool SimpleRuleDelegate();

    public class SimpleRule : RuleBase
    {
        private SimpleRuleDelegate _ruleDelegate;

        public SimpleRule(string propertyName, string brokenDescription, SimpleRuleDelegate ruleDelegate): base(propertyName, brokenDescription)
        {
            _ruleDelegate = ruleDelegate;
        }

        #region Overrides of RuleBase

        protected virtual SimpleRuleDelegate RuleDelegate
        {
            get { return _ruleDelegate; }
            set { _ruleDelegate = value; }
        }

        public override string Description { get; set; }
        public override bool ValidateRule(object domainObject)
        {
            return RuleDelegate();
        }

        #endregion
    }
}