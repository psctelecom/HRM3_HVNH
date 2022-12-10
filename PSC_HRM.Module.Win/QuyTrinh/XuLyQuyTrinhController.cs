using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class XuLyQuyTrinhController : ViewController
    {
        public XuLyQuyTrinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        private void XuLyQuyTrinhController_ViewControlsCreated(object sender, EventArgs e)
        {
            DashboardView view = View as DashboardView;
            if (view != null && view.Id == "QuyTrinh_DashboardView")
            {
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id == "CustomControl")
                    {
                        PanelControl panel = item.Control as PanelControl;
                        if (panel != null)
                        {
                            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            panel.Dock = System.Windows.Forms.DockStyle.Fill;

                            QuyTrinhBaseController control = QuyTrinhFactory.CreateControl(Application, View.ObjectSpace);
                            view.Caption = control.Caption;

                            int left = CalculatorLocation(panel.Width, control.Width);
                            int top = CalculatorLocation(panel.Height, control.Height);
                            control.SetLocation(top, left);

                            panel.Controls.Clear();
                            panel.Controls.Add(control);
                            control.PerformLayout();
                        }
                    }
                }
            }
        }

        private int CalculatorLocation(int width1, int width2)
        {
            int location = 0;
            if (width1 > width2)
                location = (width1 - width2) / 2;

            return location;
        }
    }
}
