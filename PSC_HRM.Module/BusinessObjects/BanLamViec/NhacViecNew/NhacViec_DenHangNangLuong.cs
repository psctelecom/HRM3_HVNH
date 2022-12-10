using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.Persistent.BaseImpl;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.BanLamViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nhắc việc - Đến hạng nâng lương")]
    public class NhacViec_DenHangNangLuong : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đến hạn nâng")]
        public XPCollection<NhacViec_ChiTietDenHangNangLuong> ChiTietDenHangNangLuongList
        { get; set; }
        

        public NhacViec_DenHangNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
               && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiDenHanNangLuong)
            {
                ChiTietDenHangNangLuongList = new XPCollection<NhacViec_ChiTietDenHangNangLuong>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiNangLuong);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachDenHanNangLuong", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietDenHangNangLuong obj = new NhacViec_ChiTietDenHangNangLuong(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.NgayHuongLuong = nhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                                    obj.MocNangLuong = nhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh != DateTime.MinValue ? nhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh : nhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;
                                    obj.NgachLuong = nhanVien.NhanVienThongTinLuong.NgachLuong;
                                    obj.BacLuong = nhanVien.NhanVienThongTinLuong.BacLuong;
                                    obj.HeSoLuong = nhanVien.NhanVienThongTinLuong.HeSoLuong;
                                    obj.PhanTramVuotKhung = nhanVien.NhanVienThongTinLuong.VuotKhung;
                                    obj.GhiChu = "Đến hạn nâng lương vào ngày " + obj.MocNangLuong.ToString("d"); ;
                                }
                            }

                            ChiTietDenHangNangLuongList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
