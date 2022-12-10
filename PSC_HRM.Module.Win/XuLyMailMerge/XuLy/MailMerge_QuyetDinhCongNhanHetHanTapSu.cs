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
    public class MailMerge_QuyetDinhCongNhanHetHanTapSu : IMailMerge<IList<QuyetDinhCongNhanHetHanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhCongNhanHetHanTapSu> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietCongNhanHetHanTapSu.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietCongNhanHetHanTapSu.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhCongNhanHetHanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhCongNhanHetHanTapSuCaNhan>();
            Non_QuyetDinhCongNhanHetHanTapSuCaNhan qd;
            foreach (QuyetDinhCongNhanHetHanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhCongNhanHetHanTapSuCaNhan();

                //Non_QuyetDinh
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                                
                foreach (ChiTietCongNhanHetHanTapSu item in quyetDinh.ListChiTietCongNhanHetHanTapSu)
                {
                    //Non_QuyetDinhNhanVien
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                    {
                        qd.DanhXungVietThuong = "ông";
                        qd.DanhXungVietHoa = "Ông";
                    }
                    else
                    {
                        qd.DanhXungVietThuong = "bà";
                        qd.DanhXungVietHoa = "Bà";
                    }
                    //HoSo
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("d") : "";
                    qd.NoiSinh = item.ThongTinNhanVien.NoiSinh != null ? item.ThongTinNhanVien.NoiSinh.FullDiaChi : "";
                    qd.DiaChiThuongTru = item.ThongTinNhanVien.DiaChiThuongTru != null ? item.ThongTinNhanVien.DiaChiThuongTru.FullDiaChi : "";
                    qd.DienThoaiDiDong = item.ThongTinNhanVien.DienThoaiDiDong;
                    qd.Email = item.ThongTinNhanVien.Email;
                    //NhanVienTrinhDo
                    qd.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ?
                        item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    qd.ChuyenMonDaoTao = item.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ?
                        item.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "" ;
                    qd.TruongDaoTao = item.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao != null ?
                        item.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao.TenTruongDaoTao : "";
                    //NhanVien
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu:"";
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.TenChucVu :
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    }
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NgayVaoCoQuan = item.ThongTinNhanVien.NgayVaoCoQuan.ToString("dd/MM/yyyy");
                    //NhanVienThongTinLuong
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    qd.HeSoLuong = item.BacLuong.HeSoLuong.ToString("N2");
                    qd.MaNgachLuongMoi = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.NgachLuongMoi = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    qd.HeSoLuongMoi = item.BacLuong.HeSoLuong.ToString("N2");
                    qd.BacLuongMoi = item.BacLuong.TenBacLuong;
                    //Non_QuyetDinhCongNhanHetHanTapSuCaNhan
                    qd.TuThang = item.ThongTinNhanVien.NgayVaoCoQuan.ToString("MM/yyyy");
                    qd.TuNgay = qd.NgayVaoCoQuan;
                    qd.DenNgay = item.NgayHetHanTapSu.ToString("dd/MM/yyyy");
                    qd.NgayHuongLuong = item.NgayHuongLuong.ToString("dd/MM/yyyy");
                    qd.MocNangLuong = item.MocNangLuong.ToString("dd/MM/yyyy");
                    qd.NgayHetHanTapSuDate = item.NgayHetHanTapSu.ToString("dd/MM/yyyy");
                    qd.TaiBoMon = item.TaiBoMon != null ? item.TaiBoMon.TenBoPhan : "";
                    qd.PhuCapUuDai = item.PhuCapUuDai.ToString();
                }

                
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhCongNhanHetHanTapSuCaNhan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhCongNhanHetHanTapSuCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công nhận đào tạo trong hệ thống.");

        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhCongNhanHetHanTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhCongNhanHetHanTapSu>();
            Non_QuyetDinhCongNhanHetHanTapSu qd;
            foreach (QuyetDinhCongNhanHetHanTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhCongNhanHetHanTapSu();
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
                Non_ChiTietQuyetDinhCongNhanHetHanTapSuMaster master = new Non_ChiTietQuyetDinhCongNhanHetHanTapSuMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhCongNhanHetHanTapSuDetail detail;
                quyetDinh.ListChiTietCongNhanHetHanTapSu.Sorting.Clear();
                quyetDinh.ListChiTietCongNhanHetHanTapSu.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietCongNhanHetHanTapSu item in quyetDinh.ListChiTietCongNhanHetHanTapSu)
                {
                    detail = new Non_ChiTietQuyetDinhCongNhanHetHanTapSuDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.BoMon = item.ThongTinNhanVien.TaiBoMon.TenBoPhan;
                    detail.NgayHetHanTapSu = item.NgayHetHanTapSu.ToString("dd/MM/yyyy");
                    detail.NgachLuong = item.NgachLuong.MaQuanLy;
                    detail.TenNgachLuong = item.NgachLuong.TenNgachLuong;
                    detail.BacLuong = item.BacLuong.MaQuanLy;
                    detail.HeSoLuong = item.BacLuong.HeSoLuong.ToString("N2");
                    detail.MocNangLuong = item.MocNangLuong.ToString("dd/MM/yyyy");
                    qd.Detail.Add(detail);
                    stt++;
                }

                qd.SoNguoiCongNhan = (stt - 1).ToString();
                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongNhanHetHanTapSuTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongNhanHethanTapSuTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhCongNhanHetHanTapSuTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhCongNhanHetHanTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công nhận đào tạo trong hệ thống.");
        }
    }
}
