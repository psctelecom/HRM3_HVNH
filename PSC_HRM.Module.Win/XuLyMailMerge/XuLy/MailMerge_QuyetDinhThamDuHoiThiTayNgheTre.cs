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
    public class MailMerge_QuyetDinhThamDuHoiThiTayNgheTre : IMailMerge<IList<QuyetDinhThamDuHoiThiTayNgheTre>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThamDuHoiThiTayNgheTre> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhThamDuHoiThiTayNgheTre.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhThamDuHoiThiTayNgheTre.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhThamDuHoiThiTayNgheTre> qdList)
        {
            var list = new List<Non_QuyetDinhThamDuHoiThiTayNgheTreCaNhan>();
            Non_QuyetDinhThamDuHoiThiTayNgheTreCaNhan qd;
            foreach (QuyetDinhThamDuHoiThiTayNgheTre quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThamDuHoiThiTayNgheTreCaNhan();
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

                qd.DiaDiem = quyetDinh.DiaDiem ?? "";
                qd.SoCongVan = quyetDinh.SoCongVan ?? "";
                qd.DonViToChuc = quyetDinh.DonViToChuc ?? "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.LyDo = quyetDinh.LyDo ?? "";

                foreach (ChiTietQuyetDinhThamDuHoiThiTayNgheTre item in quyetDinh.ListChiTietQuyetDinhThamDuHoiThiTayNgheTre)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    if (item.ViTriCongTac != null)
                        qd.ViTri = item.ViTriCongTac.TenViTriCongTac;
                    if (item.NgheThamGia != null)
                        qd.NgheThamGia = item.NgheThamGia.TenChuyenMonDaoTao;
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhThamDuHoiThiTayNgheTre.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThamDuHoiThiTayNgheTreCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi công tác trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhThamDuHoiThiTayNgheTre> qdList)
        {
            var list = new List<Non_QuyetDinhThamDuHoiThiTayNgheTre>();
            Non_QuyetDinhThamDuHoiThiTayNgheTre qd;
            foreach (QuyetDinhThamDuHoiThiTayNgheTre quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThamDuHoiThiTayNgheTre();
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

                qd.DiaDiem = quyetDinh.DiaDiem ?? "";
                qd.SoCongVan = quyetDinh.SoCongVan ?? "";
                qd.DonViToChuc = quyetDinh.DonViToChuc ?? "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.LyDo = quyetDinh.LyDo ?? "";

                //master
                Non_ChiTietQuyetDinhThamDuHoiThiTayNgheTreMaster master = new Non_ChiTietQuyetDinhThamDuHoiThiTayNgheTreMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhThamDuHoiThiTayNgheTreDetail detail;
                quyetDinh.ListChiTietQuyetDinhThamDuHoiThiTayNgheTre.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhThamDuHoiThiTayNgheTre.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
           
                foreach (ChiTietQuyetDinhThamDuHoiThiTayNgheTre item in quyetDinh.ListChiTietQuyetDinhThamDuHoiThiTayNgheTre)
                {
                    detail = new Non_ChiTietQuyetDinhThamDuHoiThiTayNgheTreDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    if (item.ViTriCongTac != null)
                        detail.ViTri = item.ViTriCongTac.TenViTriCongTac;
                    if (item.NgheThamGia != null)
                        detail.NgheThamGia = item.NgheThamGia.TenChuyenMonDaoTao;

                    qd.Detail.Add(detail);
                    stt++;
                   
                }
                master.TongSoNguoi = Convert.ToString(stt - 1);

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThamDuHoiThiTayNgheTreTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThamDuHoiThiTayNgheTreTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhThamDuHoiThiTayNgheTreTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThamDuHoiThiTayNgheTre>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi công tác trong hệ thống.");
        }
    }
}
