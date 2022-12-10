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
    public class MailMerge_QuyetDinhMienNhiemKiemNhiem : IMailMerge<IList<QuyetDinhMienNhiemKiemNhiem>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhMienNhiemKiemNhiem> qdList)
        {
            var list = new List<Non_QuyetDinhMienNhiemKiemNhiem>();
            Non_QuyetDinhMienNhiemKiemNhiem qd;
            foreach (QuyetDinhMienNhiemKiemNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhMienNhiemKiemNhiem();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
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
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                //qd.ChucVuKiemNhiem = quyetDinh.QuyetDinhBoNhiemKiemNhiem.ChucVuKiemNhiemMoi != null ? quyetDinh.QuyetDinhBoNhiemKiemNhiem.ChucVuKiemNhiemMoi.TenChucVu : "";
                //qd.TaiDonVi = quyetDinh.QuyetDinhBoNhiemKiemNhiem.BoPhanMoi.TenBoPhan;
                qd.ChucVuKiemNhiemMoi = quyetDinh.ChucVuKiemNhiemMoi != null ? quyetDinh.ChucVuKiemNhiemMoi.TenChucVu : "";
                qd.HSPCKiemNhiemMoi = quyetDinh.HSPCKiemNhiemMoi.ToString("N2");

                qd.LyDo = quyetDinh.LyDo;
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhMienNhiemKiemNhiem.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhMienNhiemKiemNhiem>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ miễn nhiệm kiêm nhiệm trong hệ thống.");
        }
    }
}
