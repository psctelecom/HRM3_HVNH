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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KeKhai_KhoiLuongSauGiang_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        KhaiBao_KeKhaiSauGiang _source;
        QuanLyKeKhaiSauGiang _View;
        Session ses;
        public KeKhai_KhoiLuongSauGiang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKeKhaiSauGiang_DetailView";
        }

        void KeKhai_KhoiLuongSauGiang_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HVNH")
                popKeKhaiSauGiang.Active["TruyCap"] = false;
        }

        private void popKeKhaiSauGiang_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            View.ObjectSpace.CommitChanges();
            _View = View.CurrentObject as QuanLyKeKhaiSauGiang;
            if (_View != null && !_View.Khoa)
            {
                collectionSource = new CollectionSource(_obs, typeof(KhaiBao_KeKhaiSauGiang));
                using (DialogUtil.AutoWait("Load danh sách giảng viên"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(KhaiBao_KeKhaiSauGiang));
                    _source = new KhaiBao_KeKhaiSauGiang(ses);
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
            else
            {
                XtraMessageBox.Show("Kỳ tính đã khóa không thể kê khai sau giảng!", "Thông báo");
            }
        }

        private void popKeKhaiSauGiang_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {           
            string sql_Query = "";
            foreach (ChiTietKhaiBao_KeKhaiSauGiang ct in _source.listKetKhai)
            {
                if (ct.Chon)
                {
                    sql_Query += " UNION ALL " + "SELECT '"
                       + _View.Oid + "' as QuanLyKeKhaiSauGiang,'"
                       + _source.BacDaoTao.Oid + "' as BacDaoTao,'"
                       + _source.HeDaoTao.Oid + "' as HeDaoTao,'"
                       + ct.OidNhanVien + "' as NhanVien,"                      
                       + ct.ChamThiTN.ToString().Replace(",", ".") + " as SLChamThiTN,"
                       + ct.HDSVThamQuanThucTe.ToString().Replace(",", ".") + " as SLHDSVThamQuanThucTe,"
                       + ct.HDVietCDTN.ToString().Replace(",", ".") + " as SLHDVietCDTN,"
                       + ct.HDDeTaiLuanVan.ToString().Replace(",", ".") + " as SLHDDeTaiLuanVan,"
                       + ct.GiaiDapThacMac.ToString().Replace(",", ".") + " as SLGiaiDapThacMac,"
                       + ct.HeThongHoa_OnThi.ToString().Replace(",", ".") + " as SLHeThongHoa_OnThi,"
                       + ct.SoanDeThi.ToString().Replace(",", ".") + " as SLSoanDeThi,"
                       + ct.BoSungNganHangCauHoi.ToString().Replace(",", ".") + " as SLBoSungNganHangCauHoi,"
                       + ct.RaDeTotNghiep.ToString().Replace(",", ".") + " as SLRaDeTotNghiep,"
                       + ct.RaDeThiHetHocPhan.ToString().Replace(",", ".") + " as SLRaDeThiHetHocPhan"
                       ;
                }
            }
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@sql_Query", sql_Query != "" ? sql_Query.Substring(11) : "");
            param[1] = new SqlParameter("@UserName",HamDungChung.CurrentUser().Oid);
            param[2] = new SqlParameter("@QuanLyKeKhaiSauGiang", _View.Oid);
            DataProvider.ExecuteNonQuery("spd_PMS_KeKhaiSauGiang_DuLieuTho", System.Data.CommandType.StoredProcedure, param);
            View.ObjectSpace.Refresh();
        }
    }
}