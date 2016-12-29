using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AirTransSolution.Module.Reports
{
    public partial class MyTaskReports : DevExpress.XtraReports.UI.XtraReport
    {
        public MyTaskReports()
        {
            InitializeComponent();
        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var foo = (XRTableCell)sender;
        }

    }
}
