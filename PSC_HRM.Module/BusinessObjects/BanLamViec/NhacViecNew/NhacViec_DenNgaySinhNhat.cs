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
    [ModelDefault("Caption", "Nhắc việc - Đến ngày sinh nhật")]
    public class NhacViec_DenNgaySinhNhat : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách sinh nhật cán bộ")]
        public XPCollection<NhacViec_ChiTietDenNgaySinhNhat> ChiTietDenNgaySinhNhatList
        { get; set; }


        public NhacViec_DenNgaySinhNhat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
               && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiSinhNhat)
            {
            ChiTietDenNgaySinhNhatList = new XPCollection<NhacViec_ChiTietDenNgaySinhNhat>(Session, false);
            //
            DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
            DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiDenSinhNhat);
            //
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNgay", tuNgay);
            param[1] = new SqlParameter("@DenNgay", denNgay);
            //
            using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachSinhNhatCanBo", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NhacViec_ChiTietDenNgaySinhNhat obj = new NhacViec_ChiTietDenNgaySinhNhat(Session);
                        if (!item.IsNull("ThongTinNhanVien"))
                        {
                            ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                            if (nhanVien != null)
                            {
                                obj.ThongTinNhanVien = nhanVien;
                                obj.BoPhan = nhanVien.BoPhan;
                                obj.GioiTinh = nhanVien.GioiTinh;
                                obj.NgaySinh = nhanVien.NgaySinh;
                                obj.NgaySinhNhat = Convert.ToDateTime(nhanVien.NgaySinh.Day + "/" + nhanVien.NgaySinh.Month + "/" + HamDungChung.GetServerTime().Year);
                                obj.GhiChu = "Sinh nhật cán bộ ngày " + obj.NgaySinhNhat.ToString("d");
                            }
                        }
                        //
                        ChiTietDenNgaySinhNhatList.Add(obj);
                    }
                }
            }
            }
        }
    }

}
