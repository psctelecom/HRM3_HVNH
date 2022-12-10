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
    public class MailMerge_HopDongThinhGiangChatLuongCao : IMailMerge<IList<HopDong_ThinhGiangChatLuongCao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<HopDong_ThinhGiangChatLuongCao> hdList)
        {
            List<Non_HopDongThinhGiangChatLuongCao> list = new List<Non_HopDongThinhGiangChatLuongCao>();
            Non_HopDongThinhGiangChatLuongCao hd;
            foreach (HopDong_ThinhGiangChatLuongCao hopDongThinhGiang in hdList)
            {
                hd = new Non_HopDongThinhGiangChatLuongCao();
                hd.Oid = hopDongThinhGiang.Oid.ToString();
                hd.DonViChuQuan = hopDongThinhGiang.ThongTinTruong != null ? hopDongThinhGiang.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                hd.TenTruongVietHoa = hopDongThinhGiang.ThongTinTruong.TenBoPhan.ToUpper();
                hd.TenTruongVietThuong = hopDongThinhGiang.ThongTinTruong.TenBoPhan;
                hd.DiaChi = hopDongThinhGiang.DiaChi;
                hd.SoDienThoai = hopDongThinhGiang.DienThoai;
                hd.SoHopDong = hopDongThinhGiang.SoHopDong;
                hd.NgayKy = hopDongThinhGiang.NgayKy.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.ChucVuNguoiKy = hopDongThinhGiang.ChucVuNguoiKy != null ? hopDongThinhGiang.ChucVuNguoiKy.TenChucVu : "";
                hd.DanhXungNguoiKy = HamDungChung.GetChucDanhNguoiKy(hopDongThinhGiang.NguoiKy);
                hd.NguoiKyVietHoa = hopDongThinhGiang.NguoiKy.HoTen.ToUpper();
                hd.NguoiKyVietThuong = hopDongThinhGiang.NguoiKy.HoTen;
                hd.ChucDanhNguoiLaoDong = hopDongThinhGiang.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông " : "bà ";
                hd.DanhXungNguoiLaoDongVietHoa = hopDongThinhGiang.NhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông " : "Bà ";
                hd.NguoiLaoDongVietHoa = hopDongThinhGiang.NhanVien.HoTen.ToUpper();
                hd.NguoiLaoDongVietThuong = hopDongThinhGiang.NhanVien.HoTen;
                hd.QuocTich = hopDongThinhGiang.QuocTich.TenQuocGia;
                hd.NgaySinh = hopDongThinhGiang.NhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                hd.NoiSinh = hopDongThinhGiang.NhanVien.NoiSinh != null ? hopDongThinhGiang.NhanVien.NoiSinh.TinhThanh != null ? hopDongThinhGiang.NhanVien.NoiSinh.TinhThanh.TenTinhThanh : "" : "";
                hd.QueQuan = hopDongThinhGiang.NhanVien.QueQuan != null ? hopDongThinhGiang.NhanVien.QueQuan.FullDiaChi : "";
                hd.TrinhDo = hopDongThinhGiang.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? hopDongThinhGiang.NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                hd.ChuyenMon = hopDongThinhGiang.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? hopDongThinhGiang.NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                HoSoBaoHiem bhxh = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", hopDongThinhGiang.NhanVien.Oid));
                hd.SoSoBHXH = bhxh != null ? String.Format("Số sổ: {0}; ngày tham gia: {1:d}", bhxh.SoSoBHXH, bhxh.NgayThamGiaBHXH) : "Chưa tham gia BHXH";
                hd.DiaChiThuongTru = hopDongThinhGiang.NhanVien.DiaChiThuongTru != null ? hopDongThinhGiang.NhanVien.DiaChiThuongTru.FullDiaChi : "";
                hd.NoiOHienNay = hopDongThinhGiang.NhanVien.NoiOHienNay != null ? hopDongThinhGiang.NhanVien.NoiOHienNay.FullDiaChi : "";
                hd.NgayCap = hopDongThinhGiang.NhanVien.NgayCap.ToString("d");
                hd.NoiCap = hopDongThinhGiang.NhanVien.NoiCap != null ? "CA. " + hopDongThinhGiang.NhanVien.NoiCap.TenTinhThanh : "";
                hd.ChucDanhChuyenMon = hopDongThinhGiang.ChucDanhChuyenMon ?? "";
                hd.DiaDiemLamViec = hopDongThinhGiang.BoPhan != null ? hopDongThinhGiang.BoPhan.TenBoPhan : "";
                hd.MaSoThue = hopDongThinhGiang.NhanVien.NhanVienThongTinLuong.MaSoThue;
                hd.DienThoaiNhaRieng = hopDongThinhGiang.NhanVien.DienThoaiNhaRieng ?? "";
                hd.DienThoaiDiDong = hopDongThinhGiang.NhanVien.DienThoaiDiDong ?? "";
                hd.Email = hopDongThinhGiang.NhanVien.Email ?? "";
                hd.HocVi = hopDongThinhGiang.NhanVien.NhanVienTrinhDo.HocHam != null ? hopDongThinhGiang.NhanVien.NhanVienTrinhDo.HocHam.TenHocHam : "";

                foreach (TaiKhoanNganHang taiKhoan in hopDongThinhGiang.NhanVien.ListTaiKhoanNganHang)
                {
                    if (taiKhoan.TaiKhoanChinh)
                    {
                        hd.SoTaiKhoanNguoiLaoDong = taiKhoan.SoTaiKhoan;
                        hd.NganHangNguoiLaoDong = taiKhoan.NganHang != null ? taiKhoan.NganHang.TenNganHang : "";
                        //hd.DiaChiNganHangNguoiLaoDong = taiKhoan.NganHang.DiaChiChiNhanhNganHang != null ? taiKhoan.NganHang.DiaChiChiNhanhNganHang.FullDiaChi.ToString() : "";
                    }
                }

                hd.NamHoc = hopDongThinhGiang.QuanLyHopDongThinhGiang.NamHoc != null ? hopDongThinhGiang.QuanLyHopDongThinhGiang.NamHoc.TenNamHoc : "";
                hd.HocKy = hopDongThinhGiang.QuanLyHopDongThinhGiang.HocKy != null ? hopDongThinhGiang.QuanLyHopDongThinhGiang.HocKy.TenHocKy : "";

                //master
                Non_HopDongThinhGiangMaster master = new Non_HopDongThinhGiangMaster();
                master.Oid = hopDongThinhGiang.Oid.ToString();
                hd.Master.Add(master);

                //detail
                foreach (ChiTietHopDongThinhGiangChatLuongCao chiTiet in hopDongThinhGiang.ListChiTietHopDongThinhGiangChatLuongCao)
                {
                    Non_HopDongThinhGiangDetail chiTietHopDong = new Non_HopDongThinhGiangDetail();
                    //
                    chiTietHopDong.Oid = hopDongThinhGiang.Oid.ToString();
                    chiTietHopDong.SoTiet = chiTiet.SoTiet.ToString("N1");
                    chiTietHopDong.SoTien1Tiet = hopDongThinhGiang.SoTien1Tiet.ToString("N0");
                    //
                    if (!string.IsNullOrEmpty(chiTiet.MonHoc))
                        chiTietHopDong.MonHoc = String.Format("{0} - {1}", chiTiet.MonHoc, HamDungChung.TenMonHocTuPhanMenUIS(chiTiet.MonHoc));
                    //
                    hd.Detail.Add(chiTietHopDong);
                }
                //
                list.Add(hd);
            }
            //
            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiangChatLuongCaoMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "HopDongThinhGiangChatLuongCaoDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "HopDongThinhGiangChatLuongCao.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_HopDongThinhGiangChatLuongCao>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in hợp đồng thỉnh giảng trong hệ thống.");
        }
    }
}
