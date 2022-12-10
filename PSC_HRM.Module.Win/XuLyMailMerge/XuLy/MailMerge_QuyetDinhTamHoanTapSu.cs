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
    public class MailMerge_QuyetDinhTamHoanTapSu : IMailMerge<IList<QuyetDinhTamHoanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTamHoanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhTamHoanTapSu>();
            Non_QuyetDinhTamHoanTapSu qd;
            foreach (QuyetDinhTamHoanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTamHoanTapSu();
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
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.LyDo = quyetDinh.LyDo ?? "";
                qd.QuyetDinhHuongDanTapSu = quyetDinh.QuyetDinhHuongDanTapSu != null ? quyetDinh.QuyetDinhHuongDanTapSu.SoQuyetDinh : "";
                qd.MaNgach = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.MaQuanLy : "";
                qd.NgachLuong = quyetDinh.NgachLuong != null ? quyetDinh.NgachLuong.TenNgachLuong : "";
                qd.HoanTuNgay = quyetDinh.HoanTuNgay != DateTime.MinValue ? quyetDinh.HoanTuNgay.ToString("d") : "";
                qd.HoanDenNgay = quyetDinh.HoanDenNgay != DateTime.MinValue ? quyetDinh.HoanDenNgay.ToString("d") : "";
                qd.TapSuTuNgay = quyetDinh.TapSuTuNgay != DateTime.MinValue ? quyetDinh.TapSuTuNgay.ToString("d") : "";
                qd.TapSuDenNgay = quyetDinh.TapSuDenNgay != DateTime.MinValue ? quyetDinh.TapSuDenNgay.ToString("d") : "";
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTamHoanTapSu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTamHoanTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tạm hoãn tập sự trong hệ thống.");
        }
    }
}
