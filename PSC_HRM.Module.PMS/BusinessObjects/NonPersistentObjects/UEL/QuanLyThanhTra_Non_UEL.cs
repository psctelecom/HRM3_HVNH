using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL
{
    [DefaultClassOptions]
    [NonPersistent]
    [ModelDefault("Caption","Quản lý thanh tra")]
    public class QuanLyThanhTra_Non_UEL : BaseObject
    {
        private Guid _QuanLyThanhTra;
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;
        private DateTime _TuNgay;
        private DateTime _DenNgay;


        [ModelDefault("Caption","Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { 
                    SetPropertyValue("NhanVien", ref _NhanVien,value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { 
                    SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption","Từ ngày")]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set { 
                    SetPropertyValue("TuNgay", ref _TuNgay, value); 
                }
        }


        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set { 
                SetPropertyValue("DenNgay", ref _DenNgay, value); 
            }
        }

        [ModelDefault("Caption", "Chi tiết giảng dạy")]
        public XPCollection<ChiTietKhoiLuongThanhTra_Non_UEL> listChiTiet
        {
            get;
            set;
        }
        

        public QuanLyThanhTra_Non_UEL(Session session)
            : base(session)
        {
        }
        public QuanLyThanhTra_Non_UEL(Session session,Guid _QuanLyThanhTra)
            : base(session)
        {
            this._QuanLyThanhTra = _QuanLyThanhTra;
        }

        public int LoadChiTietThanhTra()
        {
            int KQ = 1;
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
            {
                using (DialogUtil.AutoWait("Đang lấy danh sách chi tiết khối lượng giảng dạy"))
                {
                    listChiTiet = new XPCollection<ChiTietKhoiLuongThanhTra_Non_UEL>(Session, false);
                    SqlParameter[] parameter = new SqlParameter[5];
                    parameter[0] = new SqlParameter("@QuanLyThanhTra", _QuanLyThanhTra);
                    parameter[1] = new SqlParameter("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                    parameter[2] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                    parameter[3] = new SqlParameter("@TuNgay", TuNgay != null ? TuNgay.ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    parameter[4] = new SqlParameter("@DenNgay", DenNgay != null ? DenNgay.ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    DataTable dt = DataProvider.GetDataTable("spd_PMS_Load_CapNhatThanhTraGioGiang", System.Data.CommandType.StoredProcedure, parameter);
                    if (dt != null)
                    {
                        try
                        {

                            foreach (DataRow item in dt.Rows)
                            {
                                ChiTietKhoiLuongThanhTra_Non_UEL temp = new ChiTietKhoiLuongThanhTra_Non_UEL(Session);
                                temp.OidChiTiet = Guid.Parse(item["Oid"].ToString());
                                if (!string.IsNullOrEmpty(item["BoPhan"].ToString()))
                                {
                                    temp.BoPhan = Session.GetObjectByKey<BoPhan>(Guid.Parse(item["BoPhan"].ToString()));
                                }
                                temp.NhanVien = Session.GetObjectByKey<NhanVien>(Guid.Parse(item["NhanVien"].ToString()));
                                temp.TenMonHoc = item["TenMonHoc"].ToString();
                                temp.MaHocPhan = item["MaHocPhan"].ToString();
                                temp.LopHocPhan = item["LopHocPhan"].ToString();
                                temp.TietBD = int.Parse(item["TietBatDau"].ToString());
                                temp.TietKT = int.Parse(item["TietKetThuc"].ToString());
                                temp.SoLuongSV = int.Parse(item["SoLuongSV"].ToString());
                                temp.LopSinhVien = item["MaLopSV"].ToString();
                                temp.Thu = GetDayOfWeek(item["Thu"].ToString());
                                temp.NgayDay = DateTime.Parse(item["NgayDay"].ToString());
                                temp.SoTietThucDay = decimal.Parse(item["SoTietThucDay"].ToString());
                                temp.CoSoGiangDay = item["CoSoGiangDay"].ToString();
                                temp.SoTietGhiNhan = decimal.Parse(item["SoTietGhiNhan"].ToString());
                                temp.GhiChu = item["GhiChu"].ToString();
                                temp.DaThanhTra = bool.Parse(item["DaThanhTra"].ToString());
                                listChiTiet.Add(temp);
                            }
                        }
                        catch (Exception ex)
                        {
                            string Loi = "LỖI !!! . " + ex.ToString();
                            return 0;
                        }
                    }
                }
            }
            else
            {
                KQ  = 0;
            }
            return KQ;
        }

        public DayOfWeek GetDayOfWeek(string number)
        {
            DayOfWeek temp;
            switch (number)
            {
                case "0":
                    temp = DayOfWeek.Sunday;
                    break;
                case "1":
                    temp = DayOfWeek.Monday;
                    break;
                case "2":
                    temp = DayOfWeek.Tuesday;
                    break;
                case "3":
                    temp = DayOfWeek.Wednesday;
                    break;
                case "4":
                    temp = DayOfWeek.Thursday;
                    break;
                case "5":
                    temp = DayOfWeek.Friday;
                    break;
                case "6":
                    temp = DayOfWeek.Saturday;
                    break;
                default:
                    temp = DayOfWeek.Sunday;
                    break;
            }


            return temp;
        }
    }
}