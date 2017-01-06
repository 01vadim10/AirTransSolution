using System;
using DevExpress.ExpressApp.Actions;

namespace AirTransSolution.Module.Controllers
{
    partial class EditFieldController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.EditFieldAction = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // EditFieldAction
            // 
            this.EditFieldAction.Caption = "Edit Field Action";
            this.EditFieldAction.Category = "Edit";
            this.EditFieldAction.ConfirmationMessage = null;
            this.EditFieldAction.Id = "EditFieldAction";
            this.EditFieldAction.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.EditFieldAction.TargetObjectType = typeof(AirTransSolution.Module.BusinessObjects.Airplane);
            this.EditFieldAction.ToolTip = null;
            this.EditFieldAction.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.EditFieldAction_Execute);
            // 
            // EditFieldController
            // 
            this.Actions.Add(this.EditFieldAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction EditFieldAction;
    }
}
