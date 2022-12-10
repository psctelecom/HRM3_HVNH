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
    public class MailMerge_HopDongThinhGiang : IMailMerge<IList<HopDong_ThinhGiang>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_ThinhGiang> hdList)
        {
            var list = new List<Non_HopDongThinhGiang>();
            decimal tong = 0;
            Non_HopDongThinhGiang hd;
            foreach (HopDong_ThinhGiang obj in hdList)
            {
                hd = new Non_HopDongThinhGiang();
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
                hd.NgayCap = obj.NhanVien.NgayCap.ToString("d");
                hd.NoiCap = obj.NhanVien.NoiCap != null ? "CA. " + obj.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = !String.IsNullOrEmpty(obj.ChucDanhChuyenMon) ? obj.ChucDanhChuyenMon : "";
                hd.DiaDiemLamViec = obj.BoPhan != null ? obj.BoPhan.TenBoPhan : "";
                hd.CanCu = obj.CanCu;
                hd.SoTien1Tiet = obj.SoTien1Tiet.ToString("N0");
                hd.SoTien1TietBangChu = HamDungChung.DocTien(obj.SoTien1Tiet);
                hd.CongViecTuyenDung = obj.NhanVien.CongViecTuyenDung != null ? obj.NhanVien.CongViecTuyenDung : "";
                hd.TuNgayDate = obj.TuNgay.ToString("dd/MM/yyyy");
                hd.DenNgayDate = obj.DenNgay.ToString("dd/MM/yyyy");
                hd.QuyenCaoNhatDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(obj.BoPhan);
             

                        GiangVienThinhGiang GiangVienThinhGiang = obs.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("Oid=?", obj.NhanVien.Oid));
                if (GiangVienThinhGiang != null && GiangVienThinhGiang.DonViCongTac != null)
                {
                    hd.DonViCongTac = GiangVienThinhGiang.DonViCongTac;                    
                }

                if (GiangVienThinhGiang != null && GiangVienThinhGiang.CongViecHienNay != null)
                {
                    hd.CongViecHienNay = GiangVienThinhGiang.CongViecHienNay.TenCongViec;
                }

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
                hd.HocKy = obj.QuanLyHopDongThinhGiang.HocKy != null ? obj.QuanLyHopDongThinhGiang.HocKy.TenHocKy : "";
                hd.NamHoc = obj.QuanLyHopDongThinhGiang.NamHoc != null ? obj.QuanLyHopDongThinhGiang.NamHoc.TenNamHoc : "";
                hd.MaSoThueNguoiLaoDong = obj.NhanVien.NhanVienThongTinLuong.MaSoThue;
               
                //master
                Non_HopDongThinhGiangMaster master = new Non_HopDongThinhGiangMaster();
                master.Oid = obj.Oid.ToString();
                hd.Master.Add(master);

                //detail
                foreach (ChiTietHopDongThinhGiang chiTiet in obj.ListChiTietHopDongThinhGiang)
                {
                    foreach (ChiTietThanhToanHopDong ct in chiTiet.ListChiTietThanhToanHopDong)
                    {

                        Non_HopDongThinhGiangDetail  chiTietHopDong = new Non_HopDongThinhGiangDetail();
                    //
                    chiTietHopDong.Oid = obj.Oid.ToString();
                    chiTietHopDong.SoTiet =chiTiet.SoTiet.ToString("N1");
                    chiTietHopDong.SoTien1Tiet = obj.SoTien1Tiet.ToString("N0");
                    chiTietHopDong.BoMon = chiTiet.BoMon != null ? chiTiet.BoMon.TenBoPhan : "";
                    chiTietHopDong.TaiKhoa = chiTiet.TaiKhoa != null ? chiTiet.TaiKhoa.TenBoPhan : "";
                   
                        chiTietHopDong.NoiDung = ct.NoiDung;
                        chiTietHopDong.SoLuong = ct.SoLuong.ToString("N0");
                        chiTietHopDong.DonViTinh = ct.DonViTinh.ToString();
                        chiTietHopDong.DonGia = ct.DonGia.ToString("N0");
                        chiTietHopDong.ThanhTien = ct.ThanhTien.ToString("N0");
                        tong += ct.ThanhTien;

                     
                        if (!string.IsNullOrEmpty(chiTiet.MonHoc))
                    chiTietHopDong.MonHoc = String.Format("{0} - {1}", chiTiet.MonHoc, HamDungChung.TenMonHocTuPhanMenUIS(chiTiet.MonHoc));
                    //
                        hd.Detail.Add(chiTietHopDong);
                        hd.TongTien = tong.ToString("N0");
                        hd.TongTienBangChu = HamDungChung.DocTien(tong);
                        hd.MonDay = chiTiet.BoMon.ToString();
                        hd.SoTietLT = chiTiet.SoTietLT.ToString("N0");
                        hd.SoTietTH = chiTiet.SoTietTH.ToString("N0");
                        hd.Lop = chiTiet.Lop;
                        hd.SiSo = chiTiet.SiSo.ToString("N0");
                      
                    }
                    
                }
                //
                list.Add(hd);
             
            }
            //
            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiangQuyCheChiTieuNoiBoMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiangQuyCheChiTieuNoiBoDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "HopDongThinhGiangQuyCheChiTieuNoiBo.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_HopDongThinhGiang>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng thỉnh giảng trong hệ thống.");
        }
    }
}
