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
    public class MailMerge_QuyetDinhXepLuong : IMailMerge<IList<QuyetDinhXepLuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhXepLuong> qdList)
        {
            var list = new List<Non_QuyetDinhXepLuong>();
            Non_QuyetDinhXepLuong qd;
            foreach (QuyetDinhXepLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhXepLuong();
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

                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.NhomNgachLuong = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.NhomNgachLuong.TenNhomNgachLuong : "";
                qd.MaNgachLuong = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.MaQuanLy : "";
                qd.NgachLuong = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.TenNgachLuong : "";
                qd.BacLuong = quyetDinh.BacLuong != null ? quyetDinh.BacLuong.TenBacLuong : "";
                qd.HeSoLuong = quyetDinh.HeSoLuong.ToString("N2");
                qd.VuotKhung = quyetDinh.VuotKhung.ToString();
                qd.MocNangLuong = quyetDinh.MocNangLuong != DateTime.MinValue ? quyetDinh.MocNangLuong.ToString("d") : "";
                qd.NgayHuongLuong = quyetDinh.NgayHuongLuong != DateTime.MinValue ? quyetDinh.NgayHuongLuong.ToString("d") : "";
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhXepLuong.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhXepLuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ xếp lương trong hệ thống.");
        }
    }
}
