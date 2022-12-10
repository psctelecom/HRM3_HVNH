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
    public class MailMerge_QuyetDinhThayCanBoHuongDanTapSu : IMailMerge<IList<QuyetDinhThayCanBoHuongDanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThayCanBoHuongDanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhThayCanBoHuongDanTapSu>();
            Non_QuyetDinhThayCanBoHuongDanTapSu qd;
            foreach (QuyetDinhThayCanBoHuongDanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThayCanBoHuongDanTapSu();
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
                qd.LoaiNhanVien = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);

                ChiTietQuyetDinhHuongDanTapSu chiTiet = obs.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhHuongDanTapSu.Oid, quyetDinh.ThongTinNhanVien.Oid));
                if (chiTiet != null)
                {
                    qd.ChucDanhCanBoHuongDanCu = HamDungChung.GetChucDanh(chiTiet.CanBoHuongDan);
                    qd.CanBoHuongDanCu = chiTiet.CanBoHuongDan.HoTen;
                }
                qd.ChucDanhCanBoHuongDanMoi = HamDungChung.GetChucDanh(quyetDinh.CanBoHuongDanMoi);
                qd.CanBoHuongDanMoi = quyetDinh.CanBoHuongDanMoi.HoTen;
                qd.HSPCTrachNhiem = quyetDinh.QuyetDinhHuongDanTapSu.HSPCTrachNhiem.ToString("N2");
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhThayCanBoHuongDanTapSu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThayCanBoHuongDanTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thay cán bộ hướng dẫn tập sự trong hệ thống.");
        }
    }
}
