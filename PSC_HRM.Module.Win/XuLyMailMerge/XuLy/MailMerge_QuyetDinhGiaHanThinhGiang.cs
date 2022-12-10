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
    public class MailMerge_QuyetDinhGiaHanThinhGiang : IMailMerge<IList<QuyetDinhGiaHanThinhGiang>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhGiaHanThinhGiang> qdList)
        {
            var list = new List<Non_QuyetDinhGiaHanThinhGiang>();
            Non_QuyetDinhGiaHanThinhGiang qd;
            foreach (QuyetDinhGiaHanThinhGiang quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhGiaHanThinhGiang();
                
                //Non_QuyetDinh
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
                qd.NhanVien = quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.HoTen;
                qd.NgaySinh = quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.NgaySinh.ToString("dd/MM/yyyy");
                qd.SoCMND = quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.CMND;
                qd.CapNgay = quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.NgayCap.ToString("dd/MM/yyyy");
                qd.DonVi = quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.DonViCongTac;
                qd.NoiCap = quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.NoiCap != null ? quyetDinh.QuyetDinhThinhGiang.GiangVienThinhGiang.NoiCap.TenTinhThanh.ToString() : "";
                qd.ChucVuDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.QuyetDinhThinhGiang.BoPhan);
              
         
                //Non_QuyetDinhGiaHanThinhGiang
                qd.TuNgay = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.ThoiGianGiaHan = quyetDinh.ThoiGianGiaHan.ToString("N0");
         

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhGiaHanThinhGiang.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhGiaHanThinhGiang>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ gia hạn tập sự trong hệ thống.");
        }
    }
}
