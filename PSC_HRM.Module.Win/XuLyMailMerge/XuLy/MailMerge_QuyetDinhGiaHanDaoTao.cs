using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhGiaHanDaoTao : IMailMerge<IList<QuyetDinhGiaHanDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhGiaHanDaoTao> qdList)
        {
            bool TrongNuoc = false;
            var list = new List<Non_QuyetDinhGiaHanDaoTao>();
            Non_QuyetDinhGiaHanDaoTao qd;
            foreach (QuyetDinhGiaHanDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhGiaHanDaoTao();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoCongVan = quyetDinh.SoCongVan;
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
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.BoPhan);


                qd.TrinhDoChuyenMon = quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon != null ? quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                ChiTietDaoTao chiTietDaoTao = obs.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhDaoTao.Oid, quyetDinh.ThongTinNhanVien.Oid));
                if (chiTietDaoTao != null)
                    qd.ChuyenMonDaoTao = chiTietDaoTao.ChuyenMonDaoTao != null ? chiTietDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                qd.TruongDaoTao = quyetDinh.QuyetDinhDaoTao.TruongDaoTao != null ? quyetDinh.QuyetDinhDaoTao.TruongDaoTao.TenTruongDaoTao : "";
                qd.KhoaDaoTao = quyetDinh.QuyetDinhDaoTao.KhoaDaoTao != null ? quyetDinh.QuyetDinhDaoTao.KhoaDaoTao.TenKhoaDaoTao : "";
                qd.ThoiGianGiaHan = quyetDinh.ThoiGianGiaHan.ToString(); 
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.ThoiGianDaoTao = quyetDinh.TuNgay.TinhSoNam(quyetDinh.DenNgay).ToString();
                qd.TuNgayQuyetDinh = quyetDinh.QuyetDinhDaoTao.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayQuyetDinh = quyetDinh.QuyetDinhDaoTao.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TuThang = quyetDinh.DenNgay.ToString("MM/yyyy");
                qd.SoQDDT = quyetDinh.QuyetDinhDaoTao.SoQuyetDinh;
                qd.NamDaoTao = quyetDinh.QuyetDinhDaoTao.NgayHieuLuc.ToString("yyyy");
                qd.NgayKyQDDTDate = quyetDinh.QuyetDinhDaoTao.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.CoQuanKyQDDT = quyetDinh.QuyetDinhDaoTao.TenCoQuan;
                qd.QuocGia = quyetDinh.QuyetDinhDaoTao.QuocGia != null ? quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia : "";
                qd.LyDo = quyetDinh.LyDo;
                qd.NgayXinGiaHan = quyetDinh.NgayXinGiaHan.ToString("dd/MM/yyyy");
                if (quyetDinh.QuyetDinhDaoTao.QuocGia != null &&
               HamDungChung.CauHinhChung.QuocGia != null &&
               quyetDinh.QuyetDinhDaoTao.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    TrongNuoc = false;
                }
                else
                {
                    TrongNuoc = true;
                }

                list.Add(qd);
            }

            if (TrongNuoc == true )
            {
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhGiaHanDaoTao.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhGiaHanDaoTao>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ gia hạn đào tạo trong nước trong hệ thống.");
            }
            else
            {
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhGiaHanDaoTaoNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhGiaHanDaoTao>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ gia hạn đào tạo ngoài nước trong hệ thống.");
            }
        }
    }
}
