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
    [ModelDefault("Caption", "Nhắc việc - Hết hạn hợp đồng")]
    public class NhacViec_HetHanHopDong : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn hợp đồng")]
        public XPCollection<NhacViec_ChiTietHetHanHopDong> ChiTietHetHanHopDongList
        { get; set; }


        public NhacViec_HetHanHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null && HamDungChung.CauHinhChung.CauHinhNhacViec != null
             && HamDungChung.CauHinhChung.CauHinhNhacViec.TheoDoiHetHanHopDong)
            {
                ChiTietHetHanHopDongList = new XPCollection<NhacViec_ChiTietHetHanHopDong>(Session, false);
                //
                DateTime tuNgay = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndMonth).AddMonths(HamDungChung.CauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanHopDong);
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_DanhSachHetHanHopDong", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanHopDong obj = new NhacViec_ChiTietHetHanHopDong(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.HopDongHienTai = nhanVien.HopDongHienTai;
                                    obj.NgayHetHan = Convert.ToDateTime(item["NgayHetHan"].ToString());
                                    obj.LoaiHopDong = item["LoaiHopDong"].ToString();
                                    obj.GhiChu = "Hết hạn hợp đồng ngày " + obj.NgayHetHan.ToString("d");
                                }
                            }
                            //
                            ChiTietHetHanHopDongList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
