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
    public class MailMerge_QuyetDinhBoNhiemChucVuDoan : IMailMerge<IList<QuyetDinhBoNhiemChucVuDoan>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhBoNhiemChucVuDoan> qdList)
        {
            var list = new List<Non_QuyetDinhBoNhiemChucVuDoan>();
            Non_QuyetDinhBoNhiemChucVuDoan qd;
            foreach (QuyetDinhBoNhiemChucVuDoan quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoNhiemChucVuDoan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
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
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.HocHam = HamDungChung.GetHocVi(quyetDinh.ThongTinNhanVien);
                
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.TaiBoMon = quyetDinh.ThongTinNhanVien.TaiBoMon!=null ? quyetDinh.ThongTinNhanVien.TaiBoMon.TenBoPhan: "" ;
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.ChucVuDoanCu = quyetDinh.ChucVuDoanCu != null ? quyetDinh.ChucVuDoanCu.TenChucVuDoan : "";
                qd.ChucVuDoanMoi = quyetDinh.ChucVuDoan != null ? quyetDinh.ChucVuDoan.TenChucVuDoan : "";
                qd.HSPCChucVuDoanMoi = quyetDinh.HSPCChucVuDoan.ToString("N2");
                qd.NgayHuongHSPCChucVuDoanMoi = quyetDinh.NgayBoNhiemDoan != DateTime.MinValue ? quyetDinh.NgayBoNhiemDoan.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NamNhiemKy = quyetDinh.NhiemKy;
                
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoNhiemChucVuDoan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhBoNhiemChucVuDoan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bổ nhiệm trong hệ thống.");
        }
    }
}
