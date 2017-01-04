using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirTransSolution.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace AirTransSolution.Module.Controllers
{
    public partial class EditFieldController : ViewController
    {
        private ChoiceActionItem _chooseField;
        public EditFieldController()
        {
            InitializeComponent();
            EditFieldAction.Items.Clear();
            _chooseField = new ChoiceActionItem(CaptionHelper.GetMemberCaption(typeof(Airplane), "Airplane"), null);
            EditFieldAction.Items.Add(_chooseField);
            FillItemWithValues(_chooseField, typeof(Airplane));
        }

        private void FillItemWithValues(ChoiceActionItem chooseField, Type type)
        {
            foreach (var field in typeof(Airplane).GetProperties()) {
                if (field.PropertyType.IsPublic && field.PropertyType.IsSealed && field.PropertyType.IsSerializable && field.PropertyType.IsClass) {
                    ChoiceActionItem item = new ChoiceActionItem(field.Name, field);
                    _chooseField.Items.Add(item);
                }
            }
        }

        private void EditFieldAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = View is ListView
                ? Application.CreateObjectSpace()
                : View.ObjectSpace;
            ArrayList objectsToProcess = new ArrayList(e.SelectedObjects);
            if (e.SelectedChoiceActionItem.ParentItem == _chooseField) {
                foreach (var obj in objectsToProcess) {
                    Airplane objInNewObjectSpace = (Airplane) objectSpace.GetObject(obj);
                    objInNewObjectSpace.Name = e.SelectedChoiceActionItem.Data.ToString();
                }
            }
            if (View is ListView) {
                objectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}

