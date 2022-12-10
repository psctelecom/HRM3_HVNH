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
    public class MailMerge_QuyetDinhTiepNhanVaXepLuong : IMailMerge<IList<QuyetDinhTiepNhanVaXepLuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTiepNhanVaXepLuong> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhanVaXepLuong>();
            Non_QuyetDinhTiepNhanVaXepLuong qd;
            foreach (QuyetDinhTiepNhanVaXepLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanVaXepLuong();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NgayHuongLuong = quyetDinh.NgayHuongLuong.ToString("dd/MM/yyyy");
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                qd.MaNgach = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.MaQuanLy : "";
                qd.NgachLuong = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.TenNgachLuong : "";
                qd.BacLuong = quyetDinh.BacLuong != null ? quyetDinh.BacLuong.MaQuanLy : "";
                qd.HeSoLuong = quyetDinh.HeSoLuong.ToString("N2");
                qd.VuotKhung = quyetDinh.VuotKhung.ToString("N0");
                qd.MocNangLuong = quyetDinh.MocNangLuong != DateTime.MinValue ? quyetDinh.MocNangLuong.ToString("d") : "";
                qd.DonViCu = quyetDinh.DonViCu != null ? quyetDinh.DonViCu : "";
                qd.DonViMoi = quyetDinh.BoPhanMoi != null ? quyetDinh.BoPhanMoi.TenBoPhan : "";
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanVaXepLuong.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanVaXepLuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tiếp nhận và phân công công tác trong hệ thống.");
        }
    }
}
