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
    [ModelDefault("Caption", "Nhắc việc - Đến tuổi nghỉ hưu")]
    public class NhacViec_DenTuoiNghiHuu : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đến tuổi nghỉ hưu")]
        public XPCollection<NhacViec_ChiTietDenTuoiNghiHuu> ChiTietDenTuoiNghiHuuList
        { get; set; }


        public NhacViec_DenTuoiNghiHuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
              && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiDenTuoiNghiHuu)
            {
                ChiTietDenTuoiNghiHuuList = new XPCollection<NhacViec_ChiTietDenTuoiNghiHuu>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiNghiHuu);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachDenHanNghiHuu", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietDenTuoiNghiHuu obj = new NhacViec_ChiTietDenTuoiNghiHuu(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.GioiTinh = nhanVien.GioiTinh;
                                    obj.NgaySinh = nhanVien.NgaySinh;
                                    obj.NgayNghiHuu = Convert.ToDateTime(item["NgayNghiHuu"].ToString());
                                    obj.GhiChu = "Sẽ nghỉ hưu vào ngày " + obj.NgayNghiHuu.ToString("d");
                                }
                            }
                            //
                            ChiTietDenTuoiNghiHuuList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
