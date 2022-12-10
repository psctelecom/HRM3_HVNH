using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo tổng thanh toán thỉnh giảng(VHU)")]
    public class PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BoPhan _DonVi;
        private LoaiNhanVien _LoaiNhanVien;

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
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

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan DonVi
        {
            get { return _DonVi; }
            set { SetPropertyValue("DonVi", ref _DonVi, value); }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiNhanVien LoaiNhanVien
        {
            get { return _LoaiNhanVien; }
            set { SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value); }
        }

        [ModelDefault("Caption", "Danh sác")]
        public XPCollection<Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non> ListDanhSach { get; set; }

        public PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }


        public void LoadData()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ThongTinTruong", ThongTinTruong.Oid);
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);
            param[3] = new SqlParameter("@DonVi", HamDungChung.GetPhanQuyenBoPhan());
            param[4] = new SqlParameter("@LoaiNhanVien", LoaiNhanVien != null ? LoaiNhanVien.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThanhToanThuLaoGiangDay_CT_Tong", CommandType.StoredProcedure, param);
            DataSet dataset = DataProvider.GetDataSet(cmd);
            if (dataset != null)
            {
                DataTable dt = dataset.Tables[0];
                if (ListDanhSach != null)
                    ListDanhSach.Reload();
                else
                    ListDanhSach = new XPCollection<Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non>(Session, false);
                foreach (DataRow itemRow in dt.Rows)
                {
                    Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non ds = new Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non(Session);
                    ds.MaGV = itemRow["MaNhanVien"].ToString();
                    ds.DonVi = itemRow["TenBoPhan"].ToString();
                    ds.HoTen = itemRow["HoTen"].ToString();
                    ds.DonGia = Convert.ToDecimal(itemRow["DonGia"].ToString());
                    ds.ThanhTien = Convert.ToDecimal(itemRow["ThanhTienTong"].ToString());
                    ds.ThueTNCN = Convert.ToDecimal(itemRow["ThueTNCN"].ToString());
                    ds.ThucNhan = Convert.ToDecimal(itemRow["ThucNhan"].ToString());
                    ds.MaSoThue = itemRow["MaSoThue"].ToString();
                    ds.SoTaiKhoan = itemRow["SoTaiKhoan"].ToString();
                    ds.NganHang = itemRow["TenNganHang"].ToString();
                    ds.TenNamhoc = itemRow["TenNamhoc"].ToString();
                    ListDanhSach.Add(ds);

                }
            }
        }
    }

}