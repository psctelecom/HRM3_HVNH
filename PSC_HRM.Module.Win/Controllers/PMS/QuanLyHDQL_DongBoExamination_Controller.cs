using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class QuanLyHDQL_DongBoExamination_Controller : ViewController
    {
        IObjectSpace _obs = null;
        QuanLyHoatDongQuanLy QuanLyHDQL;
        Session ses = null;
        public QuanLyHDQL_DongBoExamination_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHoatDongQuanLy_DetailView";
            // Target required Views (via the TargetXXX properties) and create their Actions.
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

        private void btDongBo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            QuanLyHDQL = View.CurrentObject as QuanLyHoatDongQuanLy;
            DialogResult dialogResult = DialogResult.No;
            if (QuanLyHDQL != null)
            {
                if (QuanLyHDQL.Khoa)
                {
                    MessageBox.Show("Dữ liệu đã khóa không thể sử dụng chức năng!", "Thông báo");
                }
                else
                {
                    dialogResult = MessageBox.Show("Dữ liệu củ được đồng bộ sẽ bị xóa, bạn chắc muốn đồng bộ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (DialogUtil.AutoWait("Hệ thống đang đồng bộ dữ liệu"))
                        {
                            View.ObjectSpace.CommitChanges();
                            SqlParameter[] pQuyDoi = new SqlParameter[2];
                            pQuyDoi[0] = new SqlParameter("@NamHoc", QuanLyHDQL.NamHoc.Oid);
                            pQuyDoi[1] = new SqlParameter("@HocKy", QuanLyHDQL.HocKy.Oid);
                            DataProvider.GetValueFromDatabase("spd_PMS_ThuLaoCoiThiVhu_DongBo", CommandType.StoredProcedure, pQuyDoi);
                            View.ObjectSpace.Refresh();
                            XtraMessageBox.Show("Đồng bộ dữ liệu thành công!", "Thông báo");
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Đồng bộ dữ liệu không thành công!", "Thông báo");
                    }
                }
            }
        }
    }
}
