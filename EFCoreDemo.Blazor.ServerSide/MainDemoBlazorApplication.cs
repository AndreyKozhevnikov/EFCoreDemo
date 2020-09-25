using DevExpress.EntityFrameworkCore.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using EFCoreDemo.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreDemo.Blazor.ServerSide {
    public partial class MainDemoBlazorApplication : BlazorApplication {
        class EmptySettingsStorage : SettingsStorage {
            public override string LoadOption(string optionPath, string optionName) => null;
            public override void SaveOption(string optionPath, string optionName, string optionValue) { }
        }

#region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static MainDemoBlazorApplication() {
            FrameworkSettings.DefaultSettingsCompatibilityMode = FrameworkSettingsCompatibilityMode.Latest;
        }
#endregion

        public MainDemoBlazorApplication() {
            InitializeComponent();
            AboutInfo.Instance.Version = "Version " + AssemblyInfo.FileVersion;
            AboutInfo.Instance.Copyright = AssemblyInfo.AssemblyCopyright + " All Rights Reserved";
        }
        protected override void OnSetupStarted() {
            base.OnSetupStarted();
#if !DEBUG // IsSiteMode
            ConnectionString = DemoDataStoreProvider.ConnectionString;
#else
            IConfiguration configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            if(configuration.GetConnectionString("ConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("ConnectionString");
            }
#if EASYTEST
            if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("EasyTestConnectionString");
            }
#endif
#endif
//#if DEBUG
//            if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
//            }
//#endif
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            EFCoreObjectSpaceProvider eFCoreObjectSpaceProvider = new SecuredEFCoreObjectSpaceProvider((ISelectDataSecurityProvider)Security, typeof(EFCoreDemoDbContext), TypesInfo, args.ConnectionString,
                (builder, connectionString) => { builder.UseSqlServer(connectionString); });
            args.ObjectSpaceProviders.Add(eFCoreObjectSpaceProvider);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void MainDemoBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }
        protected override SettingsStorage CreateLogonParameterStoreCore() {
            return new EmptySettingsStorage();
        }
        private void MainDemoBlazorApplication_LastLogonParametersRead(object sender, LastLogonParametersReadEventArgs e) {
            if(e.LogonObject is AuthenticationStandardLogonParameters logonParameters && string.IsNullOrEmpty(logonParameters.UserName)) {
                logonParameters.UserName = "Sam";
            }
        }
    }
}
