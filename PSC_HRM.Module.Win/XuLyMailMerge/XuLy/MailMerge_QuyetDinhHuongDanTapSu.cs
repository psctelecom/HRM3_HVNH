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
    public class MailMerge_QuyetDinhHuongDanTapSu : IMailMerge<IList<QuyetDinhHuongDanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhHuongDanTapSu> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhHuongDanTapSu.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhHuongDanTapSu.Count > 1
                         select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhHuongDanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhHuongDanTapSuCaNhan>();
            Non_QuyetDinhHuongDanTapSuCaNhan qd;
            foreach (QuyetDinhHuongDanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuongDanTapSuCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NgayXacNhan = quyetDinh.NgayXacNhan.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.SoQDTD = quyetDinh.QuyetDinhTuyenDung.SoQuyetDinh != null ? quyetDinh.QuyetDinhTuyenDung.SoQuyetDinh.ToString():"";
                qd.NgayQDTD = quyetDinh.QuyetDinhTuyenDung != null ? quyetDinh.QuyetDinhTuyenDung.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy"):"";
                qd.HSPCTrachNhiem = quyetDinh.HSPCTrachNhiem.ToString();

                foreach (ChiTietQuyetDinhHuongDanTapSu item in quyetDinh.ListChiTietQuyetDinhHuongDanTapSu)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.DanhXungCanBoHuongDanVietHoa = item.CanBoHuongDan.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungCanBoHuongDanVietThuong = item.CanBoHuongDan.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    //
                    qd.ChucVuCanBoHuongDanVietThuong = item.CanBoHuongDan.ChucVu != null ? item.CanBoHuongDan.ChucVu.TenChucVu : "";
                    qd.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    

                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.CanBoHuongDan = item.CanBoHuongDan != null ? item.CanBoHuongDan.HoTen : "";
                    qd.ChucDanhCanBoHuongDan = item.CanBoHuongDan != null ? HamDungChung.GetChucDanh(item.CanBoHuongDan) : "";
                    qd.DonViCanBoHuongDan = item.BoPhanCanBoHuongDan != null ? HamDungChung.GetTenBoPhan(item.BoPhanCanBoHuongDan) : "";
                    qd.TuNgay = item.TuNgay != DateTime.MinValue ? item.TuNgay.ToString("d") : "";
                    qd.DenNgay = item.DenNgay != DateTime.MinValue ? item.DenNgay.ToString("d") : "";
                    qd.SoThang = item.TuNgay.TinhSoThang(item.DenNgay).ToString();
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuongDanTapSu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhHuongDanTapSuCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hướng dẫn tập sự trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhHuongDanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhHuongDanTapSu>();
            Non_QuyetDinhHuongDanTapSu qd;
            foreach (QuyetDinhHuongDanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuongDanTapSu();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.SoQDTD = quyetDinh.QuyetDinhTuyenDung.SoQuyetDinh != null ? quyetDinh.QuyetDinhTuyenDung.SoQuyetDinh.ToString() : "";
                qd.NgayQDTD = quyetDinh.QuyetDinhTuyenDung.NgayQuyetDinh != null ? quyetDinh.QuyetDinhTuyenDung.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NgayQDTD = quyetDinh.QuyetDinhTuyenDung.NgayQuyetDinh != null ? quyetDinh.QuyetDinhTuyenDung.NgayQuyetDinh.ToString("yyyy") : "";
                //master
                Non_ChiTietQuyetDinhHuongDanTapSuMaster master = new Non_ChiTietQuyetDinhHuongDanTapSuMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhHuongDanTapSuDetail detail;
                quyetDinh.ListChiTietQuyetDinhHuongDanTapSu.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhHuongDanTapSu.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhHuongDanTapSu item in quyetDinh.ListChiTietQuyetDinhHuongDanTapSu)
                {
                    detail = new Non_ChiTietQuyetDinhHuongDanTapSuDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.CanBoHuongDan = item.CanBoHuongDan != null ? item.CanBoHuongDan.HoTen : "";
                    detail.ChucDanhCanBoHuongDan = item.CanBoHuongDan != null ? HamDungChung.GetChucDanh(item.CanBoHuongDan) : "";
                    detail.DonViCanBoHuongDan = item.BoPhanCanBoHuongDan != null ? HamDungChung.GetTenBoPhan(item.BoPhanCanBoHuongDan) : "";
                    detail.TuNgay = item.TuNgay != DateTime.MinValue ? item.TuNgay.ToString("d") : "";
                    detail.DenNgay = item.DenNgay != DateTime.MinValue ? item.DenNgay.ToString("d") : "";
                    detail.SoThang = item.TuNgay.TinhSoThang(item.DenNgay).ToString();

                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhHuongDanTapSuTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhHuongDanTapSuTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhHuongDanTapSuTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhHuongDanTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hướng dẫn tập sự trong hệ thống.");
        }
    }
}
