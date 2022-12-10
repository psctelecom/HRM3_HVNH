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
    public class MailMerge_QuyetDinhThoiChucVuDoan : IMailMerge<IList<QuyetDinhThoiChucVuDoan>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThoiChucVuDoan> qdList)
        {
            var list = new List<Non_QuyetDinhThoiChucVuDoan>();
            Non_QuyetDinhThoiChucVuDoan qd;
            foreach (QuyetDinhThoiChucVuDoan quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThoiChucVuDoan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("d");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("d");
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
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";

                qd.ChucVuDoan = quyetDinh.ChucVuDoanCu != null ? quyetDinh.ChucVuDoanCu.TenChucVuDoan : "";
                qd.NgayThoiHuongHSPCChucVuDoan = quyetDinh.NgayThoiHuongHSPCChucVuDoan != DateTime.MinValue ? quyetDinh.NgayThoiHuongHSPCChucVuDoan.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.LyDo = quyetDinh.LyDo;
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhThoiChucVuDoan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThoiChucVuDoan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thôi chức trong hệ thống.");
        }
    }
}
