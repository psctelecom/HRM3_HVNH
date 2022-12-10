using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhHuyDaoTao : IMailMerge<IList<QuyetDinhHuyDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhHuyDaoTao> qdList)
        {
            bool TrongNuoc = false;
            var list = new List<Non_QuyetDinhHuyDaoTao>();
            Non_QuyetDinhHuyDaoTao qd;
            foreach (QuyetDinhHuyDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuyDaoTao();
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
                qd.MaNgachLuong = quyetDinh.ThongTinNhanVien != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                qd.NgachLuong = quyetDinh.ThongTinNhanVien != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.BoPhan);
                qd.LyDo = quyetDinh.LyDo;
                qd.NguonKinhPhi = quyetDinh.QuyetDinhDaoTao != null ? quyetDinh.QuyetDinhDaoTao.NguonKinhPhi.TenNguonKinhPhi : "";
                //ChiTietDaoTao chiTietDaoTao = obs.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhBoiDuong.Oid, quyetDinh.ThongTinNhanVien.Oid));
                //if (chiTietDaoTao != null)
                //qd.ChuyenMonDaoTao = chiTietDaoTao.ChuyenMonDaoTao != null ? chiTietDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                //qd.ThoiGianGiaHan = quyetDinh.ThoiGianGiaHan.ToString(); 
                //qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                //qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                //qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                //qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TuNgayQuyetDinh = quyetDinh.QuyetDinhDaoTao.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayQuyetDinh = quyetDinh.QuyetDinhDaoTao.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TruongBoiDuong = quyetDinh.QuyetDinhDaoTao.TruongDaoTao != null ? quyetDinh.QuyetDinhDaoTao.TruongDaoTao.TenTruongDaoTao : "";
                //qd.CTBoiDuong = quyetDinh.QuyetDinhDaoTao.ChuongTrinhBoiDuong != null ? quyetDinh.QuyetDinhBoiDuong.ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong : "";
                //qd.ThoiGianGiaHan = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
                //qd.ChuyenNganhDaoTao = quyetDinh.QuyetDinhBoiDuong.ChuyenNganhDaoTao != null ? quyetDinh.QuyetDinhBoiDuong.ChuyenNganhDaoTao.TenTrinhDoChuyenMon : "";
                qd.SoQDBD = quyetDinh.QuyetDinhDaoTao.SoQuyetDinh;
                qd.NgayKyQDBDDate = quyetDinh.QuyetDinhDaoTao.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CoQuanKyQDBD = quyetDinh.QuyetDinhDaoTao.TenCoQuan;
                qd.QuocGia = quyetDinh.QuyetDinhDaoTao.QuocGia != null ? quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia : "";
                //qd.NoiBD = quyetDinh.QuyetDinhDaoTao.NoiBoiDuong;
                //qd.NoiDungBD = quyetDinh.QuyetDinhBoiDuong.NoiDungBoiDuong;
               // qd.DVTCBD = quyetDinh.QuyetDinhDaoTao.DonViToChuc;

                if (quyetDinh.QuyetDinhDaoTao.QuocGia != null &&
               HamDungChung.CauHinhChung.QuocGia != null &&
               quyetDinh.QuyetDinhDaoTao.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    TrongNuoc = false;
                }
                else
                {
                    TrongNuoc = true;
                }

                list.Add(qd);
            }

            if (TrongNuoc == true )
            {
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuyDaoTao.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuyDaoTao>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hủy đào tạo trong nước trong hệ thống.");
            }
            else
            {
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuyDaoTaoNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuyDaoTao>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hủy đào tạo ngoài nước trong hệ thống.");
            }
        }
    }
}
