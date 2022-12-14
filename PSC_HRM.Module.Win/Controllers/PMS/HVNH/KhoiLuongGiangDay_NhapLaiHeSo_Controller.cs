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
    public partial class KhoiLuongGiangDay_NhapLaiHeSo_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        KhaiBao_LaiHeSoKhoiLuongGiangDay _source;
        KhoiLuongGiangDay _View;
        Session ses;
        public KhoiLuongGiangDay_NhapLaiHeSo_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }

        void KeKhai_KhoiLuongGiangDay_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "HVNH")
                popKeKhaiSauGiang.Active["TruyCap"] = false;
        }
        private void popKeKhaiSauGiang_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            View.ObjectSpace.CommitChanges();
            _View = View.CurrentObject as KhoiLuongGiangDay;
            if (_View != null && !_View.Khoa)
            {
                collectionSource = new CollectionSource(_obs, typeof(KhaiBao_LaiHeSoKhoiLuongGiangDay));
                using (DialogUtil.AutoWait("Load dữ liệu"))
                {
                    collectionSource = new CollectionSource(_obs, typeof(KhaiBao_LaiHeSoKhoiLuongGiangDay));
                    _source = new KhaiBao_LaiHeSoKhoiLuongGiangDay(ses);
                    _source.LoadData(_View.Oid);
                    e.View = Application.CreateDetailView(_obs, _source);
                }
            }
            else
            {
                XtraMessageBox.Show("Dữ liệu đã khóa không thể thay đổi!", "Thông báo");
            }
        }

        private void popKeKhaiSauGiang_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {           
            string sql_Query = "";
            foreach (ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay ct in _source.listKetKhai)
            {
                if (ct.Chon)
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@OidChiTiet", ct.OidChiTiet);
                    param[1] = new SqlParameter("@HeSoBacDaoTao", ct.HeSoBacDaoTao);
                    param[2] = new SqlParameter("@HeSoLopDong", ct.HeSoLopDong);
                    param[3] = new SqlParameter("@HeSoNgoaiGio", ct.HeSoNgoaiGio);
                    param[4] = new SqlParameter("@HeSoNgonNgu", ct.HeSoNgonNgu);
                    param[5] = new SqlParameter("@HeSoTinhChi", ct.HeSoTinhChi);
                    DataProvider.ExecuteNonQuery("spd_PMS_KhoiLuongGiangDay_CapNhatHeSo", System.Data.CommandType.StoredProcedure, param);
                }
            }
            
            View.ObjectSpace.Refresh();
        }
    }
}