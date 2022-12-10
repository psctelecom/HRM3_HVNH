using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhThoiViec : IMailMerge<IList<QuyetDinhThoiViec>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThoiViec> qdList)
        {
            var list = new List<Non_QuyetDinhThoiViec>();
            Non_QuyetDinhThoiViec qd;
            foreach (QuyetDinhThoiViec quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThoiViec();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("d");
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("d");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NgaySinhDate = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("d");
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NoiSinh = quyetDinh.ThongTinNhanVien.NoiSinh != null ? quyetDinh.ThongTinNhanVien.NoiSinh.TinhThanh.TenTinhThanh : "";
                TaiKhoanNganHang TaiKhoan = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien = ? and TaiKhoanChinh", quyetDinh.ThongTinNhanVien.Oid));
                if (TaiKhoan != null)
                {
                    qd.TKNganHang = TaiKhoan.SoTaiKhoan;
                    qd.NganHang = TaiKhoan.NganHang.TenNganHang;
                }

                qd.NgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                qd.BacLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong.TenBacLuong : "";
                qd.HeSoLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";              
                qd.SoCMND = quyetDinh.ThongTinNhanVien.CMND;
                qd.TinhTroCapTuNgay = quyetDinh.TinhTroCapTuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.TinhTroCapTuNgayMonth = quyetDinh.TinhTroCapTuNgay.ToString("MM/yyyy");
                qd.TinhTroCapDenNgay = quyetDinh.TinhTroCapDenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.TinhTroCapDenNgayMonth = quyetDinh.TinhTroCapDenNgay.ToString("MM/yyyy");
                if (quyetDinh.TinhTroCapTuNgay != DateTime.MinValue && quyetDinh.TinhTroCapDenNgay != DateTime.MinValue)
                    qd.SoNamTroCap = (Math.Round((decimal)(quyetDinh.TinhTroCapDenNgay.Subtract(quyetDinh.TinhTroCapTuNgay).Days / 365.2425), 1)).ToString("N1");
                qd.MucLuongHienHuong = quyetDinh.MucLuongHienHuong.ToString("N0");
                qd.SoTienTroCap = quyetDinh.SoTienTroCap.ToString("N0");
                qd.SoTienTroCapBangChu = HamDungChung.DocTien(quyetDinh.SoTienTroCap);
                qd.NgayVaoCoQuan = quyetDinh.ThongTinNhanVien.NgayVaoCoQuan.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NghiViecTuNgay = quyetDinh.NghiViecTuNgay != DateTime.MinValue ? quyetDinh.NghiViecTuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NgayXinNghi = quyetDinh.NgayXinNghi != DateTime.MinValue ? quyetDinh.NgayXinNghi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TraLuongDenNgay = quyetDinh.TraLuongDenHetNgay != DateTime.MinValue ? quyetDinh.TraLuongDenHetNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.ThoiHanBanGiaoCongViec = quyetDinh.ThoiHanBanGiaoCongViec != DateTime.MinValue ? quyetDinh.ThoiHanBanGiaoCongViec.ToString("d") : "";
                qd.LyDo = quyetDinh.LyDo;
                qd.SoBHXH = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.SoSoBHXH;
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhThoiViec.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThoiViec>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thôi việc trong hệ thống.");
        }
    }
}
