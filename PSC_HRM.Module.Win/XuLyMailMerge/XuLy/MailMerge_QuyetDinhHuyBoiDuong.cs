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
    public class MailMerge_QuyetDinhHuyBoiDuong : IMailMerge<IList<QuyetDinhHuyBoiDuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhHuyBoiDuong> qdList)
        {
            bool TrongNuoc = false;
            var list = new List<Non_QuyetDinhHuyBoiDuong>();
            Non_QuyetDinhHuyBoiDuong qd;
            foreach (QuyetDinhHuyBoiDuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuyBoiDuong();
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
                qd.NguonKinhPhi = quyetDinh.QuyetDinhBoiDuong != null ? quyetDinh.QuyetDinhBoiDuong.NguonKinhPhi.TenNguonKinhPhi : "";
                //ChiTietDaoTao chiTietDaoTao = obs.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhBoiDuong.Oid, quyetDinh.ThongTinNhanVien.Oid));
                //if (chiTietDaoTao != null)
                //qd.ChuyenMonDaoTao = chiTietDaoTao.ChuyenMonDaoTao != null ? chiTietDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                //qd.ThoiGianGiaHan = quyetDinh.ThoiGianGiaHan.ToString(); 
                //qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                //qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                //qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                //qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TuNgayQuyetDinh = quyetDinh.QuyetDinhBoiDuong.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhBoiDuong.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayQuyetDinh = quyetDinh.QuyetDinhBoiDuong.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhBoiDuong.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TruongBoiDuong = quyetDinh.QuyetDinhBoiDuong.TruongDaoTao != null ? quyetDinh.QuyetDinhBoiDuong.TruongDaoTao : "";
                qd.CTBoiDuong = quyetDinh.QuyetDinhBoiDuong.ChuongTrinhBoiDuong != null ? quyetDinh.QuyetDinhBoiDuong.ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong : "";
                //qd.ThoiGianGiaHan = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
                //qd.ChuyenNganhDaoTao = quyetDinh.QuyetDinhBoiDuong.ChuyenNganhDaoTao != null ? quyetDinh.QuyetDinhBoiDuong.ChuyenNganhDaoTao.TenTrinhDoChuyenMon : "";
                qd.SoQDBD = quyetDinh.QuyetDinhBoiDuong.SoQuyetDinh;
                qd.NgayKyQDBDDate = quyetDinh.QuyetDinhBoiDuong.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CoQuanKyQDBD = quyetDinh.QuyetDinhBoiDuong.TenCoQuan;
                qd.QuocGia = quyetDinh.QuyetDinhBoiDuong.QuocGia != null ? quyetDinh.QuyetDinhBoiDuong.QuocGia.TenQuocGia : "";
                qd.NoiBD = quyetDinh.QuyetDinhBoiDuong.NoiBoiDuong;
                qd.NoiDungBD = quyetDinh.QuyetDinhBoiDuong.NoiDungBoiDuong;
                qd.DVTCBD = quyetDinh.QuyetDinhBoiDuong.DonViToChuc;

                if (quyetDinh.QuyetDinhBoiDuong.QuocGia != null &&
               HamDungChung.CauHinhChung.QuocGia != null &&
               quyetDinh.QuyetDinhBoiDuong.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
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
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuyBoiDuong.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuyBoiDuong>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hủy bồi dưỡng trong nước trong hệ thống.");
            }
            else
            {
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuyBoiDuongNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuyBoiDuong>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hủy bồi dưỡng ngoài nước trong hệ thống.");
            }
        }
    }
}
