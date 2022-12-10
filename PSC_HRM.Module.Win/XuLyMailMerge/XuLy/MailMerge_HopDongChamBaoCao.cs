using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.HopDong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_HopDongChamBaoCao : IMailMerge<IList<HopDong_ChamBaoCao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_ChamBaoCao> hdList)
        {
            var list = new List<Non_HopDongChamBaoCao>();
            Non_HopDongChamBaoCao hd;
            foreach (HopDong_ChamBaoCao obj in hdList)
            {
                hd = new Non_HopDongChamBaoCao();
                hd.Oid = obj.Oid.ToString();
                hd.DonViChuQuan = obj.ThongTinTruong != null ? obj.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = obj.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = obj.ThongTinTruong.TenBoPhan;
                hd.DiaChi = obj.DiaChi;
                hd.SoDienThoai = obj.DienThoai;
                hd.SoHopDong = obj.SoHopDong;
                hd.NgayKy = obj.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.ChucVuNguoiKy = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu : "";
                hd.DanhXungNguoiKy = HamDungChung.GetChucDanhNguoiKy(obj.NguoiKy);
                hd.NguoiKyVietHoa = obj.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = obj.NguoiKy.HoTen;
                hd.ChucDanhNguoiLaoDong = HamDungChung.GetChucDanh(obj.NhanVien);
                hd.NguoiLaoDongVietHoa = obj.NhanVien.HoTen.ToUpper();
                hd.NguoiLaoDongVietThuong = obj.NhanVien.HoTen;
                hd.QuocTich = obj.QuocTich.TenQuocGia;
                hd.NgaySinh = obj.NhanVien.NgaySinh.ToString("d");
                hd.NoiSinh = obj.NhanVien.NoiSinh != null ? obj.NhanVien.NoiSinh.TinhThanh != null ? obj.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                hd.QueQuan = obj.NhanVien.QueQuan != null ? obj.NhanVien.QueQuan.FullDiaChi : "";
                hd.TrinhDo = obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                hd.ChuyenMon = obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                HoSoBaoHiem bhxh = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", obj.NhanVien.Oid));
                hd.SoSoBHXH = bhxh != null ? String.Format("Số sổ: {0}; ngày tham gia: {1:d}", bhxh.SoSoBHXH, bhxh.NgayThamGiaBHXH) : "Chưa tham gia BHXH";
                hd.DiaChiThuongTru = obj.NhanVien.DiaChiThuongTru != null ? obj.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                hd.NoiOHienNay = obj.NhanVien.NoiOHienNay != null ? obj.NhanVien.NoiOHienNay.FullDiaChi : "";
                hd.SoCMND = obj.NhanVien.CMND;
                hd.NgayCap = obj.NhanVien.NgayCap.ToString("d");
                hd.NoiCap = obj.NhanVien.NoiCap != null ? "CA. " + obj.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(obj.ChucDanhChuyenMon) ? obj.ChucDanhChuyenMon : "";
                hd.DiaDiemLamViec = obj.BoPhan != null ? obj.BoPhan.TenBoPhan : "";
                hd.CanCu = obj.CanCu;

                TaiKhoanNganHang taiKhoanNganHang = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien=? and TaiKhoanChinh", obj.NhanVien.Oid));
                if (taiKhoanNganHang != null)
                {
                    hd.SoTaiKhoanNguoiLaoDong = taiKhoanNganHang.SoTaiKhoan;
                    hd.NganHangNguoiLaoDong = taiKhoanNganHang.NganHang != null ? taiKhoanNganHang.NganHang.TenNganHang : "";
                }

                if (obj.ThongTinTruong != null)
                {
                    taiKhoanNganHang = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("ThongTinTruong=? and TaiKhoanChinh", obj.ThongTinTruong.Oid));
                    if (taiKhoanNganHang != null)
                    {
                        hd.SoTaiKhoanTruong = taiKhoanNganHang.SoTaiKhoan;
                        hd.NganHangTruong = taiKhoanNganHang.NganHang != null ? taiKhoanNganHang.NganHang.TenNganHang : "";
                    }
                    hd.MaSoThueTruong = obj.ThongTinTruong.MaSoThue;
                }

                hd.NamHoc = obj.QuanLyHopDongThinhGiang.NamHoc != null ? obj.QuanLyHopDongThinhGiang.NamHoc.TenNamHoc : "";
                hd.MaSoThueNguoiLaoDong = obj.NhanVien.NhanVienThongTinLuong.MaSoThue;
                hd.MonDay = obj.MonChamBaoCao;
                Non_ChiTietHopDongChamBaoCao chiTietHopDong;
                decimal tongTien = 0;
                foreach (ChiTietHopDongChamBaoCao chiTiet in obj.ListChiTietHopDongChamBaoCao)
                {
                    chiTietHopDong = new Non_ChiTietHopDongChamBaoCao();
                    chiTietHopDong.Oid = obj.Oid.ToString();
                    chiTietHopDong.Lop = chiTiet.Lop;
                    chiTietHopDong.SiSo = chiTiet.SiSo.ToString();
                    chiTietHopDong.SoBaiBaoCao = chiTiet.BaoCao.ToString("N1");
                    chiTietHopDong.SoTien1BaiBaoCao = chiTiet.SoTien.ToString("N0");
                    chiTietHopDong.ThanhTien = chiTiet.ThanhTien.ToString("N0");
                    chiTietHopDong.ThueTNCN = chiTiet.ThueTNCN.ToString("N0");
                    chiTietHopDong.SoTienConLai = chiTiet.SoTienConLai.ToString("N0");

                    hd.Master.Add(chiTietHopDong);

                    tongTien += chiTiet.SoTienConLai;
                }
                hd.TongTien = tongTien.ToString("N0");
                hd.TongTienBangChu = HamDungChung.DocTien(tongTien);
                list.Add(hd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "HopDongChamBaoCao.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_HopDongChamBaoCao>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng chấm báo cáo trong hệ thống.");
        }
    }
}
