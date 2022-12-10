using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.HopDong;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_ThanhLyHopDongThinhGiang : IMailMerge<IList<ChiTietThanhLyHopDongThinhGiang>>
    {
        public void Merge(IObjectSpace obs, IList<ChiTietThanhLyHopDongThinhGiang> hdList)
        {
            ThongTinTruong truong = HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session);
            var list = new List<Non_ThanhLyHopDongThinhGiang>();
            Non_ThanhLyHopDongThinhGiang hd;
          
            foreach (ChiTietThanhLyHopDongThinhGiang thanhLyHopDong in hdList)
            {
                hd = new Non_ThanhLyHopDongThinhGiang();
                hd.Oid = thanhLyHopDong.Oid.ToString();
                hd.DonViChuQuan = thanhLyHopDong.HopDongThinhGiang.ThongTinTruong != null ? thanhLyHopDong.HopDongThinhGiang.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = thanhLyHopDong.HopDongThinhGiang.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = thanhLyHopDong.HopDongThinhGiang.ThongTinTruong.TenBoPhan;
                hd.DiaChi = thanhLyHopDong.HopDongThinhGiang.DiaChi;
                hd.SoDienThoai = thanhLyHopDong.HopDongThinhGiang.DienThoai;
                hd.SoHopDong = thanhLyHopDong.So;
                hd.NgayKy = thanhLyHopDong.NgayLap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.ChucVuNguoiKy = thanhLyHopDong.ChucVuNguoiKy != null ? thanhLyHopDong.ChucVuNguoiKy.TenChucVu : "";
                hd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(thanhLyHopDong.NguoiKy);
                hd.DanhXungNguoiKy = thanhLyHopDong.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.NguoiKyVietHoa = thanhLyHopDong.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = thanhLyHopDong.NguoiKy.HoTen;

                hd.DanhXungNLDVietHoa = thanhLyHopDong.HopDongThinhGiang.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.DanhXungNLDVietThường = thanhLyHopDong.HopDongThinhGiang.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                hd.ChucDanhNguoiLaoDong = HamDungChung.GetChucDanh(thanhLyHopDong.HopDongThinhGiang.NhanVien);
                hd.NguoiLaoDongVietHoa = thanhLyHopDong.HopDongThinhGiang.NhanVien.HoTen.ToUpper();
                hd.NguoiLaoDongVietThuong = thanhLyHopDong.HopDongThinhGiang.NhanVien.HoTen;
                hd.QuocTich = thanhLyHopDong.HopDongThinhGiang.QuocTich.TenQuocGia;
                hd.NgaySinh = thanhLyHopDong.HopDongThinhGiang.NhanVien.NgaySinh.ToString("d");
                hd.NoiSinh = thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiSinh != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiSinh.TinhThanh != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                hd.QueQuan = thanhLyHopDong.HopDongThinhGiang.NhanVien.QueQuan != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.QueQuan.FullDiaChi : "";
                hd.TrinhDo = thanhLyHopDong.HopDongThinhGiang.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                hd.HocHam = thanhLyHopDong.HopDongThinhGiang.NhanVien.NhanVienTrinhDo.HocHam != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.NhanVienTrinhDo.HocHam.TenHocHam : "";
                hd.DiaChiThuongTru = thanhLyHopDong.HopDongThinhGiang.NhanVien.DiaChiThuongTru != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                hd.NoiOHienNay = thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiOHienNay != null ? thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiOHienNay.FullDiaChi : "";
                hd.SoCMND = thanhLyHopDong.HopDongThinhGiang.NhanVien.CMND;
                hd.NgayCap = thanhLyHopDong.HopDongThinhGiang.NhanVien.NgayCap.ToString("d");
                hd.NoiCap = thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiCap != null ? "CA. " + thanhLyHopDong.HopDongThinhGiang.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(thanhLyHopDong.HopDongThinhGiang.ChucDanhChuyenMon) ? thanhLyHopDong.HopDongThinhGiang.ChucDanhChuyenMon : "";
                hd.DiaDiemLamViec = thanhLyHopDong.HopDongThinhGiang.BoPhan != null ? thanhLyHopDong.HopDongThinhGiang.BoPhan.TenBoPhan : "";
                GiangVienThinhGiang GiangVienThinhGiang = obs.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("Oid=?", thanhLyHopDong.HopDongThinhGiang.NhanVien.Oid));
                if (GiangVienThinhGiang != null && GiangVienThinhGiang.HocVi != null)
                {
                    hd.HocVi = GiangVienThinhGiang.HocVi.TenHocVi;
                }

                TaiKhoanNganHang taiKhoanNganHang = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien=? and TaiKhoanChinh", thanhLyHopDong.HopDongThinhGiang.NhanVien.Oid));
                if (taiKhoanNganHang != null)
                {
                    hd.SoTaiKhoan = taiKhoanNganHang.SoTaiKhoan;
                    hd.NganHang = taiKhoanNganHang.NganHang != null ? taiKhoanNganHang.NganHang.TenNganHang : "";
                    hd.MaNganHang = taiKhoanNganHang.NganHang != null ? taiKhoanNganHang.NganHang.MaQuanLy : "";
                }

                hd.SoHopDongThinhGiang = thanhLyHopDong.HopDongThinhGiang.SoHopDong;
                hd.NgayKyHopDongThinhGiang = thanhLyHopDong.HopDongThinhGiang.NgayKy.ToString("d");
                //hd.MonDay = thanhLyHopDong.HopDongThinhGiang.MonDay;
                decimal soTiet = 0, soTien1Tiet = 0, tongTien = 0;
                foreach (ChiTietHopDongThinhGiang chiTiet in thanhLyHopDong.HopDongThinhGiang.ListChiTietHopDongThinhGiang)
                {
                  

                        //hd.Lop += HamDungChung.GetTenLop(chiTiet.Lop) + ", ";
                        soTiet += chiTiet.SoTiet;
                        soTien1Tiet = chiTiet.HopDongThinhGiang.SoTien1Tiet;
                      
                }
                tongTien += soTiet * soTien1Tiet;
                if (hd.Lop != null && hd.Lop.Length > 2)
                    hd.Lop = hd.Lop.Remove(hd.Lop.Length - 2);
                hd.SoTiet = soTiet.ToString("N1");
                hd.SoTienTrenTiet = soTien1Tiet.ToString("N0");
                hd.TongTien = tongTien.ToString("N0");
                hd.TongTienBangChu = HamDungChung.DocTien(tongTien);
                list.Add(hd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "ThanhLyHopDongThinhGiang.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_ThanhLyHopDongThinhGiang>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in thanh lý hợp đồng thỉnh giảng trong hệ thống.");
        }
    }
}
