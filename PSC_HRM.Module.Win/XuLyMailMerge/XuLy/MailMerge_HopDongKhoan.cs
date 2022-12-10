using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.HopDong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_HopDongKhoan : IMailMerge<IList<HopDong_Khoan>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_Khoan> hdList)
        {
            var listThuViec = new List<Non_HopDongKhoan>();
            var listChinhThuc = new List<Non_HopDongKhoan>();
            Non_HopDongKhoan hd;

            foreach (HopDong_Khoan obj in hdList)
            {
                hd = new Non_HopDongKhoan();
                hd.Oid = obj.Oid.ToString();
                hd.DonViChuQuan = obj.ThongTinTruong != null ? obj.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = obj.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = obj.ThongTinTruong.TenBoPhan;
                hd.DiaChi = obj.DiaChi;
                hd.SoDienThoai = obj.DienThoai;
                hd.LoaiHopDong = obj.HinhThucHopDong != null ? obj.HinhThucHopDong.TenHinhThucHopDong : "";
                hd.SoHopDong = obj.SoHopDong;
                hd.NgayKy = obj.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.ChucVuNguoiKy = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu : "";
                hd.DanhXungNguoiKy = obj.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.NguoiKyVietHoa = obj.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = obj.NguoiKy.HoTen;

                if (obj.NguoiLaoDongCoTrongHoSo == true)
                {
                    hd.ChucDanhNguoiLaoDong = HamDungChung.GetChucDanh(obj.NhanVien);
                    hd.DanhXungNLDVietHoa = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    hd.DanhXungNLDVietThường = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    hd.NguoiLaoDongVietHoa = obj.NhanVien.HoTen.ToUpper();
                    hd.NguoiLaoDongVietThuong = obj.NhanVien.HoTen;
                    hd.NgaySinhDate = obj.NhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    hd.NgaySinh = obj.NhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NoiSinh = obj.NhanVien.NoiSinh != null ? obj.NhanVien.NoiSinh.TinhThanh != null ? obj.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : obj.NhanVien.NoiSinh.FullDiaChi : "";
                    hd.DiaChiThuongTru = obj.NhanVien.DiaChiThuongTru != null ? obj.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                    hd.NoiOHienNay = obj.NhanVien.NoiOHienNay != null ? obj.NhanVien.NoiOHienNay.FullDiaChi : "";
                    hd.SoCMND = obj.NhanVien.CMND;
                    hd.NgayCapDate = obj.NhanVien.NgayCap.ToString("dd/MM/yyyy");
                    hd.NgayCap = obj.NhanVien.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NoiCap = obj.NhanVien.NoiCap != null ? "CA. " + obj.NhanVien.NoiCap.TenTinhThanh : "";
                    hd.QueQuan = obj.NhanVien.QueQuan != null ? obj.NhanVien.QueQuan.TinhThanh != null ? obj.NhanVien.QueQuan.TinhThanh.TenTinhThanh : obj.NhanVien.QueQuan.FullDiaChi : "";
                    hd.DiaDiemLamViec = obj.BoPhan != null ? obj.BoPhan.TenBoPhan : "";

                    hd.QuocTich = obj.QuocTich != null ? obj.QuocTich.TenQuocGia : "";
                    hd.TrinhDo = obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    hd.ChuyenMon = obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    hd.NamTotNghiep = obj.NhanVien.NhanVienTrinhDo.NamTotNghiep > 0 ? obj.NhanVien.NhanVienTrinhDo.NamTotNghiep.ToString("d") : "";
                    HoSoBaoHiem bhxh = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", obj.NhanVien.Oid));
                    hd.SoSoBHXH = bhxh != null ? String.Format("Số sổ: {0}; ngày tham gia: {1:dd/MM/yyyy}", bhxh.SoSoBHXH, bhxh.NgayThamGiaBHXH) : "Chưa tham gia BHXH";
                }
                else
                {
                    hd.DanhXungNLDVietHoa = "Ông (Bà)";
                    hd.DanhXungNLDVietThường = "ông (bà)";
                    hd.NguoiLaoDongVietHoa = obj.HoTen.ToUpper();
                    hd.NguoiLaoDongVietThuong = obj.HoTen;
                    hd.NgaySinhDate = obj.NgaySinh.ToString("dd/MM/yyyy");
                    hd.NgaySinh = obj.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NoiSinh = obj.NoiSinh != null ? obj.NoiSinh.TinhThanh != null ? obj.NoiSinh.TinhThanh.TenTinhThanh : obj.NoiSinh.FullDiaChi : "";
                    hd.DiaChiThuongTru = !String.IsNullOrEmpty(obj.DiaChiThuongTru) ? obj.DiaChiThuongTru : "";
                    hd.SoCMND = obj.CMND;
                    hd.NgayCapDate = obj.NgayCap.ToString("dd/MM/yyyy");
                    hd.NgayCap = obj.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    hd.NoiCap = obj.NoiCap != null ? "CA. " + obj.NoiCap.TenTinhThanh : "";
                    hd.QueQuan = obj.NoiCap != null ? "CA. " + obj.NoiCap.TenTinhThanh : "";
                    hd.DiaDiemLamViec = !String.IsNullOrEmpty(obj.NoiLamViec) ? obj.NoiLamViec : "";
                }

                hd.ChucVu = obj.ChucVu != null ? obj.ChucVu.TenChucVu : "Không";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(obj.ChucDanhChuyenMon) ? obj.ChucDanhChuyenMon : "";
                decimal tienLuong = obj.TienLuong * 0.85m;
                hd.TienLuong = obj.Huong85PhanTramLuong ? String.Format("{0:N0} x 85% = {1:N0}", obj.TienLuong, tienLuong) : obj.TienLuong.ToString("N0");
                hd.TienLuongBangChu = obj.Huong85PhanTramLuong ? HamDungChung.DocTien(tienLuong) : HamDungChung.DocTien(obj.TienLuong);
                hd.TienAn = obj.PhuCapTienAn.ToString("N0");
                hd.TienXang = obj.PhuCapTienXang.ToString("N0");
                hd.CacKhoanKhac = obj.PhuCapTangThem.ToString("N0");
                hd.CanCu = obj.CanCu;
                hd.TuNgayDate = obj.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = obj.DenNgay.ToString("dd/MM/yyyy");
                hd.TuNgay = obj.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.DenNgay = obj.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                
                hd.DienThoaiDiDong = obj.NhanVien.DienThoaiDiDong;

                if (obj.HinhThucThanhToan == HinhThucThanhToanEnum.ThanhToanBangTienMat)
                    hd.HinhThucThanhToan = "tiền mặt"; 
                else
                    hd.HinhThucThanhToan = "qua thẻ ATM";

                if (obj.ThamGiaBHXH)
                    hd.ThamGiaBHXH = "Theo quy định của Nhà nước";
                else
                    hd.ThamGiaBHXH = "Không";

                if (obj.PhanLoai == HopDongKhoanEnum.ThuViec)
                    listThuViec.Add(hd);
                else
                    listChinhThuc.Add(hd);
            }

            MailMergeTemplate merge;
            if (listThuViec.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongKhoanThuViec.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongKhoan>(listThuViec, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng khoán trong hệ thống.");
            }
            if (listChinhThuc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "HopDongKhoanChinhThuc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_HopDongKhoan>(listChinhThuc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng khoán trong hệ thống.");
            }
        }
    }
}
