namespace EFCoreDemo.Blazor.ServerSide {
    partial class MainDemoBlazorApplication {
        //private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.systemModule1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.systemBlazorModule1 = new DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule();
            this.fileAttachmentsBlazorModule1 = new DevExpress.ExpressApp.FileAttachments.Blazor.FileAttachmentsBlazorModule();
            this.validationBlazorModule1 = new DevExpress.ExpressApp.Validation.Blazor.ValidationBlazorModule();
            this.efCoreMainDemoModule = new Module.EFCoreDemoModule();
            this.blazorMainDemoModule = new Module.Blazor.MainDemoBlazorModule();
            // 
            // MainDemoBlazorApplication
            // 
            this.ApplicationName = "EFCoreDemo";
            this.Modules.Add(this.systemModule1);
            this.Modules.Add(this.securityModule1);
            //this.Modules.Add(this.auditTrailModule);
            this.Modules.Add(this.systemBlazorModule1);
            this.Modules.Add(this.fileAttachmentsBlazorModule1);
            this.Modules.Add(this.validationBlazorModule1);
            this.Modules.Add(this.efCoreMainDemoModule);
            this.Modules.Add(this.blazorMainDemoModule);
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.DatabaseVersionMismatch += MainDemoBlazorApplication_DatabaseVersionMismatch;
            this.LastLogonParametersRead += MainDemoBlazorApplication_LastLogonParametersRead;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        #endregion

        private DevExpress.ExpressApp.FileAttachments.Blazor.FileAttachmentsBlazorModule fileAttachmentsBlazorModule1;
        private DevExpress.ExpressApp.Validation.Blazor.ValidationBlazorModule validationBlazorModule1;
        private DevExpress.ExpressApp.SystemModule.SystemModule systemModule1;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule systemBlazorModule1;
        private Module.EFCoreDemoModule efCoreMainDemoModule;
        private Module.Blazor.MainDemoBlazorModule blazorMainDemoModule;
    }
}
