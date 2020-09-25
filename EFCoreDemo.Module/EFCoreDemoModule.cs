using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Validation;
using DevExpress.Persistent.Validation;
using EFCoreDemo.Module.CodeRules;
using EFCoreDemo.Module.BusinessObjects;

namespace EFCoreDemo.Module {
    public sealed partial class EFCoreDemoModule : ModuleBase {
        public EFCoreDemoModule() {
            InitializeComponent();
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
        }
        public override void Setup(ApplicationModulesManager moduleManager) {
            base.Setup(moduleManager);
            ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(RuleMemberPermissionsCriteria), typeof(IRuleBaseProperties));
            ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(RuleObjectPermissionsCriteria), typeof(IRuleBaseProperties));
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            List<ModuleUpdater> moduleUpdaters = new List<ModuleUpdater>();
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            moduleUpdaters.Add(updater);
            return moduleUpdaters;
        }
		
		 protected override IEnumerable<Type> GetDeclaredExportedTypes() {
            return new Type[] {
                typeof(Address),
                typeof(Contact),
                typeof(ContactDemoTask),
                typeof(Country),
                typeof(DemoTask),
                typeof(Department),
                typeof(DepartmentPosition),
                typeof(FileAttachment),
                typeof(FileData),
                typeof(Location),
                typeof(Note),
                typeof(Party),
                typeof(Paycheck),
                typeof(Person),
                typeof(PhoneNumber),
                typeof(PortfolioFileData),
                typeof(Position),
                typeof(Resume),
                typeof(Task)
            };
        }
		
        static EFCoreDemoModule() {
            ResetViewSettingsController.DefaultAllowRecreateView = false;
        }
        private static bool? isSiteMode;
        public static bool IsSiteMode {
            get {
                if(isSiteMode == null) {
                    string siteMode = System.Configuration.ConfigurationManager.AppSettings["SiteMode"];
                    isSiteMode = ((siteMode != null) && (siteMode.ToLower() == "true"));
                }
                return isSiteMode.Value;
            }
        }
    }
}
