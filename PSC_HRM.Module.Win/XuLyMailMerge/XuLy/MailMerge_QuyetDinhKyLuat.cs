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
    public class MailMerge_QuyetDinhKyLuat : IMailMerge<IList<QuyetDinhKyLuat>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhKyLuat> qdList)
        {
            var list = new List<Non_QuyetDinhKyLuat>();
            Non_QuyetDinhKyLuat qd;
            foreach (QuyetDinhKyLuat quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKyLuat();
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
                qd.NamQuyetDinh = quyetDinh.NgayHieuLuc.Year.ToString("####");
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc=quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.HinhThucKyLuat = quyetDinh.HinhThucKyLuat != null ? quyetDinh.HinhThucKyLuat.TenHinhThucKyLuat : "";
                qd.LyDo = quyetDinh.LyDo;
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("d") : "";
                qd.NgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                qd.MaNgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                
                qd.TruLuong = quyetDinh.TruLuongTangThem == true ? "Trừ lương tăng thêm" : "Không trừ lương tăng thêm";
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                if (TruongConfig.MaTruong == "BUH")
                {
                    qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ?
                                    quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu :
                                    quyetDinh.ThongTinNhanVien.ChucDanh != null ?
                                        quyetDinh.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                }
                
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhKyLuat.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKyLuat>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ kỷ luật trong hệ thống.");
        }
    }
}
