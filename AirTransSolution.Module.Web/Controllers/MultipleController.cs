using System;
using AirTransSolution.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.SystemModule;

namespace AirTransSolution.Module.Web.Controllers
{
    public partial class MultipleController : UpdateListEditorSelectedObjectsController
    {
        private string _defMsg;
        private DeleteObjectsViewController _deleteObjectsViewController;
        private ListViewController _editViewController;

        public MultipleController()
        {
            this.TargetObjectType = typeof(Airplane);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            _deleteObjectsViewController = Frame.GetController<DeleteObjectsViewController>();
            _editViewController = Frame.GetController<ListViewController>();
            if (_deleteObjectsViewController != null) {
                _defMsg = _deleteObjectsViewController.DeleteAction.GetFormattedConfirmationMessage();
                View.SelectionChanged += View_SelectionChanged;
                UpdateConfirmationMsg();
            }
            if (_editViewController != null) {
                View.SelectionChanged += View_SelectionChanged;
            }
        }

        void View_SelectionChanged(object sender, EventArgs e)
        {
            UpdateConfirmationMsg();
//            _editViewController.EditAction.BeginUpdate();
        }

        private void UpdateConfirmationMsg()
        {
            if (View.SelectedObjects.Count == 1) {
                _deleteObjectsViewController.DeleteAction.ConfirmationMessage =
                    String.Format("You are about to delete the '{0}' Contact. Do you want to proceed?",
                    ((Airplane) View.CurrentObject).Name);
            }
            else {
                _deleteObjectsViewController.DeleteAction.ConfirmationMessage =
                    String.Format("You are about to delete {0} Contacts. Do you want to proceed?",
                    View.SelectedObjects.Count);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            
            if (_deleteObjectsViewController != null) {
                View.SelectionChanged -= View_SelectionChanged;
                _deleteObjectsViewController.DeleteAction.ConfirmationMessage = _defMsg;
                _deleteObjectsViewController = null;
            }

            if (_editViewController != null) {
                View.SelectionChanged -= View_SelectionChanged;
                _editViewController = null;
            }
        }
    }
}
