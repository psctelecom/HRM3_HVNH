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
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class PMS_NhapHoatDong_HeTuXaController : ViewController
    {
        IObjectSpace _obs = null;
        NhapHuongDan_TuXaNon _source;
        QuanLyXemKeKhaiTuXa_Non _TKB;
        Session ses;
        public PMS_NhapHoatDong_HeTuXaController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyXemKeKhaiTuXa_Non_DetailView";
        }

        void PMS_NhapHoatDong_HeTuXaController_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong == "HVNH")
            //    popDongBo.Active["TruyCap"] = false;
        }

        private void popDongBoTK_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _TKB = View.CurrentObject as QuanLyXemKeKhaiTuXa_Non;
            if (_TKB != null)
            {
                _obs = Application.CreateObjectSpace();
                ses = ((XPObjectSpace)_obs).Session;
                _source = new NhapHuongDan_TuXaNon(ses);
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
        private void popDongBoTK_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            SqlParameter[] pDongBo = new SqlParameter[9];
            pDongBo[0] = new SqlParameter("@NhanVien", _source.NhanVien.Oid);
            pDongBo[1] = new SqlParameter("@TenMonHoc", _source.TenMonHoc);
            pDongBo[2] = new SqlParameter("@LopMonHoc", _source.LopMonHoc);
            pDongBo[3] = new SqlParameter("@BoMonQuanLy", _source.BoMonQuanLy.Oid);
            pDongBo[4] = new SqlParameter("@LoaiHoatDong", _source.LoaiHuongDan.Oid);
            pDongBo[5] = new SqlParameter("@SoLuongHuongDan", _source.SoLuongHuongDan);
            pDongBo[6] = new SqlParameter("@BoPhan", _source.NhanVien.BoPhan.Oid);
            pDongBo[7] = new SqlParameter("@NamHoc", _TKB.NamHoc.Oid);
            pDongBo[8] = new SqlParameter("@HocKy", _TKB.HocKy.Oid);
            DataProvider.ExecuteNonQuery("spd_pms_KekhaiTuXa_NhapHuongDanChuyenDe", CommandType.StoredProcedure, pDongBo);
            _TKB.LoadData();
        }
    }
}