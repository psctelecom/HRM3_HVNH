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
    public class MailMerge_QuyetDinhTiepNhan : IMailMerge<IList<QuyetDinhTiepNhan>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTiepNhan> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhan>();
            Non_QuyetDinhTiepNhan qd;
            foreach (QuyetDinhTiepNhan quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                }
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NgayXinTiepNhan = quyetDinh.NgayXinTiepNhan.ToString("dd/MM/yyyy");

                if (quyetDinh.ThongTinNhanVien != null)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                    qd.ChucDanhVietThuong = HamDungChung.GetChucDanhVietThuong(quyetDinh.ThongTinNhanVien);
                    qd.ChucVuNhanVienVietThuong = HamDungChung.GetChucVuNhanVien(quyetDinh.ThongTinNhanVien).ToLower();
                    qd.ChucVuNhanVienVietHoa = HamDungChung.GetChucVuNhanVien(quyetDinh.ThongTinNhanVien);
                    qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                    qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                    qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.BoPhan);
                    qd.TenVietTatDonVi = quyetDinh.BoPhan.TenBoPhanVietTat;
                    qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                    qd.NgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                }

                if (quyetDinh.QuyetDinhNghiKhongHuongLuong != null)
                {
                    qd.SoQDNghiKhongLuong = quyetDinh.QuyetDinhNghiKhongHuongLuong.SoQuyetDinh;
                    qd.NgayNghiKhongLuong = quyetDinh.QuyetDinhNghiKhongHuongLuong.NgayQuyetDinh.ToString("d");
                    qd.TuNgayNghiKhongLuong = quyetDinh.QuyetDinhNghiKhongHuongLuong.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhNghiKhongHuongLuong.TuNgay.ToString("d") : "";
                    qd.DenNgayNghiKhongLuong = quyetDinh.QuyetDinhNghiKhongHuongLuong.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhNghiKhongHuongLuong.DenNgay.ToString("d") : "";
                    qd.SoThang = ((quyetDinh.QuyetDinhNghiKhongHuongLuong.DenNgay.Year * 12 + quyetDinh.QuyetDinhNghiKhongHuongLuong.DenNgay.Month)
                                        - (quyetDinh.QuyetDinhNghiKhongHuongLuong.TuNgay.Year * 12 + quyetDinh.QuyetDinhNghiKhongHuongLuong.TuNgay.Month)).ToString("00");
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tiếp nhận trong hệ thống.");
        }
    }
}
