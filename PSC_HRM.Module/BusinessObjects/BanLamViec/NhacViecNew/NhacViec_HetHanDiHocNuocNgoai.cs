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
    [ModelDefault("Caption", "Nhắc việc - Hết hạn đi học nước ngoài")]
    public class NhacViec_HetHanDiHocNuocNgoai : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn đi học")]
        public XPCollection<NhacViec_ChiTietHetHanDiHocNuocNgoai> ChiTietHetHanDiHocNuocNgoaiList
        { get; set; }


        public NhacViec_HetHanDiHocNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
             && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiDiHocNuocNgoai)
            {
                ChiTietHetHanDiHocNuocNgoaiList = new XPCollection<NhacViec_ChiTietHetHanDiHocNuocNgoai>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanDiHoc);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachHetHanDiHocNuocNgoai", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanDiHocNuocNgoai obj = new NhacViec_ChiTietHetHanDiHocNuocNgoai(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    QuyetDinh.QuyetDinh quyetDinh = Session.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("Oid=?", new Guid(item["QuyetDinh"].ToString())));
                                    if (quyetDinh != null)
                                    {
                                        obj.ThongTinNhanVien = nhanVien;
                                        obj.BoPhan = nhanVien.BoPhan;
                                        obj.QuyetDinh = quyetDinh;
                                        obj.TinhTrang = nhanVien.TinhTrang;
                                        obj.TuNgay = Convert.ToDateTime(item["TuNgay"].ToString());
                                        obj.DenNgay = Convert.ToDateTime(item["DenNgay"].ToString());
                                        obj.GhiChu = "Hết hạn đi học ngày " + obj.DenNgay.ToString("d");
                                    }
                                }
                            }
                            //
                            ChiTietHetHanDiHocNuocNgoaiList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
