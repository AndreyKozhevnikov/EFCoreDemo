using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace EFCoreDemo.Module.Blazor {
    [PropertyEditor(typeof(Type), true)]
    public class CustomTypePropertyEditor : TypePropertyEditor {
        private readonly static ICollection<Type> suitableTypes = new HashSet<Type>() {
          ///*   Action Permissions */ typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyActionPermissionObject),
          ///*   Address */ typeof(DevExpress.Persistent.BaseImpl.Address),
          ///*   Analytics */ typeof(DevExpress.Persistent.BaseImpl.Analysis),
          ///*   Audit Event */ typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent),
          ///*   Audited Object Weak Reference */ typeof(DevExpress.Persistent.BaseImpl.AuditedObjectWeakReference),
          ///*   Base Role */ typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRoleBase),
          ///*   Base Task */ typeof(DevExpress.Persistent.BaseImpl.Task),
            /* + Contact */ typeof(EFCoreDemo.Module.BusinessObjects.Contact),
          ///*   Country */ typeof(DevExpress.Persistent.BaseImpl.Country),
            /* + Department */ typeof(EFCoreDemo.Module.BusinessObjects.Department),
          ///*   File Data */ typeof(DevExpress.Persistent.BaseImpl.FileData),
          ///*   Location */ typeof(EFCoreDemo.Module.BusinessObjects.Location),
            /* U Member Operation Permissions */ typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyMemberPermissionsObject),
          ///*   Navigation Item Permissions */ typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyNavigationPermissionObject),
          ///*   Note */ typeof(DevExpress.Persistent.BaseImpl.Note),
            /* U Object Operation Permissions */ typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyObjectPermissionsObject),
          ///*   Party */ typeof(DevExpress.Persistent.BaseImpl.Party),
            /* + Paycheck */ typeof(EFCoreDemo.Module.BusinessObjects.Paycheck),
          ///*   Person */ typeof(DevExpress.Persistent.BaseImpl.Person),
          ///*   Phone Number */ typeof(DevExpress.Persistent.BaseImpl.PhoneNumber),
          ///*   Phone Type */ typeof(DevExpress.Persistent.BaseImpl.PhoneType),
          ///*   Portfolio File Data */ typeof(EFCoreDemo.Module.BusinessObjects.PortfolioFileData),
            /* + Position */ typeof(EFCoreDemo.Module.BusinessObjects.Position),
          ///*   Resource */ typeof(DevExpress.Persistent.BaseImpl.Resource),
            /* + Resume */ typeof(EFCoreDemo.Module.BusinessObjects.Resume),
            /* U Role */ typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole),
          ///*   Scheduler Event */ typeof(DevExpress.Persistent.BaseImpl.Event),
            /* + Task */ typeof(EFCoreDemo.Module.BusinessObjects.DemoTask),
            /* U Type Operation Permissions */ typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject),
            /* U User */ typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser)
        };
        public CustomTypePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override bool IsSuitableType(Type type) {
            return base.IsSuitableType(type) && suitableTypes.Contains(type);
        }
    }
}
