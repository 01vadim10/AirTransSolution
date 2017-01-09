using System;
using System.Collections;
using System.Reflection;
using AirTransSolution.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraReports.UserDesigner;

namespace AirTransSolution.Module.Controllers
{
    public partial class EditFieldController : ViewController
    {
        private ChoiceActionItem _chooseField;
        private ArrayList _objectsToProcess;
//        private IObjectSpace objectSpace;

//        [NonPersistent]
        [DefaultClassOptions]
        private class ChangePropertyValue : BaseObject
        {
            public ChangePropertyValue(Session session) : base(session) { }
            public string newPropValue;

            public string NewPropValue
            {
                get { return newPropValue; }
                set { SetPropertyValue("New Property value", ref newPropValue, value); }
            }
        }
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
#warning Reflection using...
            foreach (var field in typeof(Airplane).GetProperties(BindingFlags.Public 
                | BindingFlags.Instance 
                | BindingFlags.DeclaredOnly))
            {
                ChoiceActionItem item = new ChoiceActionItem(field.Name, field);
                _chooseField.Items.Add(item);
            }
        }

        private void EditFieldAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = View is ListView
                ? Application.CreateObjectSpace()
                : View.ObjectSpace;
            _objectsToProcess = new ArrayList(e.SelectedObjects);
            if (e.SelectedChoiceActionItem.ParentItem == _chooseField) {
                var propValue = objectSpace.CreateObject<Airplane>();
                var svp = new ShowViewParameters();
                svp.CreatedView = Application.CreateDetailView(objectSpace, propValue);
                svp.TargetWindow = TargetWindow.NewModalWindow;
                DialogController dialogController = new DialogController();
                dialogController.AcceptAction.ExecuteCompleted += AcceptAction_ExecuteCompleted;
//                TargetViewId = EditFieldAction.SelectedItem.Id;
                svp.Controllers.Add(dialogController);
                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
                objectSpace.Delete(propValue);
            }
        }

        private void AcceptAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            var propertyName = EditFieldAction.SelectedItem.Id;
            var newPropertyValue = (Airplane)((SimpleActionExecuteEventArgs)e).SelectedObjects[0];
#warning Reflection using...
            foreach (var obj in _objectsToProcess) {
                PropertyInfo propertyInfo = 
                    obj.GetType().GetProperty(propertyName);
                propertyInfo.SetValue(obj, Convert.ChangeType(propertyInfo.GetValue(newPropertyValue), propertyInfo.PropertyType), null);
            }

            if (View is ListView) {
                View.ObjectSpace.CommitChanges();
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

//        private void ChangePropertyAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
//        {
//            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Airplane));
//            string noteListViewId = Application.FindLookupListViewId(typeof(Airplane));
//            CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(Airplane), noteListViewId);
//            e.View = Application.CreateListView(noteListViewId, collectionSource, true);
//        }
    }
}

