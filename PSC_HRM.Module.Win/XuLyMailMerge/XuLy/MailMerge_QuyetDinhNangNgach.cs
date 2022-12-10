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
    public class MailMerge_QuyetDinhNangNgach : IMailMerge<IList<QuyetDinhNangNgach>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNangNgach> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangNgach.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangNgach.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangNgach> qdList)
        {
            var list = new List<Non_QuyetDinhNangNgachCaNhan>();
            Non_QuyetDinhNangNgachCaNhan qd;
            foreach (QuyetDinhNangNgach quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangNgachCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc=quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                foreach (ChiTietQuyetDinhNangNgach item in quyetDinh.ListChiTietQuyetDinhNangNgach)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NhomNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong: "";
                    qd.MaNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.MaQuanLy : "";
                    qd.NgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : "";
                    qd.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    qd.HeSoLuongCu = item.HeSoLuongCu.ToString("N2");
                    qd.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("d") : "";
                    qd.NgayHuongLuongCu = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("d") : "";
                    qd.NgayBoNhiemNgachCu = item.NgayBoNhiemNgachMoi != DateTime.MinValue ? item.NgayBoNhiemNgachCu.ToString("d") : "";
                    qd.NhomNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : "";
                    qd.MaNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.MaQuanLy : "";
                    qd.NgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : "";
                    qd.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    qd.HeSoLuongMoi = item.HeSoLuongMoi.ToString("N2");
                    qd.MocNangLuongMoi = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("d") : "";
                    qd.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("d") : "";
                    qd.NgayBoNhiemNgachMoi = item.NgayBoNhiemNgachMoi != DateTime.MinValue ? item.NgayBoNhiemNgachMoi.ToString("d") : "";
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangNgach.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangNgachCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng ngạch trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangNgach> qdList)
        {
            var list = new List<Non_QuyetDinhNangNgach>();
            Non_QuyetDinhNangNgach qd;
            foreach (QuyetDinhNangNgach quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangNgach();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                
                //master
                Non_ChiTietQuyetDinhNangNgachMaster master = new Non_ChiTietQuyetDinhNangNgachMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhNangNgachDetail detail;
                quyetDinh.ListChiTietQuyetDinhNangNgach.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangNgach.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhNangNgach item in quyetDinh.ListChiTietQuyetDinhNangNgach)
                {
                    detail = new Non_ChiTietQuyetDinhNangNgachDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.NhomNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : "";
                    detail.MaNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.MaQuanLy : "";
                    detail.NgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : "";
                    detail.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    detail.HeSoLuongCu = item.HeSoLuongCu.ToString("N2");
                    detail.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("d") : "";
                    detail.NgayHuongLuongCu = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("d") : "";
                    detail.NgayBoNhiemNgachCu = item.NgayBoNhiemNgachMoi != DateTime.MinValue ? item.NgayBoNhiemNgachCu.ToString("d") : "";
                    detail.NhomNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : "";
                    detail.MaNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.MaQuanLy : "";
                    detail.NgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : "";
                    detail.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    detail.HeSoLuongMoi = item.HeSoLuongMoi.ToString("N2");
                    detail.MocNangLuongMoi = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("d") : "";
                    detail.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("d") : "";
                    detail.NgayBoNhiemNgachMoi = item.NgayBoNhiemNgachMoi != DateTime.MinValue ? item.NgayBoNhiemNgachMoi.ToString("d") : "";

                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangNgachTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangNgachTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhNangNgachTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangNgach>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng ngạch trong hệ thống.");
        }
    }
}
