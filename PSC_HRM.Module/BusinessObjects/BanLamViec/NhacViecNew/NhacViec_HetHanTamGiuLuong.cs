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
    [ModelDefault("Caption", "Nhắc việc - Hết hạn tạm giữ lương")]
    public class NhacViec_HetHanTamGiuLuong : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn tạm giữ lương")]
        public XPCollection<NhacViec_ChiTietHetHanTamGiuLuong> ChiTietHetHanTamGiuLuongList
        { get; set; }


        public NhacViec_HetHanTamGiuLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
             && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiHetHanTamGiuLuong)
            {
                ChiTietHetHanTamGiuLuongList = new XPCollection<NhacViec_ChiTietHetHanTamGiuLuong>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanTamGiuLuong);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachHetHanTamGiuLuong", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanTamGiuLuong obj = new NhacViec_ChiTietHetHanTamGiuLuong(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.TinhTrang = nhanVien.TinhTrang;
                                    obj.NgayTamGiuLuong = Convert.ToDateTime(item["NgayTamGiuLuong"].ToString());
                                    obj.NgayHuongLuongTroLai = Convert.ToDateTime(item["NgayHuongLuongTroLai"].ToString());
                                    obj.GhiChu = "Hưởng lương trở lại vào ngày " + obj.NgayHuongLuongTroLai.ToString("d");
                                }
                            }
                            //
                            ChiTietHetHanTamGiuLuongList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
