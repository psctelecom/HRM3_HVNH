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
    public class MailMerge_QuyetDinhHuyDiNuocNgoai : IMailMerge<IList<QuyetDinhHuyDiNuocNgoai>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhHuyDiNuocNgoai> qdList)
        {
            var list = new List<Non_QuyetDinhHuyDiNuocNgoai>();
            Non_QuyetDinhHuyDiNuocNgoai qd;
            foreach (QuyetDinhHuyDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuyDiNuocNgoai();
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
                qd.NguonKinhPhi = quyetDinh.QuyetDinhDiNuocNgoai != null ? quyetDinh.QuyetDinhDiNuocNgoai.NguonKinhPhi.TenNguonKinhPhi : "";

                qd.TuNgayQuyetDinh = quyetDinh.QuyetDinhDiNuocNgoai.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDiNuocNgoai.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayQuyetDinh = quyetDinh.QuyetDinhDiNuocNgoai.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDiNuocNgoai.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TruongHoTro = quyetDinh.QuyetDinhDiNuocNgoai.TruongHoTro != null ? quyetDinh.QuyetDinhDiNuocNgoai.TruongHoTro : "";

                qd.SoQDBD = quyetDinh.QuyetDinhDiNuocNgoai.SoQuyetDinh;
                qd.NgayKyQDBDDate = quyetDinh.QuyetDinhDiNuocNgoai.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CoQuanKyQDBD = quyetDinh.QuyetDinhDiNuocNgoai.TenCoQuan;
                qd.QuocGia = quyetDinh.QuyetDinhDiNuocNgoai.QuocGia != null ? quyetDinh.QuyetDinhDiNuocNgoai.QuocGia.TenQuocGia : "";
                qd.DiaDiem = quyetDinh.QuyetDinhDiNuocNgoai.DiaDiem;
                qd.DVTCBD = quyetDinh.QuyetDinhDiNuocNgoai.DonViToChuc;

                list.Add(qd);
            }
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuyDiNuocNgoai.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuyDiNuocNgoai>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hủy đi nước ngoài trong hệ thống.");
            }
    }
}
