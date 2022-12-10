using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.NghiepVu;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.ThanhTra
{
    [NonPersistent]
    [ModelDefault("Caption", "Thanh tra giảng dạy")]
    [Appearance("Hide_ThanhTraGiangDay_VHU", TargetItems = "TuNgay;DenNgay"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "Quanlythanhtra.ThongTinTruong.TenVietTat = 'VHU'")]
    public class ThanhTraGiangDay : BaseObject
    {
        private Quanlythanhtra _Quanlythanhtra;
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý")]
        public Quanlythanhtra Quanlythanhtra
        {
            get { return _Quanlythanhtra; }
            set
            {
                SetPropertyValue("Quanlythanhtra", ref _Quanlythanhtra, value);
                if(value != null)
                {
                    TuNgay = Quanlythanhtra.NamHoc.NgayBatDau;
                    DenNgay = Quanlythanhtra.NamHoc.NgayKetThuc;
                }
            }   
        }

        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                    if (BoPhan != null)
                    {
                        LoadDanhSach();
                    }
            }
        }
        [ModelDefault("Caption", "Giảng viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading)
                    if (NhanVien != null)
                    {
                        LoadDanhSach();
                    }
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);              
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }
        [ModelDefault("Caption", "Chi tiết giảng dạy")]
        public XPCollection<ChiTietThanhTraGiangDay> listChiTiet
        {
            get;
            set;
        }
        public ThanhTraGiangDay()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public ThanhTraGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        void LoadNhanVien()
        {

        }
        void LoadDanhSach()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;

            if (Quanlythanhtra.ThongTinTruong.TenVietTat != "HUFLIT")
            {
                cmd = new SqlCommand("spd_PMS_LayDanhSach_KhoiLuongGiangDay_ThanhTra", DataProvider.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1800;
                cmd.Parameters.AddWithValue("@KhoiLuongGiangDay", Quanlythanhtra.Oid);
                cmd.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
                cmd.Parameters.AddWithValue("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
            }
            else
            {
                {
                    cmd = new SqlCommand("spd_PMS_LayDanhSach_KhoiLuongGiangDay_ThanhTra", DataProvider.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1800;
                    cmd.Parameters.AddWithValue("@KhoiLuongGiangDay", Quanlythanhtra.Oid);
                    cmd.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
                    cmd.Parameters.AddWithValue("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                    cmd.Parameters.AddWithValue("@TuNgay", TuNgay != null ? TuNgay : DateTime.MinValue);
                    cmd.Parameters.AddWithValue("@DenNgay", DenNgay != null ? DenNgay : DateTime.MaxValue);
                }
            }

            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            if(dt!=null)
            {
                listChiTiet.Reload();
                foreach(DataRow item in dt.Rows)
                {
                    ChiTietThanhTraGiangDay ct = new ChiTietThanhTraGiangDay(Session);
                    ct.OidChiTiet = new Guid(item["Oid"].ToString());
                    ct.NhanVien = item["HoTen"].ToString();
                    ct.DonVi = item["DonVi"].ToString();
                    ct.TenMonHoc = item["TenMonHoc"].ToString();
                    ct.LoaiChuongTrinh = item["LoaiChuongTrinh"].ToString();
                    ct.MaHocPhan = item["MaHocPhan"].ToString();
                    ct.LopHocPhan = item["LopHocPhan"].ToString();
                    ct.SoTinChi = Convert.ToDecimal(item["SoTinChi"].ToString());
                    ct.SoTietThucDay = Convert.ToDecimal(item["SoTietThucDay"].ToString());
                    ct.SoLuongSV = Convert.ToInt32(item["SoLuongSV"].ToString()); 
                    int Thu = Convert.ToInt32(item["Thu"].ToString());
                    switch(Thu)
                    {
                        case 1:
                            ct.Thu = DayOfWeek.Monday;
                            break;
                        case 2:
                            ct.Thu = DayOfWeek.Tuesday;
                            break;
                        case 3:
                            ct.Thu = DayOfWeek.Wednesday;
                            break;
                        case 4:
                            ct.Thu = DayOfWeek.Thursday;
                            break;
                        case 5:
                            ct.Thu = DayOfWeek.Friday;
                            break;
                        case 6:
                            ct.Thu = DayOfWeek.Saturday;
                            break;
                        default:
                            ct.Thu = DayOfWeek.Saturday;
                            break;
                    }
                    ct.TietBD = Convert.ToInt32(item["TietBD"].ToString());
                    ct.TietKT = Convert.ToInt32(item["TietKT"].ToString());
                    ct.CoSoGiangDay = item["TenCoSo"].ToString();
                    ct.NgayBD = Convert.ToDateTime(item["NgayBD"].ToString());
                    ct.NgayKT = Convert.ToDateTime(item["NgayKT"].ToString());
                    listChiTiet.Add(ct);
                }
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            listChiTiet = new XPCollection<ChiTietThanhTraGiangDay>(Session,false);

        }
    }

}