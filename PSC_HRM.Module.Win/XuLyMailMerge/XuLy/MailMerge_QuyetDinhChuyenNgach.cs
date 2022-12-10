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
    public class MailMerge_QuyetDinhChuyenNgach : IMailMerge<IList<QuyetDinhChuyenNgach>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChuyenNgach> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhChuyenNgach.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhChuyenNgach.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChuyenNgach> qdList)
        {
            var list = new List<Non_QuyetDinhChuyenNgachCaNhan>();
            Non_QuyetDinhChuyenNgachCaNhan qd;
            foreach (QuyetDinhChuyenNgach quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChuyenNgachCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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

                foreach (ChiTietQuyetDinhChuyenNgach item in quyetDinh.ListChiTietQuyetDinhChuyenNgach)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NhomNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.NhomNgachLuong.TenNhomNgachLuong : "";
                    qd.MaNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.MaQuanLy : "";
                    qd.NgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : "";
                    qd.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    qd.HeSoLuongCu = item.HeSoLuongCu.ToString("N2");
                    qd.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayHuongLuongCu = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NhomNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.NhomNgachLuong.TenNhomNgachLuong : "";
                    qd.MaNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.MaQuanLy : "";
                    qd.NgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : "";
                    qd.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    qd.HeSoLuongMoi = item.HeSoLuongMoi.ToString("N2");
                    qd.MocNangLuongMoi = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    //
                    qd.MocNangLuongMoiDate = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("dd/MM/yyyy") : "";
                    qd.NgayHuongLuongMoiDate = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("dd/MM/yyyy") : "";
                    //
                    qd.MocNangLuongCuDate = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("dd/MM/yyyy") : "";
                    qd.NgayHuongLuongCuDate = item.NgayHuongLuongCu != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("dd/MM/yyyy") : "";

                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhChuyenNgach.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChuyenNgachCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chuyển ngạch trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChuyenNgach> qdList)
        {
            var list = new List<Non_QuyetDinhChuyenNgach>();
            Non_QuyetDinhChuyenNgach qd;
            foreach (QuyetDinhChuyenNgach quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChuyenNgach();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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

                //master
                Non_ChiTietQuyetDinhChuyenNgachMaster master = new Non_ChiTietQuyetDinhChuyenNgachMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhChuyenNgachDetail detail;
                quyetDinh.ListChiTietQuyetDinhChuyenNgach.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhChuyenNgach.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhChuyenNgach item in quyetDinh.ListChiTietQuyetDinhChuyenNgach)
                {
                    detail = new Non_ChiTietQuyetDinhChuyenNgachDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.NhomNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.NhomNgachLuong.TenNhomNgachLuong : "";
                    detail.MaNgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.MaQuanLy : "";
                    detail.NgachLuongCu = item.NgachLuongCu != null ? item.NgachLuongCu.TenNgachLuong : "";
                    detail.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    detail.HeSoLuongCu = item.HeSoLuongCu.ToString("N2");
                    detail.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("d") : "";
                    detail.NgayHuongLuongCu = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongCu.ToString("d") : "";
                    //detail.NhomNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.NhomNgachLuong.TenNhomNgachLuong : "";
                    detail.MaNgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.MaQuanLy : "";
                    detail.NgachLuongMoi = item.NgachLuongMoi != null ? item.NgachLuongMoi.TenNgachLuong : "";
                    detail.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    detail.HeSoLuongMoi = item.HeSoLuongMoi.ToString("N2");
                    detail.MocNangLuongMoi = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("d") : "";
                    detail.NgayHuongLuongMoi = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("d") : "";

                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChuyenNgachTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChuyenNgachTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhChuyenNgachTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChuyenNgach>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chuyển ngạch trong hệ thống.");
        }
    }
}
