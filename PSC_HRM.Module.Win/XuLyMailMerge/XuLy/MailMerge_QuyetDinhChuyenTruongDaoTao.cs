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
    public class MailMerge_QuyetDinhChuyenTruongDaoTao : IMailMerge<IList<QuyetDinhChuyenTruongDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChuyenTruongDaoTao> qdList)
        {
            var list = new List<Non_QuyetDinhChuyenTruongDaoTao>();
            Non_QuyetDinhChuyenTruongDaoTao qd;
            foreach (QuyetDinhChuyenTruongDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChuyenTruongDaoTao();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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

                qd.QuocGiaMoi = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.TruongDaoTaoMoi = quyetDinh.TruongDaoTaoMoi != null ? quyetDinh.TruongDaoTaoMoi.TenTruongDaoTao : "";
                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.HinhThucDaoTao = quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao != null ? quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao.TenHinhThucDaoTao : "";
                if (quyetDinh.QuyetDinhDaoTao.DaoTaoTapTrung)
                    qd.HinhThucDaoTao += " (Tập trung)";
                else
                    qd.HinhThucDaoTao += " (Không tập trung)";
                qd.TruongDaoTao = quyetDinh.QuyetDinhDaoTao.TruongDaoTao != null ? quyetDinh.QuyetDinhDaoTao.TruongDaoTao.TenTruongDaoTao : "";
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia : "";
                qd.TrinhDoChuyenMon = quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon != null ? quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                qd.KhoaDaoTao = quyetDinh.QuyetDinhDaoTao.KhoaDaoTao != null ? quyetDinh.QuyetDinhDaoTao.KhoaDaoTao.TenKhoaDaoTao : "";
                qd.NguonKinhPhi = quyetDinh.QuyetDinhDaoTao.NguonKinhPhi != null ? quyetDinh.QuyetDinhDaoTao.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhDaoTao.TruongHoTro) ? quyetDinh.QuyetDinhDaoTao.TruongHoTro : "";
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhChuyenTruongDaoTao.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChuyenTruongDaoTao>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chuyển trường đào tạo trong hệ thống.");
        }
    }
}
