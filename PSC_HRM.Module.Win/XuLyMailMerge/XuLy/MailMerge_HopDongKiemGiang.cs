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
    public class MailMerge_HopDongKiemGiang : IMailMerge<IList<HopDong_KiemGiang>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_KiemGiang> hdList)
        {
            var list = new List<Non_HopDongKiemGiang>();
            Non_HopDongKiemGiang hd;
            foreach (HopDong_KiemGiang obj in hdList)
            {
                hd = new Non_HopDongKiemGiang();
                //Non_HopDong
                hd.Oid = obj.Oid.ToString();
                hd.DonViChuQuan = obj.ThongTinTruong != null ? obj.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = obj.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = obj.ThongTinTruong.TenBoPhan;
                hd.DiaChi = obj.DiaChi;
                hd.SoDienThoai = obj.DienThoai;
                hd.SoHopDong = obj.SoHopDong;
                hd.NgayKy = obj.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgayHieuLuc = obj.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.ChucVuNguoiKy = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu : "";
                hd.ChucVuNguoiKyVietHoa = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                hd.ChucDanhChuyenMon = HamDungChung.GetChucDanhNguoiKy(obj.NguoiKy);
                hd.DanhXungNguoiKy = obj.NguoiKy.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.NguoiKyVietHoa = obj.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = obj.NguoiKy.HoTen;
       
                
                hd.ChucDanhNguoiLaoDong = HamDungChung.GetChucDanh(obj.NhanVien);
                hd.DanhXungNLDVietHoa = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                hd.DanhXungNLDVietThường = obj.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                hd.NguoiLaoDongVietHoa = obj.NhanVien.HoTen.ToUpper();
                hd.NguoiLaoDongVietThuong = obj.NhanVien.HoTen;
                hd.QuocTich = obj.QuocTich.TenQuocGia;
                hd.NgaySinh = obj.NhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NgaySinhDate = obj.NhanVien.NgaySinh.ToString("dd/MM/yyyy");
                hd.NoiSinh = obj.NhanVien.NoiSinh != null ? obj.NhanVien.NoiSinh.TinhThanh != null ? obj.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : obj.NhanVien.NoiSinh.FullDiaChi : "";
                hd.QueQuan = obj.NhanVien.QueQuan != null ? obj.NhanVien.QueQuan.FullDiaChi : "";
                hd.TrinhDo = obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? obj.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                hd.ChuyenMon = obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? obj.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                HoSoBaoHiem bhxh = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", obj.NhanVien.Oid));
                hd.SoSoBHXH = bhxh != null ? String.Format("Số sổ: {0}; ngày tham gia: {1:d}", bhxh.SoSoBHXH, bhxh.NgayThamGiaBHXH) : "Chưa tham gia BHXH";
                hd.DiaChiThuongTru = obj.NhanVien.DiaChiThuongTru != null ? obj.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                hd.DienThoaiDiDong = obj.NhanVien.DienThoaiDiDong;
                hd.DienThoaiNhaRieng = obj.NhanVien.DienThoaiNhaRieng;
               
                hd.NoiOHienNay = obj.NhanVien.NoiOHienNay != null ? obj.NhanVien.NoiOHienNay.FullDiaChi : "";
                hd.NgayCap =  obj.NhanVien.NgayCap.ToString("d") ;
                hd.NoiCap = obj.NhanVien.NoiCap != null ? "CA. " + obj.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(obj.ChucDanhChuyenMon) ? obj.ChucDanhChuyenMon : "";
                hd.DiaDiemLamViec = obj.BoPhan != null ? obj.BoPhan.TenBoPhan : "";
                hd.CanCu = obj.CanCu;
                hd.SoCMND = obj.NhanVien.CMND;
                hd.CongViecTuyenDung = obj.NhanVien.CongViecTuyenDung != null ? obj.NhanVien.CongViecTuyenDung : "";
                hd.TuNgayDate = obj.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = obj.DenNgay.ToString("dd/MM/yyyy");
                hd.QuyenCaoNhatDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(obj.BoPhan);
             

                TaiKhoanNganHang taiKhoanNganHang = obs.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien=? and TaiKhoanChinh", obj.NhanVien.Oid));
                if (taiKhoanNganHang != null)
                {
                    hd.SoTaiKhoanNguoiLaoDong = taiKhoanNganHang.SoTaiKhoan;
                    hd.NganHangNguoiLaoDong = taiKhoanNganHang.NganHang != null ? taiKhoanNganHang.NganHang.TenNganHang : "";
                    hd.MaNganHang = taiKhoanNganHang.NganHang != null ? taiKhoanNganHang.NganHang.MaQuanLy : "";
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
              
                hd.MaSoThueNguoiLaoDong = obj.NhanVien.NhanVienThongTinLuong.MaSoThue;
                hd.MonDayKiemGiang = HamDungChung.TenMonHocTuPhanMenUIS(obj.MonHoc);
                hd.DonViCongTac = obj.TaiKhoa!=null ? obj.TaiKhoa.ToString():"";
                hd.ChucVuNhanVien = HamDungChung.GetChucDanh(obj.NhanVien);
                hd.TongTien = obj.PhuCapKiemGiang.ToString("N0");
                hd.TongTienBangChu = HamDungChung.DocTien(obj.PhuCapKiemGiang);
                hd.CongViecHienNay =  obj.NhanVien.CongViecHienNay != null ? obj.NhanVien.CongViecHienNay.ToString():"";
                hd.DiaChiThuongTru = obj.NhanVien.DiaChiThuongTru!=null ? obj.NhanVien.DiaChiThuongTru.ToString():"";
                //
                list.Add(hd);
             
            }
            //
            MailMergeTemplate[] merge = new MailMergeTemplate[1];
            merge[0] = HamDungChung.GetTemplate(obs, "HopDongKiemGiang.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_HopDongKiemGiang>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng kiêm giảng trong hệ thống.");
        }
    }
}
