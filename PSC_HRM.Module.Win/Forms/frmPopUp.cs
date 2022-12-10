using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.QuyTrinh;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmPopUp<T> : XtraForm where T : BaseController
    {
        private bool _IsCancel;
        public XafApplication Application { get; private set; }
        public IObjectSpace ObjectSpace { get; private set; }
        public T CurrentControl { get; private set; }

        public frmPopUp(XafApplication app, IObjectSpace obs, T control, string caption, bool isPopup)
        {
            InitializeComponent();

            Application = app;
            ObjectSpace = obs;
            CurrentControl = control;

            Text = caption;
            mainPanel.Controls.Clear();
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Controls.Add(control);
            control.PerformLayout();

            Width = control.Width + 40;
            Height = control.Height + 106;
            if (!isPopup)
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public frmPopUp(XafApplication app, IObjectSpace obs, T control, string caption, string acceptCaption, bool isPopup)
        {
            InitializeComponent();

            Application = app;
            ObjectSpace = obs;
            CurrentControl = control;

            Text = caption;
            btnOK.Text = acceptCaption;
            mainPanel.Controls.Clear();
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Controls.Add(control);
            control.PerformLayout();

            Width = control.Width + 40;
            Height = control.Height + 106;
            if (!isPopup)
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _IsCancel = true;
            Close();
        }

        private void frmPopUp_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!_IsCancel)
                e.Cancel = !CurrentControl.IsValidate();
        }
    }
}