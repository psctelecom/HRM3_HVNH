using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using PSC_HRM.Module.XuLyQuyTrinh;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    [ToolboxItem(false)]
    public partial class QuyTrinhBaseController : DevExpress.XtraEditors.XtraUserControl
    {
        protected XafApplication Application { get; set; }
        protected IObjectSpace ObjectSpace { get; set; }
        protected IThucHienQuyTrinh ThucHienQuyTrinh { get; set; }
        public string Caption { get; private set; }

        public QuyTrinhBaseController()
        {
            InitializeComponent();
        }

        public QuyTrinhBaseController(XafApplication application, IObjectSpace objectSpace)
        {
            InitializeComponent();

            Application = application;
            ObjectSpace = objectSpace;
        }

        /// <summary>
        /// Set notification
        /// </summary>
        /// <param name="message"></param>
        protected void SetNotification(string message)
        {
            txtNotification.Text = message;
            Caption = message;
        }

        /// <summary>
        /// Set group caption
        /// </summary>
        /// <param name="caption"></param>
        protected void SetGroupCaption(string caption)
        {
            groupControl1.Text = caption;
        }

        /// <summary>
        /// Show help form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnHelp_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Show current data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnOpen_Click(object sender, EventArgs e)
        {

        }

        public virtual void SetLocation(int left, int top)
        {
            Location = new System.Drawing.Point(left, top);
            //Left = left;
            //Top = top;
        }

        protected virtual void btnBatDau_Click(object sender, EventArgs e)
        {

        }

        protected virtual void btnKetThuc_Click(object sender, EventArgs e)
        {

        }
    }
}
