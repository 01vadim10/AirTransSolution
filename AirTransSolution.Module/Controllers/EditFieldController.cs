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

        [NonPersistent]
        private class ChangePropertyValue /*: BaseObject*/
        {
//            public ChangePropertyValue(Session session) : base(session) { }
            public string NewPropValue;

//            public string NewPropValue
//            {
//                get { return newPropValue; }
//                set { SetPropertyValue("New Property value", ref newPropValue, value); }
//            }
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
//                var propValue = objectSpace.CreateObject<Airplane>();
                var propValue = new ChangePropertyValue();
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, propValue);
                e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                DialogController dialogController = new DialogController();
                dialogController.AcceptAction.ExecuteCompleted += AcceptAction_ExecuteCompleted;
                e.ShowViewParameters.Controllers.Add(dialogController);
                Application.ShowViewStrategy.ShowView(e.ShowViewParameters, new ShowViewSource(null, null));

//                foreach (var obj in objectsToProcess) {
//                    Airplane objInNewObjectSpace = (Airplane) objectSpace.GetObject(obj);
//                    PropertyInfo propertyInfo = 
//                        objInNewObjectSpace.GetType().GetProperty(e.SelectedChoiceActionItem.ParentItem.Caption);
//                    propertyInfo.SetValue(objInNewObjectSpace, Convert.ChangeType(value, propertyInfo.PropertyType), null);
//                }
            }
//            if (View is ListView) {
//                objectSpace.CommitChanges();
//                View.ObjectSpace.Refresh();
//            }
        }

        private void AcceptAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            IObjectSpace objectSpace = View is ListView
                ? Application.CreateObjectSpace()
                : View.ObjectSpace;
            var newPropertyValue = (ChangePropertyValue)((SimpleActionExecuteEventArgs)e).SelectedObjects[0];
            foreach (var obj in _objectsToProcess) {
                Airplane objInNewObjectSpace = (Airplane) objectSpace.GetObject(obj);
                PropertyInfo propertyInfo = 
                    objInNewObjectSpace.GetType().GetProperty("Name");
                propertyInfo.SetValue(objInNewObjectSpace, Convert.ChangeType(newPropertyValue.NewPropValue, propertyInfo.PropertyType), null);
            }

            if (View is ListView) {
                objectSpace.Delete(newPropertyValue); //Удаляем объект, который использовался для создания нового значения параметра
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

//        private void ChangePropertyAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
//        {
//            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Airplane));
//            string noteListViewId = Application.FindLookupListViewId(typeof(Airplane));
//            CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(Airplane), noteListViewId);
//            e.View = Application.CreateListView(noteListViewId, collectionSource, true);
//        }
    }
}

