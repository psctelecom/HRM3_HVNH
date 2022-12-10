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
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    public partial class KhoiLuongGiangDay_QuyDoiKhoiLuongGiangDay_Controller : ViewController
    {

         private KhoiLuongGiangDay_UEL _KhoiLuonGiangDay_UEL;
         private IObjectSpace _obs;
         private Session _session;

         public KhoiLuongGiangDay_QuyDoiKhoiLuongGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            if (TruongConfig.MaTruong == "UEL")
            {
                TargetViewId = "KhoiLuongGiangDay_UEL_DetailView";
            }
            else if (TruongConfig.MaTruong != "UEL")
            {
                TargetViewId = "NULL";
            }
        }


        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KhoiLuonGiangDay_UEL = View.CurrentObject as KhoiLuongGiangDay_UEL;
            _obs = Application.CreateObjectSpace();
            _session = ((XPObjectSpace)_obs).Session;
            if(_KhoiLuonGiangDay_UEL != null)
            {
                if(_KhoiLuonGiangDay_UEL.BangChotThuLao == null)
                {
                    SqlParameter[] pDongBo = new SqlParameter[3];
                    pDongBo[0] = new SqlParameter("@KhoiLuongGiangDay_UEL", _KhoiLuonGiangDay_UEL.Oid);
                    pDongBo[1] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                    pDongBo[1].Direction = ParameterDirection.Output;
                    pDongBo[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                    DataProvider.ExecuteNonQuery("spd_PMS_KhoiLuongGiangDay_QuyDoiKhoiLuongGiangDay", System.Data.CommandType.StoredProcedure, pDongBo);
                    string KQ = pDongBo[1].Value.ToString();
                    if (KQ == "SUCCESS")
                    {
                        XtraMessageBox.Show("Đã quy đổi thành công", "THÀNH CÔNG");
                    }
                    else
                    {
                        XtraMessageBox.Show("Quy đổi không thành công , Lỗi ở  :"+KQ, "LỖI");
                    }
                    View.Refresh();
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    XtraMessageBox.Show("Đã có bảng chốt thù lao , vui lòng không đồng bộ", "LỖI");
                }
            }
            else
            {
                XtraMessageBox.Show("Không tìm thấy Quản lý đào tạo", "LỐI");
            }
        }
    }
}
