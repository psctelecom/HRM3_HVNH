using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.HopDong;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_HopDongLamViec : IMailMerge<IList<HopDong_LamViec>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_LamViec> hdList)
        {
            var listHopDongLanDau = new List<Non_HopDongLamViec>();
            var listCoThoiHan = new List<Non_HopDongLamViec>();
            var listKhongThoiHan = new List<Non_HopDongLamViec>();
            Non_HopDongLamViec hd;
            foreach (HopDong_LamViec obj in hdList)
            {
                hd = new Non_HopDongLamViec();
                hd.Oid = obj.Oid.ToString();
                hd.DonViChuQuan = obj.ThongTinTruong != null ? obj.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = obj.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = obj.ThongTinTruong.TenBoPhan;
                hd.DiaChi = obj.DiaChi;
                hd.SoDienThoai = obj.DienThoai;
                hd.LoaiHopDong = obj.HinhThucHopDong != null ? obj.HinhThucHopDong.TenHinhThucHopDong : "Không xác định thời hạn";
                hd.SoHopDong = obj.SoHopDong;
                hd.NgayKy = obj.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayKyDate = obj.NgayKy.ToString("dd/MM/yyyy");
                hd.ChucVuNguoiKy = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu : "";
                hd.ChucDanhNguoiKy = HamDungChung.GetChucDanh(obj.NguoiKy);
                hd.DanhXungNguoiKy = obj.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.NguoiKyVietHoa = obj.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = obj.NguoiKy.HoTen;
                hd.MaHocHamNguoiKy = obj.NguoiKy.NhanVienTrinhDo.HocHam != null ? obj.NguoiKy.NhanVienTrinhDo.HocHam.MaQuanLy : "";
                hd.MaTrinhDoNguoiKy = obj.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy : "";
                
                hd.ChucVu = obj.ChucVu != null ? obj.ChucVu.TenChucVu : "Không";
                hd.ChucDanhNguoiLaoDong = HamDungChung.GetChucDanh(obj.NhanVien);
                
                hd.DanhXungNLDVietHoa = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.DanhXungNLDVietThường = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                hd.NguoiLaoDongVietHoa = obj.NhanVien.HoTen.ToUpper();
                hd.NguoiLaoDongVietThuong = obj.NhanVien.HoTen;
                hd.QuocTich = obj.QuocTich.TenQuocGia;
                hd.NgaySinh = obj.NhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgaySinhDate = obj.NhanVien.NgaySinh.ToString("dd/MM/yyyy");
                hd.NoiSinh = obj.NhanVien.NoiSinh != null ? obj.NhanVien.NoiSinh.TinhThanh != null ? obj.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                hd.QueQuan = obj.NhanVien.QueQuan != null ? obj.NhanVien.QueQuan.FullDiaChi : "";
                hd.TrinhDo = obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                hd.ChuyenMon = obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                hd.NamTotNghiep = obj.NhanVien.NhanVienTrinhDo.NamTotNghiep > 0 ? obj.NhanVien.NhanVienTrinhDo.NamTotNghiep.ToString("d") : "";
                HoSoBaoHiem bhxh = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", obj.NhanVien.Oid));
                hd.SoSoBHXH = bhxh != null ? String.Format("Số sổ: {0}; ngày tham gia: {1:d}", bhxh.SoSoBHXH, bhxh.NgayThamGiaBHXH) : "Chưa tham gia BHXH";
                hd.DienThoaiDiDong = obj.NhanVien.DienThoaiDiDong;
                hd.DiaChiThuongTru = obj.NhanVien.DiaChiThuongTru != null ? obj.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                hd.NoiOHienNay = obj.NhanVien.NoiOHienNay != null ? obj.NhanVien.NoiOHienNay.FullDiaChi : "";
                hd.SoCMND = obj.NhanVien.CMND;
                hd.NgayCap = obj.NhanVien.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayCapDate = obj.NhanVien.NgayCap.ToString("dd/MM/yyyy");
                hd.NoiCap = obj.NhanVien.NoiCap != null ? "CA. " + obj.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(obj.ChucDanhChuyenMon) ? obj.ChucDanhChuyenMon : "";
                if (TruongConfig.MaTruong.Equals("QNU"))
                    hd.DiaDiemLamViec = obj.NoiLamViec;
                else
                    hd.DiaDiemLamViec = obj.BoPhan != null ? obj.BoPhan.TenBoPhan : "";
                hd.TuNgay = obj.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = obj.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.TuNgayDate = obj.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = obj.DenNgay.ToString("dd/MM/yyyy");
                hd.MucLuongDuocHuong = !obj.DieuKhoanHopDong.Huong85PhanTramMucLuong ? "100%" : "85%";
                hd.TapSuTuNgay = obj.TapSuTuNgay != DateTime.MinValue ? obj.TapSuTuNgay.ToString("d") : "";
                hd.TapSuDenNgay = obj.TapSuDenNgay != DateTime.MinValue ? obj.TapSuDenNgay.ToString("d") : "";
                hd.NgheNghiepTruocKhiDuocTuyen = obj.NgheNghiepTruocKhiDuocTuyen ?? "";

                hd.MaNgach = obj.DieuKhoanHopDong.NgachLuong != null ? obj.DieuKhoanHopDong.NgachLuong.MaQuanLy : "";
                hd.NgachLuong = obj.DieuKhoanHopDong.NgachLuong != null ? obj.DieuKhoanHopDong.NgachLuong.TenNgachLuong : "";
                hd.BacLuong = obj.DieuKhoanHopDong.BacLuong != null ? obj.DieuKhoanHopDong.BacLuong.TenBacLuong : "";
                hd.HeSoLuong = obj.DieuKhoanHopDong.HeSoLuong.ToString("N2");
                hd.VuotKhung = obj.DieuKhoanHopDong.VuotKhung > 0 ? obj.DieuKhoanHopDong.VuotKhung.ToString("N0") : ""; 

                hd.ThoiGianXetNangBacLuong = obj.MocNangLuong != DateTime.MinValue ? obj.MocNangLuong.ToString("d") : "";
                hd.CanCu = obj.CanCu;
                hd.NgayBatDauDongBaoHiem = obj.NgayBatDauDongBaoHiem.ToString("d");

                if (obj.PhanLoai == HopDongLamViecEnum.HopDongLanDau)
                {
                    QuyetDinhTuyenDung quyetDinh = obs.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("ListChiTietQuyetDinhTuyenDung[ThongTinNhanVien=?]", obj.NhanVien.Oid));
                    if (quyetDinh != null)
                    {
                        hd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                        hd.NgayKyQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("d");
                        hd.NamTuyenDung = quyetDinh.NgayQuyetDinh.Year.ToString("####");
                    }
                    listHopDongLanDau.Add(hd);
                }
                if (obj.PhanLoai == HopDongLamViecEnum.CoThoiHan)
                {
                    QuyetDinhTuyenDung quyetDinh = obs.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("ListChiTietQuyetDinhTuyenDung[ThongTinNhanVien=?]", obj.NhanVien.Oid));
                    if (quyetDinh != null)
                    {
                        hd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                        hd.NgayKyQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("d");
                        hd.NamTuyenDung = quyetDinh.NgayQuyetDinh.Year.ToString("####");
                    }
                    listCoThoiHan.Add(hd);
                }
                if (obj.PhanLoai == HopDongLamViecEnum.KhongThoiHan)
                {
                    QuyetDinhBoNhiemNgach boNhiemNgach = obs.FindObject<QuyetDinhBoNhiemNgach>(CriteriaOperator.Parse("ListChiTietQuyetDinhBoNhiemNgach[ThongTinNhanVien=?]", obj.NhanVien.Oid));
                    if (boNhiemNgach != null)
                    {
                        hd.SoQuyetDinh = boNhiemNgach.SoQuyetDinh;
                        hd.NgayKyQuyetDinh = boNhiemNgach.NgayQuyetDinh.ToString("d");
                    }
                    listKhongThoiHan.Add(hd);
                }                
            }

            MailMergeTemplate merge;
            if (listHopDongLanDau.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLamViecLanDau.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLamViec>(listHopDongLanDau, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng làm việc trong hệ thống.");
            }
            if (listCoThoiHan.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLamViecCoThoiHan.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLamViec>(listCoThoiHan, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng làm việc trong hệ thống.");
            }
            if (listKhongThoiHan.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLamViecKhongThoiHan.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLamViec>(listKhongThoiHan, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng làm việc trong hệ thống.");
            }
        }
    }
}
