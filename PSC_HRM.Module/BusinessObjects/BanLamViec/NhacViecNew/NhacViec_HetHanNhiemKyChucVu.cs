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
    [ModelDefault("Caption", "Nhắc việc - Hết hạn nhiệm kỳ chức vụ")]
    public class NhacViec_HetHanNhiemKyChucVu : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn nhiệm kỳ chức vụ")]
        public XPCollection<NhacViec_ChiTietHetHanNhiemKyChucVu> ChiTietHetHanNhiemKyChucVuList
        { get; set; }


        public NhacViec_HetHanNhiemKyChucVu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
            && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiHetNhiemKyChucVu)
            {
                ChiTietHetHanNhiemKyChucVuList = new XPCollection<NhacViec_ChiTietHetHanNhiemKyChucVu>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetNhiemKyChucVu);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachHetHanNhiemKyChucVu", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanNhiemKyChucVu obj = new NhacViec_ChiTietHetHanNhiemKyChucVu(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    //
                                    QuyetDinh.QuyetDinh quyetDinh = Session.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("Oid=?", new Guid(item["QuyetDinh"].ToString())));
                                    if (quyetDinh != null)
                                    {
                                        obj.QuyetDinh = quyetDinh;
                                        obj.ThongTinNhanVien = nhanVien;
                                        obj.BoPhan = nhanVien.BoPhan;
                                        obj.ChucVu = nhanVien.ChucVu;
                                        obj.NgayBoNhiemChucVu = Convert.ToDateTime(item["NgayBoNhiemChucVu"].ToString());
                                        obj.NgayHetHanNhiemKy = Convert.ToDateTime(item["NgayHetHanNhiemKy"].ToString());
                                        obj.GhiChu = "Hết hạn nhiệm kỳ ngày " + obj.NgayHetHanNhiemKy.ToString("d");
                                    }
                                }
                            }
                            //
                            ChiTietHetHanNhiemKyChucVuList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
