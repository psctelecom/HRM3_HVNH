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
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;
using PSC_HRM.Module.PMS.NghiepVu.HUFLIT;
using PSC_HRM.Module.PMS.CauHinh.HeSo;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class PMS_DanhMucNhieuHeSoChucDanh_ChayStore_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        CollectionSource collectionSource;
        DanhSachMonHocNhieuChucDanh _source;
        QuanLyHeSo qly;
        public PMS_DanhMucNhieuHeSoChucDanh_ChayStore_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHeSo_DetailView";
        }


        private void PMS_DanhMucNhieuHeSoChucDanh_ChayStore_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HUFLIT")
            {
                popDongBo.Active["TruyCap"] = true;
            }
            else
            {
                popDongBo.Active["TruyCap"] = false;
            }
        }
        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            qly = View.CurrentObject as QuanLyHeSo;

            using (DialogUtil.AutoWait("Load danh sách"))
            {
                collectionSource = new CollectionSource(_obs, typeof(DanhSachMonHocNhieuChucDanh));
                _source = new DanhSachMonHocNhieuChucDanh(ses);
                _source.NamHoc = ses.GetObjectByKey<NamHoc>(qly.NamHoc.Oid);
                _source.HocKy = ses.GetObjectByKey<HocKy>(qly.HocKy.Oid);
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            qly = View.CurrentObject as QuanLyHeSo;
            if (_source != null)
            {
                foreach (var item in _source.ListDanhSach)
                {
                    if (item.Chon)
                    {
                        SqlParameter[] param = new SqlParameter[10];
                        param[0] = new SqlParameter("@NhanVien", item.NhanVien);
                        param[1] = new SqlParameter("@LoaiMonHoc", item.LoaiMonHoc);
                        param[2] = new SqlParameter("@BacDaoTao", item.BacDaoTao);
                        param[3] = new SqlParameter("@HeDaoTao", item.HeDaoTao);
                        param[4] = new SqlParameter("@MaMonhoc", item.MaMonhoc);
                        param[5] = new SqlParameter("@TenMonHoc", item.TenMonHoc);
                        param[6] = new SqlParameter("@LopHocPhan", item.LopHocPhan);
                        param[7] = new SqlParameter("@LopSinhVien", item.LopSinhVien);
                        param[8] = new SqlParameter("@HeSo", item.HeSo);
                        param[9] = new SqlParameter("@QuanLyHeSo", qly.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_HUFLIT_InserHeSoChucDanhMonHoc", CommandType.StoredProcedure, param);
                    }
                }
            }
        }
    }
}