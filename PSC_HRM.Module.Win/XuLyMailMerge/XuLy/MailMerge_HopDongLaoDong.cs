using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.HopDong;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_HopDongLaoDong : IMailMerge<IList<HopDong_LaoDong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_LaoDong> hdList)
        {
            var listThuViec = new List<Non_HopDongLaoDong>();
            var listCoThoiHan = new List<Non_HopDongLaoDong>();
            var listCoThoiHan_NhanVien = new List<Non_HopDongLaoDong>();
            var listCoThoiHan_Duoi12T = new List<Non_HopDongLaoDong>();
            var listKhongThoiHan = new List<Non_HopDongLaoDong>();
            Non_HopDongLaoDong hd;

            foreach (HopDong_LaoDong obj in hdList)
            {
                hd = new Non_HopDongLaoDong();
                hd.Oid = obj.Oid.ToString();
                hd.DonViChuQuan = obj.ThongTinTruong != null ? obj.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = obj.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = obj.ThongTinTruong.TenBoPhan;
                hd.DiaChi = obj.DiaChi;
                hd.SoDienThoai = obj.DienThoai;
                if(!TruongConfig.MaTruong.Contains("UEL"))
                    hd.LoaiHopDong = obj.HinhThucHopDong != null ? obj.HinhThucHopDong.TenHinhThucHopDong : "Không xác định thời hạn";
                else
                    hd.LoaiHopDong = obj.HinhThucHopDong != null ? obj.HinhThucHopDong.SoThang + " tháng" : "Không xác định thời hạn";
                hd.SoHopDong = obj.SoHopDong;
                hd.NgayKy = obj.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.ChucVuNguoiKy = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu : "";
                hd.ChucVuNguoiKyVietHoa = obj.ChucVuNguoiKy !=null ? obj.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                hd.ChucDanhNguoiKy = HamDungChung.GetChucDanh(obj.NguoiKy);
                hd.DanhXungNguoiKy = obj.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.NguoiKyVietHoa = obj.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = obj.NguoiKy.HoTen;
                hd.DonViNguoiKy = obj.NguoiKy.BoPhan != null ? obj.NguoiKy.BoPhan.TenBoPhan : "";
                hd.MaHocHamNguoiKy = obj.NguoiKy.NhanVienTrinhDo.HocHam != null ? obj.NguoiKy.NhanVienTrinhDo.HocHam.MaQuanLy : "";
                hd.MaTrinhDoNguoiKy = obj.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NguoiKy.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy : "";
                

                hd.ChucVu = obj.ChucVu != null ? obj.ChucVu.TenChucVu : "Không";
                hd.ChucDanhNguoiLaoDong = HamDungChung.GetChucDanh(obj.NhanVien);
                hd.DanhXungNLDVietHoa = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.DanhXungNLDVietThường = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                hd.NguoiLaoDongVietHoa = obj.NhanVien.HoTen.ToUpper();
                hd.NguoiLaoDongVietThuong = obj.NhanVien.HoTen;
                hd.GioiTinh = obj.NhanVien.GioiTinh == 0 ? "Nam" : "Nữ";
                hd.QuocTich = obj.NhanVien.QuocTich.TenQuocGia;
                hd.NgaySinh = obj.NhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgaySinhDate = obj.NhanVien.NgaySinh.ToString("d");
                hd.NoiSinh = obj.NhanVien.NoiSinh != null ? obj.NhanVien.NoiSinh.TinhThanh != null ? obj.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : obj.NhanVien.NoiSinh.FullDiaChi : "";
                hd.QueQuan = obj.NhanVien.QueQuan != null ? obj.NhanVien.QueQuan.TinhThanh != null ? obj.NhanVien.QueQuan.TinhThanh.TenTinhThanh : obj.NhanVien.QueQuan.FullDiaChi : "";
                hd.TrinhDo = obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                hd.ChuyenMon = obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                hd.NamTotNghiep = obj.NhanVien.NhanVienTrinhDo.NamTotNghiep > 0 ? obj.NhanVien.NhanVienTrinhDo.NamTotNghiep.ToString("d") : "";
                HoSoBaoHiem bhxh = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", obj.NhanVien.Oid));
                hd.SoSoBHXH = bhxh != null ? String.Format("Số sổ: {0}; ngày tham gia: {1:d}", bhxh.SoSoBHXH, bhxh.NgayThamGiaBHXH) : "Chưa tham gia BHXH";
                hd.DienThoaiDiDong = obj.NhanVien.DienThoaiDiDong ;
                hd.DiaChiThuongTru = obj.NhanVien.DiaChiThuongTru != null ? obj.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                hd.NoiOHienNay = obj.NhanVien.NoiOHienNay != null ? obj.NhanVien.NoiOHienNay.FullDiaChi : "";
                hd.SoCMND = obj.NhanVien.CMND;
                hd.NgayCap = obj.NhanVien.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayCapDate = obj.NhanVien.NgayCap.ToString("d");
                hd.NoiCap = obj.NhanVien.NoiCap != null ? "CA. " + obj.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(obj.ChucDanhChuyenMon) ? obj.ChucDanhChuyenMon : "";
                if (TruongConfig.MaTruong.Equals("QNU"))
                    hd.DiaDiemLamViec = obj.NoiLamViec;
                else
                    hd.DiaDiemLamViec = obj.BoPhan != null ? obj.BoPhan.TenBoPhan : "";
                hd.TuNgay = obj.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = obj.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.BoPhan = obj.NhanVien.BoPhan != null ? obj.NhanVien.BoPhan.TenBoPhan : "";
                hd.NgheNghiepTruocKhiDuocTuyen = obj.NgheNghiepTruocKhiDuocTuyen;
                hd.CongViecTuyenDung = obj.CongViecTuyenDung;
                hd.MaSoThue = obj.NhanVien.NhanVienThongTinLuong.MaSoThue;
                TaiKhoanNganHang taiKhoan = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien=? and TaiKhoanChinh=1", obj.NhanVien.Oid));
                hd.SoTaiKhoan = taiKhoan != null ? taiKhoan.SoTaiKhoan : "";
                hd.NganHang = taiKhoan != null ? taiKhoan.NganHang != null ? taiKhoan.NganHang.TenNganHang : "" : "";

                if (TruongConfig.MaTruong.Equals("GTVT"))
                {
                    if (obj.DenNgay != DateTime.MinValue)
                        hd.DenNgay = obj.DenNgay.ToString(" đến 'ngày' dd 'tháng' MM 'năm' yyyy");
                    else
                        hd.DenNgay = "";
                }

                hd.TuNgayDate = obj.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = obj.DenNgay.ToString("dd/MM/yyyy");
                
                hd.MucLuongDuocHuong = !obj.DieuKhoanHopDong.Huong85PhanTramMucLuong ? "100%" : "85%";
                hd.HinhThucThanhToan = obj.HinhThucThanhToan == HinhThucThanhToanEnum.ThanhToanBangTienMat ? "Tiền mặt" : "Qua thẻ ATM";
                hd.ThoiGianXetNangBacLuong = obj.MocNangLuong.ToString("d");
                hd.NgheNghiepTruocKhiDuocTuyen = obj.NgheNghiepTruocKhiDuocTuyen;
                hd.MaNgach = obj.DieuKhoanHopDong.NgachLuong != null ? obj.DieuKhoanHopDong.NgachLuong.MaQuanLy : "";
                hd.NgachLuong = obj.DieuKhoanHopDong.NgachLuong != null ? obj.DieuKhoanHopDong.NgachLuong.TenNgachLuong : "";
                hd.BacLuong = obj.DieuKhoanHopDong.BacLuong != null ? obj.DieuKhoanHopDong.BacLuong.TenBacLuong : "";
                hd.HeSoLuong = obj.DieuKhoanHopDong.HeSoLuong.ToString("N2");
                hd.VuotKhung = obj.DieuKhoanHopDong.VuotKhung > 0 ? obj.DieuKhoanHopDong.VuotKhung.ToString("N0") : "";
                hd.TienLuong = obj.MucLuong.ToString("N0");
                hd.PhuCapTienAn = obj.PhuCapTienAn.ToString("N0");
                hd.PhuCapDocHai = obj.PhuCapDocHai.ToString("N0");

                decimal tienLuong = obj.MucLuong * 0.85m;
                hd.TienLuong = obj.DieuKhoanHopDong.Huong85PhanTramMucLuong ? String.Format("{0:N0} x 85% = {1:N0}", obj.MucLuong, tienLuong) : obj.MucLuong.ToString("N0");
                hd.TienLuongBangChu = obj.DieuKhoanHopDong.Huong85PhanTramMucLuong ? HamDungChung.DocTien(tienLuong).Trim() : HamDungChung.DocTien(obj.MucLuong).Trim();
                
                hd.CanCu = obj.CanCu;

                if (obj.PhanLoai == HopDongLaoDongEnum.TapSuThuViec)
                    listThuViec.Add(hd);
                else if (obj.PhanLoai == HopDongLaoDongEnum.CoThoiHan)
                {
                    ThongTinNhanVien nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", obj.NhanVien.Oid));
                    if (TruongConfig.MaTruong.Equals("UFM"))
                    {
                        if (nhanVien.LoaiNhanSu != null && !nhanVien.LoaiNhanSu.TenLoaiNhanSu.Equals("Giảng viên"))
                            listCoThoiHan_NhanVien.Add(hd);
                        else
                            listCoThoiHan.Add(hd);
                    }
                    else if (TruongConfig.MaTruong.Equals("QNU"))
                    {
                        if (obj.HinhThucHopDong.SoThang < 12)
                            listCoThoiHan_Duoi12T.Add(hd);
                        else
                            listCoThoiHan.Add(hd);
                    }
                    else
                        listCoThoiHan.Add(hd);
                }
                else
                    listKhongThoiHan.Add(hd);
            }

            MailMergeTemplate merge;
            if (listThuViec.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLaoDongTapSu.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLaoDong>(listThuViec, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng lao động trong hệ thống.");
            }
            if (listCoThoiHan.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLaoDongCoThoiHan.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLaoDong>(listCoThoiHan, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng lao động trong hệ thống.");
            }

            if (listCoThoiHan_Duoi12T.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLaoDongCoThoiHanDuoi12Thang.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLaoDong>(listCoThoiHan_Duoi12T, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng lao động trong hệ thống.");
            }

            if (listCoThoiHan_NhanVien.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLaoDongCoThoiHan_NhanVien.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLaoDong>(listCoThoiHan_NhanVien, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng lao động trong hệ thống.");
            }

            if (listKhongThoiHan.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongLaoDongKhongThoiHan.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongLaoDong>(listKhongThoiHan, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng lao động trong hệ thống.");
            }
        }
    }
}
