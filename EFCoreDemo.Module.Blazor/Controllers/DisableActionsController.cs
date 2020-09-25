using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.ViewVariantsModule;
using EFCoreDemo.Module.Controllers;

namespace EFCoreDemo.Module.Blazor.Controllers {
    public class DisableActionsController : ViewController {
        protected override void OnActivated() {
            base.OnActivated();
            DeactivateController<PopupNotesController>();
            DeactivateController<ObjectMethodActionsViewController>();
            DeactivateController<TaskActionsController>();

            if(View is ListView) {
                Frame.GetController<ChangeVariantController>()?.ChangeVariantAction.Active.SetItemValue("BlazorTemporary", false);
            }
        }
        private void DeactivateController<T>() where T : Controller {
            Frame.GetController<T>()?.Active.SetItemValue("BlazorTemporary", false);
        }
        private void ActivateController<T>() where T : Controller {
            Frame.GetController<T>()?.Active.SetItemValue("BlazorTemporary", true);
        }
    }
}
