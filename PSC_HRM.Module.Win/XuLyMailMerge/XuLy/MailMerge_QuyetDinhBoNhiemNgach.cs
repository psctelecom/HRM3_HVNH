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
    public class MailMerge_QuyetDinhBoNhiemNgach : IMailMerge<IList<QuyetDinhBoNhiemNgach>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhBoNhiemNgach> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhBoNhiemNgach.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhBoNhiemNgach.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhBoNhiemNgach> qdList)
        {
            var list = new List<Non_QuyetDinhBoNhiemNgachCaNhan>();
            Non_QuyetDinhBoNhiemNgachCaNhan qd;
            foreach (QuyetDinhBoNhiemNgach quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoNhiemNgachCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;

                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayQuyetDinh == DateTime.MinValue)
                {
                    qd.NgayQuyetDinh = HamDungChung.GetServerTime().ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayQuyetDinhDate = HamDungChung.GetServerTime().ToString("d");
                }
                else
                {
                    qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("d");
                }

                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = HamDungChung.GetServerTime().ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = HamDungChung.GetServerTime().ToString("d");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("d");
                }          
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NgayXacNhan = quyetDinh.NgayXacNhan.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.SoQDTD = quyetDinh.DeNghiBoNhiemNgach != null ? quyetDinh.DeNghiBoNhiemNgach.QuyetDinhHuongDanTapSu.QuyetDinhTuyenDung.SoQuyetDinh : "";
                qd.SoQDHDTS = quyetDinh.DeNghiBoNhiemNgach != null ? quyetDinh.DeNghiBoNhiemNgach.QuyetDinhHuongDanTapSu.SoQuyetDinh : "";
                qd.NgayTungDung = quyetDinh.DeNghiBoNhiemNgach != null ? quyetDinh.DeNghiBoNhiemNgach.QuyetDinhHuongDanTapSu.QuyetDinhTuyenDung.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NgayHuongDanTapSu = quyetDinh.DeNghiBoNhiemNgach != null ? quyetDinh.DeNghiBoNhiemNgach.QuyetDinhHuongDanTapSu.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";

                foreach (ChiTietQuyetDinhBoNhiemNgach item in quyetDinh.ListChiTietQuyetDinhBoNhiemNgach)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NhomNgachLuong = item.NgachLuong != null ? item.NgachLuong.NhomNgachLuong.TenNhomNgachLuong : "";
                    qd.MaNgachLuong = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong.ToLower() : "";
                    qd.BacLuong = item.BacLuong != null ? item.BacLuong.TenBacLuong : "";
                    qd.HeSoLuong = item.HeSoLuong.ToString("N2");
                   
                    qd.MocNangLuong = item.MocNangLuong != DateTime.MinValue ? item.MocNangLuong.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayHuongLuong = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.NgayBoNhiemNgach = item.NgayBoNhiemNgach != DateTime.MinValue ? item.NgayBoNhiemNgach.ToString("'ngày' dd 'tháng' MM 'năm' yyyy"): "";
                   
                    qd.MocNangLuongDate = item.MocNangLuong != DateTime.MinValue ? item.MocNangLuong.ToString("dd/MM/yyyy") : "";
                    qd.NgayHuongLuongDate = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("dd/MM/yyyy") : "";
                    qd.NgayBoNhiemNgachDate = item.NgayBoNhiemNgach != DateTime.MinValue ? item.NgayBoNhiemNgach.ToString("dd/MM/yyyy") : "";
                    qd.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoNhiemNgach.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhBoNhiemNgachCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bổ nhiệm ngạch trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhBoNhiemNgach> qdList)
        {
            var list = new List<Non_QuyetDinhBoNhiemNgach>();
            Non_QuyetDinhBoNhiemNgach qd;
            foreach (QuyetDinhBoNhiemNgach quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoNhiemNgach();
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

                //master
                Non_ChiTietQuyetDinhBoNhiemNgachMaster master = new Non_ChiTietQuyetDinhBoNhiemNgachMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhBoNhiemNgachDetail detail;
                quyetDinh.ListChiTietQuyetDinhBoNhiemNgach.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhBoNhiemNgach.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhBoNhiemNgach item in quyetDinh.ListChiTietQuyetDinhBoNhiemNgach)
                {
                    detail = new Non_ChiTietQuyetDinhBoNhiemNgachDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.NhomNgachLuong = item.NgachLuong != null ? item.NgachLuong.NhomNgachLuong.TenNhomNgachLuong : "";
                    detail.MaNgachLuong = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    detail.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong.ToLower() : "";
                    detail.BacLuong = item.BacLuong != null ? item.BacLuong.TenBacLuong : "";
                    detail.HeSoLuong = item.HeSoLuong.ToString("N2");
                    detail.MocNangLuong = item.MocNangLuong != DateTime.MinValue ? item.MocNangLuong.ToString("d") : "";
                    detail.NgayHuongLuong = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("d") : "";
                    detail.NgayBoNhiemNgach = item.NgayBoNhiemNgach != DateTime.MinValue ? item.NgayBoNhiemNgach.ToString("d") : "";

                    //
                    qd.MaNgachLuong = item.NgachLuong!= null ? item.NgachLuong.MaQuanLy : string.Empty;
                    qd.NgayBoNhiemNgach = item.NgayBoNhiemNgach != DateTime.MinValue ? item.NgayBoNhiemNgach.ToString("d") : "";
                    //
                    qd.Detail.Add(detail);
                    stt++;
                }

                qd.SoLuongCanBo = Convert.ToString(stt - 1);
                //
                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhBoNhiemNgachTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhBoNhiemNgachTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhBoNhiemNgachTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhBoNhiemNgach>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bổ nhiệm ngạch trong hệ thống.");
        }
    }
}
