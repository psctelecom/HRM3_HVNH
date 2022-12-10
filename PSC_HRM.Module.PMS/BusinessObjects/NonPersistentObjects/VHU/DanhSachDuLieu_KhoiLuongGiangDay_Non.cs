using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class DanhSachDuLieu_KhoiLuongGiangDay_Non : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private LoaiNhanVien _LoaiNhanVien;

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (value != null)
                {
                    HocKy = null;
                }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiNhanVien LoaiNhanVien
        {
            get { return _LoaiNhanVien; }
            set { SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value); }
        }

        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietDuLieu_KhoiLuongGiangDay_Non> ListDanhSach { get; set; }

        public DanhSachDuLieu_KhoiLuongGiangDay_Non(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        public void LoadData()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);
            param[2] = new SqlParameter("@LoaiNhanVien", LoaiNhanVien != null ? LoaiNhanVien.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_PMS_VHU_DuLieuKhoiLuongGiangDay_TongHop", CommandType.StoredProcedure, param);
            DataSet dataset = DataProvider.GetDataSet(cmd);
            if (dataset != null)
            {
                DataTable dt = dataset.Tables[0];
                if (ListDanhSach != null)
                    ListDanhSach.Reload();
                else
                    ListDanhSach = new XPCollection<ChiTietDuLieu_KhoiLuongGiangDay_Non>(Session, false);
                foreach (DataRow itemRow in dt.Rows)
                {
                    ChiTietDuLieu_KhoiLuongGiangDay_Non ds = new ChiTietDuLieu_KhoiLuongGiangDay_Non(Session);
                    ds.HocKy = itemRow["HocKy"].ToString();
                    ds.BoPhan = itemRow["BoPhan"].ToString();
                    ds.NhanVien = itemRow["NhanVien"].ToString();
                    ds.ChucDanh = itemRow["ChucDanh"].ToString();
                    ds.HocHam = itemRow["HocHam"].ToString();
                    ds.TrinhDoChuyenMon = itemRow["TrinhDoChuyenMon"].ToString();//Học vị
                    ds.TenMonHoc = itemRow["TenMonHoc"].ToString();
                    ds.ChuyenNganh = Convert.ToBoolean(itemRow["ChuyenNganh"].ToString());
                    ds.LoaiChuongTrinh = itemRow["LoaiChuongTrinh"].ToString();
                    ds.MaHocPhan = itemRow["MaHocPhan"].ToString();
                    ds.LopHocPhan = itemRow["LopHocPhan"].ToString();
                    ds.CoXepTKB = Convert.ToBoolean(itemRow["CoXepTKB"].ToString());
                    ds.MaLopSV = itemRow["MaLopSV"].ToString();
                    ds.TenLopSV = itemRow["TenLopSV"].ToString();
                    ds.SoTinChi = Convert.ToDecimal(itemRow["SoTinChi"].ToString());
                    ds.SoLuongSV = Convert.ToInt32(itemRow["SoLuongSV"].ToString());
                    //ds.Thu = itemRow["Thu"].ToString() ;
                    //ds.TietBD = Convert.ToInt32(itemRow["TietBD"].ToString());
                    //ds.TietKT = Convert.ToInt32(itemRow["TietKT"].ToString());
                    //ds.NgayBD = Convert.ToDateTime(itemRow["NgayBD"].ToString());
                    //ds.NgayKT = Convert.ToDateTime(itemRow["NgayKT"].ToString());
                    ds.SoTietThucDay = Convert.ToDecimal(itemRow["SoTietThucDay"].ToString());
                    ds.LoaiHocPhan = itemRow["LoaiHocPhan"].ToString();
                    ds.LoaiMonHoc = itemRow["LoaiMonHoc"].ToString();
                    ds.NgonNguGiangDay = itemRow["NgonNguGiangDay"].ToString();
                    ds.GioGiangDay = itemRow["GioGiangDay"].ToString();
                    ds.BacDaoTao = itemRow["BacDaoTao"].ToString();
                    ds.HeDaoTao = itemRow["HeDaoTao"].ToString();
                    ds.CoSoGiangDay = itemRow["CoSoGiangDay"].ToString();
                    ds.PhongHoc = itemRow["PhongHoc"].ToString();
                    ds.HeSo_ChucDanh = Convert.ToDecimal(itemRow["HeSo_ChucDanh"].ToString());
                    ds.HeSo_LopDong = Convert.ToDecimal(itemRow["HeSo_LopDong"].ToString());
                    ds.HeSo_DaoTao = Convert.ToDecimal(itemRow["HeSo_DaoTao"].ToString());
                    ds.HeSo_CoSo = Convert.ToDecimal(itemRow["HeSo_CoSo"].ToString());
                    ds.HeSo_GiangDayNgoaiGio = Convert.ToDecimal(itemRow["HeSo_GiangDayNgoaiGio"].ToString());
                    ds.HeSo_TinChi = Convert.ToDecimal(itemRow["HeSo_TinChi"].ToString());
                    ds.HeSo_TNTH = Convert.ToDecimal(itemRow["HeSo_TNTH"].ToString());
                    ds.HeSo_BacDaoTao = Convert.ToDecimal(itemRow["HeSo_BacDaoTao"].ToString());
                    ds.HeSo_NgonNgu = Convert.ToDecimal(itemRow["HeSo_NgonNgu"].ToString());
                    ds.TongHeSo = Convert.ToDecimal(itemRow["TongHeSo"].ToString());
                    ds.TongGio = Convert.ToDecimal(itemRow["TongGio"].ToString());
                    ds.LoaiHopDong = itemRow["LoaiHopDong"].ToString();

                    ListDanhSach.Add(ds);

                }
            }
        }
    }

}