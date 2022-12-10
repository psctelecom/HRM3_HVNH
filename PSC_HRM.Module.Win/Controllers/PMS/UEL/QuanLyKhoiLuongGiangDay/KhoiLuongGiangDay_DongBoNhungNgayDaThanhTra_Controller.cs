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
    public partial class KhoiLuongGiangDay_DongBoNhungNgayDaThanhTra_Controller : ViewController
    {

         private KhoiLuongGiangDay_UEL _KhoiLuonGiangDay_UEL;
         private IObjectSpace _obs;
         private Session _session;

        public KhoiLuongGiangDay_DongBoNhungNgayDaThanhTra_Controller()
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
            string OidDaLayDuLieu ="";
            if(_KhoiLuonGiangDay_UEL != null)
            {
                if(_KhoiLuonGiangDay_UEL.BangChotThuLao == null)
                {
                    foreach(var item in _KhoiLuonGiangDay_UEL.ListChiTietKhoiLuongGiangDay_UEL)
                    {
                        if(item.DaLayDuLieu == true)
                        {
                            OidDaLayDuLieu += item.Oid_ChiTietThanhTraGiangDay;
                        }
                    }
                    SqlParameter[] pDongBo = new SqlParameter[4];
                    pDongBo[0] = new SqlParameter("@KhoiLuongGiangDay_UEL", _KhoiLuonGiangDay_UEL.Oid);
                    pDongBo[1] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                    pDongBo[1].Direction = ParameterDirection.Output;
                    pDongBo[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                    pDongBo[3] = new SqlParameter("@OidDaLayDuLieu", OidDaLayDuLieu);
                    DataProvider.ExecuteNonQuery("spd_PMS_KhoiLuongGiangDay_DongBoDuLieuGiangDay", System.Data.CommandType.StoredProcedure, pDongBo);
                    string KQ = pDongBo[1].Value.ToString();
                    if (KQ == "SUCCESS")
                    {
                        XtraMessageBox.Show("Đã đồng bộ khối lượng giảng dạy đã thanh tra", "THÀNH CÔNG");
                    }
                    else
                    {
                        XtraMessageBox.Show("Không đồng bộ thành công , Lỗi ở  :"+KQ, "LỖI");
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
