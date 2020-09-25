using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.CodeRules {
    [CodeRule]
    internal class RuleObjectPermissionsCriteria : RuleCriteriaValidationBase {
        public RuleObjectPermissionsCriteria() : base("RuleObjectPermissionsCriteria", "Save", typeof(PermissionPolicyObjectPermissionsObject)) { }
        public RuleObjectPermissionsCriteria(IRuleBaseProperties properties) : base(properties) { }
        protected override string TargetPropertyName => nameof(PermissionPolicyObjectPermissionsObject.Criteria);
    }
}
