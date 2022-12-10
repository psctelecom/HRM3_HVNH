using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhBoNhiem : IMailMerge<IList<QuyetDinhBoNhiem>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhBoNhiem> qdList)
        {
            var list = new List<Non_QuyetDinhBoNhiem>();
            Non_QuyetDinhBoNhiem qd;
            foreach (QuyetDinhBoNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoNhiem();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("d");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : "";
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.HocHam = HamDungChung.GetHocVi(quyetDinh.ThongTinNhanVien);
                
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.ChucDanhNhanVien = quyetDinh.ThongTinNhanVien.ChucDanh != null ? quyetDinh.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.TaiBoMon = quyetDinh.TaiBoMon != null ? quyetDinh.TaiBoMon.TenBoPhan : "";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.ChucVuCu = quyetDinh.ChucVuCu != null ? quyetDinh.ChucVuCu.TenChucVu : "";
                qd.ChucVu = quyetDinh.ChucVuMoi != null ? quyetDinh.ChucVuMoi.TenChucVu : "";
                qd.HSPCChucVuMoi = quyetDinh.HSPCChucVuMoi.ToString("N2");
                qd.DonViMoi = quyetDinh.BoPhanMoi!= null ? quyetDinh.BoPhanMoi.TenBoPhan : "";
                qd.NgayHuongHSPCChucVu = quyetDinh.NgayHuongHeSoMoi != DateTime.MinValue ? quyetDinh.NgayHuongHeSoMoi.ToString("d") : "";
                qd.NgayHetNhiemKy = quyetDinh.NgayHetNhiemKy != DateTime.MinValue ? quyetDinh.NgayHetNhiemKy.ToString("d") : "";
                qd.SoNamNhiemKy = quyetDinh.NhiemKy.ToString("N2");
                qd.NamNhiemKy = quyetDinh.NamNhiemKy;
                
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoNhiem.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhBoNhiem>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bổ nhiệm trong hệ thống.");
        }
    }
}
