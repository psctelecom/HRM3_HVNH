using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhMienNhiem : IMailMerge<IList<QuyetDinhMienNhiem>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhMienNhiem> qdList)
        {
            var list = new List<Non_QuyetDinhMienNhiem>();
            Non_QuyetDinhMienNhiem qd;
            foreach (QuyetDinhMienNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhMienNhiem();
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

                //qd.ChucVu = quyetDinh.QuyetDinhBoNhiem.ChucVuMoi != null ? quyetDinh.QuyetDinhBoNhiem.ChucVuMoi.TenChucVu : "";
                //qd.HSPCChucVu = quyetDinh.QuyetDinhBoNhiem.HSPCChucVuMoi.ToString("N2");
                qd.ChucVu = quyetDinh.ChucVuMoi != null ? quyetDinh.ChucVuMoi.TenChucVu : "";
                qd.HSPCChucVu = quyetDinh.HSPCChucVuMoi.ToString("N2");
                //qd.NgayThoiHuongHSPCChucVu = quyetDinh.NgayThoiHuongHSPCChucVu != DateTime.MinValue ? quyetDinh.NgayThoiHuongHSPCChucVu.ToString("d") : "";
                qd.LyDo = quyetDinh.LyDo;

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhMienNhiem.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhMienNhiem>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ miễn nhiệm trong hệ thống.");
        }
    }
}
