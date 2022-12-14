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
    public class MailMerge_QuyetDinhBoNhiemKiemNhiem : IMailMerge<IList<QuyetDinhBoNhiemKiemNhiem>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhBoNhiemKiemNhiem> qdList)
        {
            var list = new List<Non_QuyetDinhBoNhiemKiemNhiem>();
            Non_QuyetDinhBoNhiemKiemNhiem qd;
            foreach (QuyetDinhBoNhiemKiemNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoNhiemKiemNhiem();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
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
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.ChucVu = quyetDinh.ChucVuKiemNhiemMoi != null ? quyetDinh.ChucVuKiemNhiemMoi.TenChucVu : "";
                qd.TaiDonVi = quyetDinh.BoPhanMoi.TenBoPhan;
                qd.NgayHetNhiemKy = quyetDinh.NgayHetNhiemKy != DateTime.MinValue ? quyetDinh.NgayHetNhiemKy.ToString("d") : "";

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoNhiemKiemNhiem.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhBoNhiemKiemNhiem>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bổ nhiệm kiêm nhiệm trong hệ thống.");
        }
    }
}
