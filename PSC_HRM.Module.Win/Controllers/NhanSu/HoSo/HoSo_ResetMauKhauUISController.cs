using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers
{

    public partial class HoSo_ResetMauKhauUISController : ViewController
    {
        public HoSo_ResetMauKhauUISController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HHoSo_ResetMauKhauUISController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong == "DNU" || TruongConfig.MaTruong == "UFM" 
                || TruongConfig.MaTruong == "HUFLIT" || TruongConfig.MaTruong == "CYD" || TruongConfig.MaTruong == "VHU"
                || TruongConfig.MaTruong == "HVNH")
                simpleAction.Active["TruyCap"] = true;
            else
                simpleAction.Active["TruyCap"] = false;

        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            NhanVien nhanvien = null;

            nhanvien = View.CurrentObject as NhanVien;

            SqlParameter[] pQuyDoi = new SqlParameter[1];
            pQuyDoi[0] = new SqlParameter("@User", nhanvien.Oid);
            DataProvider.GetValueFromDatabase("spd_PMS_RestartMatKhauUIS", CommandType.StoredProcedure, pQuyDoi);
            XtraMessageBox.Show("Restart mật khẩu thành công!", "Thông báo");
        }

    }
}
