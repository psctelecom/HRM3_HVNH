using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.BusinessObjects.NonPersistentObjects.TaiKhoan;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class HeThong_DoiMK_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public HeThong_DoiMK_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThongTinTaiKhoan_DetailView";
        }


        private void btThongTinTaiKhoan_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinTaiKhoan tt = View.CurrentObject as ThongTinTaiKhoan;
            if (tt != null)
            {
                _obs = Application.CreateObjectSpace();
                session = ((XPObjectSpace)_obs).Session;
                NguoiSuDung us = session.FindObject<NguoiSuDung>(CriteriaOperator.Parse("UserName =?", tt.Username));
                if (us != null)
                {
                    if (us.ChangePasswordOnFirstLogon == true)
                        XtraMessageBox.Show("Tài khoản đã yêu cầu đổi mật khẩu", "Thông báo");
                    else
                    {
                        us.ChangePasswordOnFirstLogon = true;
                        XtraMessageBox.Show("Yêu cầu đổi mật khẩu thành công!", "Thông báo");

                        _obs.CommitChanges();
                    }
                }
                _obs.CommitChanges();

                us = session.FindObject<NguoiSuDung>(CriteriaOperator.Parse("UserName =?", tt.Username));
                tt.DoiMatKhau = us.ChangePasswordOnFirstLogon;
            }
        }
    }
}