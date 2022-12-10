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
    public class MailMerge_QuyetDinhCongNhanHocHam : IMailMerge<IList<QuyetDinhCongNhanHocHam>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhCongNhanHocHam> qdList)
        {
            var list = new List<Non_QuyetDinhCongNhanHocHam>();
            Non_QuyetDinhCongNhanHocHam qd;
            foreach (QuyetDinhCongNhanHocHam quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhCongNhanHocHam();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;

                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayQuyetDinh == DateTime.MinValue)
                {
                    qd.NgayQuyetDinh = HamDungChung.GetServerTime().ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayQuyetDinhDate = HamDungChung.GetServerTime().ToString("d");
                }
                else
                {
                    qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("d");
                }

                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = HamDungChung.GetServerTime().ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = HamDungChung.GetServerTime().ToString("d");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("d");
                }               
               
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NamQuyetDinh = quyetDinh.NgayHieuLuc.Year.ToString("####");

                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.BoPhan);
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.TrinhDoChuyenMon = quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy : "";
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);

                qd.HocHamMoi = quyetDinh.HocHam != null ? quyetDinh.HocHam.TenHocHam : "";
                qd.TuNgayDate = quyetDinh.TuNgay.ToString("dd/MM/yyyy");

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhCongNhanHocHam.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhCongNhanHocHam>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công nhận học hàm trong hệ thống.");
        }
    }
}
