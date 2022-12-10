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
    public class MailMerge_QuyetDinhChoPhepNghiHoc : IMailMerge<IList<QuyetDinhChoPhepNghiHoc>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChoPhepNghiHoc> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietChoPhepNghiHoc.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietChoPhepNghiHoc.Count > 1
                         select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ cho phép nghỉ học (nhiều người) trong hệ thống.");
            }
            
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChoPhepNghiHoc> qdList)
        {
            var list = new List<Non_QuyetDinhChoPhepNghiHocCaNhan>();
            Non_QuyetDinhChoPhepNghiHocCaNhan qd;
            foreach (QuyetDinhChoPhepNghiHoc quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChoPhepNghiHocCaNhan();
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

                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.LyDoNghiHoc = quyetDinh.LyDoNghiHoc;

                if (quyetDinh.QuyetDinhDaoTao != null)
                {
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
                }
                foreach (ChiTietChoPhepNghiHoc item in quyetDinh.ListChiTietChoPhepNghiHoc)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.HeSoLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
                    qd.NgaySinhDate = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChuyenNganhDaoTaoQDDT = item.ChuyenMonDaoTao != null ? item.ChuyenMonDaoTao.TenChuyenMonDaoTao : "" ;

                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhChoPhepNghiHoc.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChoPhepNghiHocCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ cho phép nghỉ học trong hệ thống.");

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
