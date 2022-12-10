using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhChoPhepNghiHocTamThoi : IMailMerge<IList<QuyetDinhChoPhepNghiHocTamThoi>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChoPhepNghiHocTamThoi> qdList)
        {
            var list = new List<Non_QuyetDinhChoPhepNghiHocTamThoiCaNhan>();
            Non_QuyetDinhChoPhepNghiHocTamThoiCaNhan qd;
            foreach (QuyetDinhChoPhepNghiHocTamThoi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChoPhepNghiHocTamThoiCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                qd.TuNgay = quyetDinh.NghiTuNgay != DateTime.MinValue ? quyetDinh.NghiTuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.NghiTuNgay != DateTime.MinValue ? quyetDinh.NghiTuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgay = quyetDinh.NghiDenNgay != DateTime.MinValue ? quyetDinh.NghiDenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgayDate = quyetDinh.NghiDenNgay != DateTime.MinValue ? quyetDinh.NghiDenNgay.ToString("dd/MM/yyyy") : "";
                
                qd.SoQuyetDinhQDDT = quyetDinh.QuyetDinhDaoTao.SoQuyetDinh;
                qd.NgayQuyetDinhQDDTDate = quyetDinh.QuyetDinhDaoTao.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CoQuanQuyetDinhQDDT = quyetDinh.QuyetDinhDaoTao.TenCoQuan; 
                qd.HinhThucDaoTaoQDDT = quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao != null ? quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao.TenHinhThucDaoTao : "";
                qd.HinhThucDaoTaoQDDT += quyetDinh.QuyetDinhDaoTao.DaoTaoTapTrung ? " (Tập trung)" : " (Không tập trung)";
                qd.TruongDaoTaoQDDT = quyetDinh.QuyetDinhDaoTao.TruongDaoTao != null ? quyetDinh.QuyetDinhDaoTao.TruongDaoTao.TenTruongDaoTao : "";
                qd.QuocGiaQDDT = quyetDinh.QuyetDinhDaoTao.QuocGia != null ? quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia : "";
                qd.TrinhDoChuyenMonQDDT = GetTrinhDo(quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon);
                qd.NganhDaoTaoQDDT = quyetDinh.QuyetDinhDaoTao.NganhDaoTao != null ? quyetDinh.QuyetDinhDaoTao.NganhDaoTao.TenNganhDaoTao : "";
                qd.KhoaDaoTaoQDDT = quyetDinh.QuyetDinhDaoTao.KhoaDaoTao != null ? quyetDinh.QuyetDinhDaoTao.KhoaDaoTao.TenKhoaDaoTao : "";
                qd.NguonKinhPhiQDDT = quyetDinh.QuyetDinhDaoTao.NguonKinhPhi != null ? quyetDinh.QuyetDinhDaoTao.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTroQDDT = !String.IsNullOrEmpty(quyetDinh.QuyetDinhDaoTao.TruongHoTro) ? quyetDinh.QuyetDinhDaoTao.TruongHoTro : "";
                qd.TuNgayQDDTDate = quyetDinh.QuyetDinhDaoTao.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayQDDTDate = quyetDinh.QuyetDinhDaoTao.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.LoaiQDDT = quyetDinh.QuyetDinhDaoTao.QuocGia != null ?
                                (quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia.Equals("Việt Nam") == true ? "trong nước" : "ngoài nước")
                                : "";

                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.MaNgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                qd.HeSoLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
                qd.NgaySinhDate = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                qd.ChuyenNganhDaoTaoQDDT = quyetDinh.ChuyenMonDaoTao != null ? quyetDinh.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";

                if (quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                    qd.DanhXungVietThuong = "ông";
                else
                    qd.DanhXungVietThuong = "bà";
                if (quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                    qd.DanhXungVietHoa = "Ông";
                else
                    qd.DanhXungVietHoa = "Bà";
                
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhChoPhepNghiHocTamThoi.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChoPhepNghiHocTamThoiCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ cho phép nghỉ học tạm thời trong hệ thống.");

        }

        private string GetTrinhDo(TrinhDoChuyenMon trinhDo)
        {
            if (trinhDo != null)
            {
                if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("tiến"))
                    return "Nghiên cứu sinh";
                else if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("thạc"))
                    return "Cao học";
                else
                    return "Cử nhân";
            }
            return "";
        }
        
    }
}
