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
    [ModelDefault("Caption", "Nhắc việc - Đến hạng nâng thâm niên hành chính")]
    public class NhacViec_DenHanNangThamNienHanhChinh : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đến hạn nâng thâm niên hành chính")]
        public XPCollection<NhacViec_ChiTietDenHangNangThamNienHanhChinh> ChiTietDenHangNangThamNienHanhChinhList
        { get; set; }


        public NhacViec_DenHanNangThamNienHanhChinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
               && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiDenHanNangThamNienHC)
            {
                ChiTietDenHangNangThamNienHanhChinhList = new XPCollection<NhacViec_ChiTietDenHangNangThamNienHanhChinh>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanNangThamNienHC);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachDenHanNangThamNienHanhChinh", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietDenHangNangThamNienHanhChinh obj = new NhacViec_ChiTietDenHangNangThamNienHanhChinh(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.NgayTinhThamNien = nhanVien.NhanVienThongTinLuong.NgayHuongThamNien.AddYears(5);
                                    obj.NgachLuong = nhanVien.NhanVienThongTinLuong.NgachLuong;
                                    obj.NgayHuongThamNien = nhanVien.NhanVienThongTinLuong.NgayHuongThamNien;
                                    obj.GhiChu = "Đến hạn nâng thâm niên ngày " + obj.NgayTinhThamNien.ToString("d");
                                }
                            }
                            //
                            ChiTietDenHangNangThamNienHanhChinhList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
