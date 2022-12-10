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
    public class MailMerge_ThongBaoGiaHanTapSu : IMailMerge<IList<ThongBaoGiaHanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<ThongBaoGiaHanTapSu> tbList)
        {
            var caNhan = from tb in tbList
                         where tb.ListChiTietThongBaoGiaHanTapSu.Count == 1
                         select tb;

            var tapThe = from tb in tbList
                         where tb.ListChiTietThongBaoGiaHanTapSu.Count > 1
                         select tb;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<ThongBaoGiaHanTapSu> tbList)
        {
            var list = new List<Non_ThongBaoGiaHanTapSuCaNhan>();
            Non_ThongBaoGiaHanTapSuCaNhan tb;
            foreach (ThongBaoGiaHanTapSu thongBao in tbList)
            {
                tb = new Non_ThongBaoGiaHanTapSuCaNhan();
                tb.Oid = thongBao.Oid.ToString();
                tb.DonViChuQuan = thongBao.ThongTinTruong != null ? thongBao.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                tb.TenTruongVietHoa = thongBao.TenCoQuan != null ? thongBao.TenCoQuan.ToUpper() : "";
                tb.TenTruongVietThuong = thongBao.TenCoQuan;
                tb.SoQuyetDinh = thongBao.SoQuyetDinh;
                tb.NgayQuyetDinh = thongBao.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                tb.NgayHieuLuc = thongBao.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                tb.CanCu = thongBao.CanCu;
                tb.NoiDung = thongBao.NoiDung;
                tb.ChucVuNguoiKy = thongBao.ChucVuNguoiKy != null ? thongBao.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                tb.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(thongBao.NguoiKy);
                tb.NguoiKy = thongBao.NguoiKy1;

                foreach (ChiTietThongBaoGiaHanTapSu item in thongBao.ListChiTietThongBaoGiaHanTapSu)
                {
                    tb.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    tb.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    tb.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    tb.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    tb.NhanVien = item.ThongTinNhanVien.HoTen;
                    tb.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    tb.MaNgach = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    tb.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    tb.NgayBatDauTapSu = item.NgayBatDauTapSu.ToString("dd/MM/yyyy");
                    tb.NgayKetThucTapSu = item.NgayKetThucTapSu.ToString("dd/MM/yyyy");
                    tb.ThoiGianGiaHan = item.ThoiGianGiaHan.ToString("N0");
                    tb.NgayDuocGiaHan = item.NgayDuocGiaHan != DateTime.MinValue ? item.NgayDuocGiaHan.ToString("d") : "";
                    tb.LyDo = item.LyDo;
                    break;
                }
                list.Add(tb);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "ThongBaoGiaHanTapSu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_ThongBaoGiaHanTapSuCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in TB chưa được công nhận tập sự trong hệ thống.");

        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<ThongBaoGiaHanTapSu> tbList)
        {
            var list = new List<Non_ThongBaoGiaHanTapSu>();
            Non_ThongBaoGiaHanTapSu tb;
            foreach (ThongBaoGiaHanTapSu thongBao in tbList)
            {
                tb = new Non_ThongBaoGiaHanTapSu();
                tb.Oid = thongBao.Oid.ToString();
                tb.DonViChuQuan = thongBao.ThongTinTruong != null ? thongBao.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                tb.TenTruongVietHoa = thongBao.TenCoQuan != null ? thongBao.TenCoQuan.ToUpper() : "";
                tb.TenTruongVietThuong = thongBao.TenCoQuan;
                tb.SoQuyetDinh = thongBao.SoQuyetDinh;
                tb.NgayQuyetDinh = thongBao.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                tb.NgayHieuLuc = thongBao.NgayHieuLuc.ToString("d");
                tb.CanCu = thongBao.CanCu;
                tb.NoiDung = thongBao.NoiDung;
                tb.ChucVuNguoiKy = thongBao.ChucVuNguoiKy != null ? thongBao.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                tb.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(thongBao.NguoiKy);
                tb.NguoiKy = thongBao.NguoiKy1;
                
                //master
                Non_ChiTietThongBaoGiaHanTapSuMaster master = new Non_ChiTietThongBaoGiaHanTapSuMaster();
                master.Oid = thongBao.Oid.ToString();
                master.DonViChuQuan = tb.DonViChuQuan;
                master.TenTruongVietHoa = tb.TenTruongVietHoa;
                master.TenTruongVietThuong = tb.TenTruongVietThuong;
                master.SoQuyetDinh = tb.SoQuyetDinh;
                master.NguoiKy = tb.NguoiKy;
                master.NgayQuyetDinh = tb.NgayQuyetDinh;
                tb.Master.Add(master);

                //detail
                Non_ChiTietThongBaoGiaHanTapSuDetail detail;
                thongBao.ListChiTietThongBaoGiaHanTapSu.Sorting.Clear();
                thongBao.ListChiTietThongBaoGiaHanTapSu.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietThongBaoGiaHanTapSu item in thongBao.ListChiTietThongBaoGiaHanTapSu)
                {
                    detail = new Non_ChiTietThongBaoGiaHanTapSuDetail();
                    detail.Oid = thongBao.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.NgayBatDauTapSu = item.NgayBatDauTapSu.ToString("dd/MM/yyyy");
                    detail.NgayKetThucTapSu = item.NgayKetThucTapSu.ToString("dd/MM/yyyy");
                    detail.ThoiGianGiaHan = item.ThoiGianGiaHan.ToString();
                    detail.NgayDuocGiaHan = item.NgayDuocGiaHan.ToString("dd/MM/yyyy");
                    detail.LyDo = item.LyDo;
                    tb.Detail.Add(detail);
                    stt++;
                }
                tb.SoLuongCanBo = (stt - 1).ToString();
                list.Add(tb);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "ThongBaoGiaHanTapSuTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "ThongBaoGiaHanTapSuTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "ThongBaoGiaHanTapSuTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_ThongBaoGiaHanTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in TB chưa được công nhận tập sự trong hệ thống.");
        }
    }
}
