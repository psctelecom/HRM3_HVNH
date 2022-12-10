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
    public class MailMerge_QuyetDinhThinhGiang : IMailMerge<IList<QuyetDinhThinhGiang>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThinhGiang> qdList)
        {
            var list = new List<Non_QuyetDinhThinhGiang>();
            Non_QuyetDinhThinhGiang qd;
            foreach (QuyetDinhThinhGiang quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThinhGiang();
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
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.GiangVienThinhGiang);
                qd.DanhXungVietHoa = quyetDinh.GiangVienThinhGiang.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.GiangVienThinhGiang.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.NhanVien = quyetDinh.GiangVienThinhGiang.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.NgaySinh = quyetDinh.GiangVienThinhGiang.NgaySinh.ToString("dd/MM/yyyy");
                qd.CMND = quyetDinh.GiangVienThinhGiang.CMND;
                qd.CapNgay = quyetDinh.GiangVienThinhGiang.NgayCap.ToString("dd/MM/yyyy");
                qd.NoiCap = quyetDinh.GiangVienThinhGiang.NoiCap != null ? quyetDinh.GiangVienThinhGiang.NoiCap.TenTinhThanh.ToString() : "";
                qd.MonDay = HamDungChung.TenMonHocTuPhanMenUIS(quyetDinh.MonDay);
                qd.QuyenCaoNhatDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.BoPhan);
                list.Add(qd);

            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhThinhGiang.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThinhGiang>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thỉnh giảng trong hệ thống.");
        }
    }
}
