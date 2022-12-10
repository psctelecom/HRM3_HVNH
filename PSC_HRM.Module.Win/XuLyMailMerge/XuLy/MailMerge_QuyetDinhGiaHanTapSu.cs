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
    public class MailMerge_QuyetDinhGiaHanTapSu : IMailMerge<IList<QuyetDinhGiaHanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhGiaHanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhGiaHanTapSu>();
            Non_QuyetDinhGiaHanTapSu qd;
            foreach (QuyetDinhGiaHanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhGiaHanTapSu();
                
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
                

                //Non_QuyetDinhNhanVien
                //HoSo
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? quyetDinh.ThongTinNhanVien.NgaySinh.ToString("d") : "";
                qd.NoiSinh = quyetDinh.ThongTinNhanVien.NoiSinh != null ? quyetDinh.ThongTinNhanVien.NoiSinh.FullDiaChi : "";
                qd.DiaChiThuongTru = quyetDinh.ThongTinNhanVien.DiaChiThuongTru != null ? quyetDinh.ThongTinNhanVien.DiaChiThuongTru.FullDiaChi : "";
                qd.DienThoaiDiDong = quyetDinh.ThongTinNhanVien.DienThoaiDiDong;
                qd.Email = quyetDinh.ThongTinNhanVien.Email;
                //NhanVienTrinhDo
                qd.TrinhDoChuyenMon = quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ?
                    quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                qd.ChuyenMonDaoTao = (quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null) ?
                    quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                qd.TruongDaoTao = quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao != null ?
                    quyetDinh.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao.TenTruongDaoTao:"";
                //NhanVien
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.NgayVaoCoQuan = quyetDinh.ThongTinNhanVien.NgayVaoCoQuan.ToString("dd/MM/yyyy");
                //NhanVienThongTinLuong
                qd.MaNgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                qd.NgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";

                //Non_QuyetDinhGiaHanTapSu
                qd.TuNgay = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.ThoiGianGiaHan = quyetDinh.ThoiGianGiaHan.ToString("N0");
                qd.TapSuDenNgay = quyetDinh.NgayHetHanTapSu != DateTime.MinValue ? quyetDinh.NgayHetHanTapSu.ToString("d") : "";
                qd.LyDo = quyetDinh.LyDo;

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhGiaHanTapSu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhGiaHanTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ gia hạn tập sự trong hệ thống.");
        }
    }
}
