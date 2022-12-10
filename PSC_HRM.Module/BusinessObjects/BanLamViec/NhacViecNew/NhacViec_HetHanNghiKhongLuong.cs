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
    [ModelDefault("Caption", "Nhắc việc - Hết hạn nghỉ không lương")]
    public class NhacViec_HetHanNghiKhongLuong : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn nghỉ không lương")]
        public XPCollection<NhacViec_ChiTietHetHanNghiKhongLuong> ChiTietHetHanNghiKhongLuongList
        { get; set; }


        public NhacViec_HetHanNghiKhongLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
             && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiHetHanNghiKhongHuongLuong)
            {
            ChiTietHetHanNghiKhongLuongList = new XPCollection<NhacViec_ChiTietHetHanNghiKhongLuong>(Session, false);
            //
            DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
            DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanNghiKhongLuong);
            //
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNgay", tuNgay);
            param[1] = new SqlParameter("@DenNgay", denNgay);
            //
            using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachHetHanNghiKhongLuong", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NhacViec_ChiTietHetHanNghiKhongLuong obj = new NhacViec_ChiTietHetHanNghiKhongLuong(Session);
                        if (!item.IsNull("ThongTinNhanVien"))
                        {
                            ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                            if (nhanVien != null)
                            {
                                QuyetDinh.QuyetDinh quyetDinh = Session.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("Oid=?", new Guid(item["QuyetDinh"].ToString())));
                                if (quyetDinh != null)
                                {
                                    obj.QuyetDinh = quyetDinh;
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.TinhTrang = nhanVien.TinhTrang;
                                    obj.NgayNghiKhongLuong = Convert.ToDateTime(item["NgayNghiKhongLuong"].ToString());
                                    obj.NgayTroLaiLamViec = Convert.ToDateTime(item["NgayTroLaiLamViec"].ToString());
                                    obj.GhiChu = "Trở lại làm việc vào ngày " + obj.NgayTroLaiLamViec.ToString("d");
                                }
                            }
                        }
                        //
                        ChiTietHetHanNghiKhongLuongList.Add(obj);
                    }
                }
            }
            }
        }
    }

}
