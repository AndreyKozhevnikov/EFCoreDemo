﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Updating;
using EFCoreDemo.Module.Blazor.Controllers;

namespace EFCoreDemo.Module.Blazor {
    public partial class MainDemoBlazorModule : ModuleBase {
        public MainDemoBlazorModule() {
            InitializeComponent();
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            application.CreateCustomLogonWindowControllers += Application_CreateCustomLogonWindowControllers;
        }

        private void Application_CreateCustomLogonWindowControllers(object sender, CreateCustomLogonWindowControllersEventArgs e) {
            e.Controllers.Add(Application.CreateController<LogonParametersViewController>());
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
        }
    }
}
