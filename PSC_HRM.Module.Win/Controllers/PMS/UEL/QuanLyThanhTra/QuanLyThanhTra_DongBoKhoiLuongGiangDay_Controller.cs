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
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class QuanLyThanhTra_DongBoKhoiLuongGiangDay_Controller : ViewController
    {
        private Quanlythanhtra _KhoiLuong;
        private IObjectSpace _obs;
        private Session _session;
        public QuanLyThanhTra_DongBoKhoiLuongGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            if(TruongConfig.MaTruong == "UEL")
            {
                TargetViewId = "Quanlythanhtra_DetailView";
            }
            if(TruongConfig.MaTruong != "UEL")
            {
                TargetViewId="NULL";
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KhoiLuong = View.CurrentObject as Quanlythanhtra;
            _obs = Application.CreateObjectSpace();
            _session = ((XPObjectSpace)_obs).Session;
            if(_KhoiLuong != null)
            {
                SqlParameter[] pDongBo = new SqlParameter[3];
                pDongBo[0] = new SqlParameter("@QuanLyThanhTra", _KhoiLuong.Oid);
                pDongBo[1] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                pDongBo[1].Direction = ParameterDirection.Output;
                pDongBo[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                DataProvider.ExecuteNonQuery("spd_PMS_QuanLyThanhTra_DongBoDuLieuGiangDay", System.Data.CommandType.StoredProcedure, pDongBo);
                string KQ = pDongBo[1].Value.ToString();
                if (KQ == "SUCCESS")
                {
                    XtraMessageBox.Show("THÀNH CÔNG . Đã đồng bộ những ngày dạy chưa được thanh tra thành công", "Thông báo");
                }
                else
                {
                    XtraMessageBox.Show("LỖI !!! . Không đồng bộ thành công", "Thông báo");
                }
                View.Refresh();
                View.ObjectSpace.Refresh();
            }
            else
            {
                 XtraMessageBox.Show("LỖI !!! . Không tìm thấy quản lý thanh tra", "Thông báo");
            }
        }
    }
}
