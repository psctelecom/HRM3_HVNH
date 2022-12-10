using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhNghiHuu : IMailMerge<IList<QuyetDinhNghiHuu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNghiHuu> qdList)
        {
            var list = new List<Non_QuyetDinhNghiHuu>();
            Non_QuyetDinhNghiHuu qd;
            foreach (QuyetDinhNghiHuu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNghiHuu();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                }
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.HocHam = HamDungChung.GetHocHam(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(quyetDinh.ThongTinNhanVien);
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.NhanVienVietHoa = quyetDinh.ThongTinNhanVien.HoTen.ToUpper();
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);

                qd.NghiViecTuNgay = quyetDinh.NghiViecTuNgay != DateTime.MinValue ? quyetDinh.NghiViecTuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? quyetDinh.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NgaySinhDate = quyetDinh.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy") : "";
                qd.NoiSinh = quyetDinh.ThongTinNhanVien.NoiSinh != null ? quyetDinh.ThongTinNhanVien.NoiSinh.FullDiaChi : "";
                qd.NoiSinh_TinhTP = quyetDinh.ThongTinNhanVien.NoiSinh.TinhThanh != null ? quyetDinh.ThongTinNhanVien.NoiSinh.TinhThanh.TenTinhThanh : "";
           
                qd.SoCMND = quyetDinh.ThongTinNhanVien.CMND;
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                qd.NgachLuong = quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? quyetDinh.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                qd.NgayKy = DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.DienThoaiDiDong = quyetDinh.ThongTinNhanVien.DienThoaiDiDong;
                qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(quyetDinh.BoPhan);
                TaiKhoanNganHang TaiKhoan = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien = ? and TaiKhoanChinh", quyetDinh.ThongTinNhanVien.Oid));
                if (TaiKhoan != null)
                {
                    qd.TKNganHang = TaiKhoan.SoTaiKhoan;
                    qd.NganHang = TaiKhoan.NganHang.TenNganHang;
                }

                HoSoBaoHiem hoSoBaoHiem = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", quyetDinh.ThongTinNhanVien.Oid));
                if (hoSoBaoHiem != null)
                {
                    qd.SoSoBHXH = hoSoBaoHiem.SoSoBHXH;
                    qd.NoiKhamBHYT = hoSoBaoHiem.NoiDangKyKhamChuaBenh != null ? hoSoBaoHiem.NoiDangKyKhamChuaBenh.TenBenhVien : null;
                }
                qd.NoiCuTruSauKhiNghiViec = quyetDinh.NoiCuTruSauKhiThoiViec != null ? quyetDinh.NoiCuTruSauKhiThoiViec.FullDiaChi : "";
                qd.NoiCuTruSauKhiNghiViec_Quan = quyetDinh.NoiCuTruSauKhiThoiViec.QuanHuyen != null ? quyetDinh.NoiCuTruSauKhiThoiViec.QuanHuyen.TenQuanHuyen : "";
                qd.NoiCuTruSauKhiNghiViec_Tinh = quyetDinh.NoiCuTruSauKhiThoiViec.TinhThanh != null ? quyetDinh.NoiCuTruSauKhiThoiViec.TinhThanh.TenTinhThanh : "";
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNghiHuu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNghiHuu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nghỉ hưu trong hệ thống.");
        }
    }
}
