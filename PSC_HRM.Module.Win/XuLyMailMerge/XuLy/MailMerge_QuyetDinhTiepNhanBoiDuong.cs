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
    public class MailMerge_QuyetDinhTiepNhanBoiDuong : IMailMerge<IList<QuyetDinhTiepNhanBoiDuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTiepNhanBoiDuong> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietTiepNhanBoiDuong.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietTiepNhanBoiDuong.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTiepNhanBoiDuong> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhanBoiDuongCaNhan>();
            Non_QuyetDinhTiepNhanBoiDuongCaNhan qd;
            foreach (QuyetDinhTiepNhanBoiDuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanBoiDuongCaNhan();
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
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
               
                if (quyetDinh.QuyetDinhBoiDuong != null)
                {
                    qd.NguonKinhPhi = quyetDinh.QuyetDinhBoiDuong.NguonKinhPhi != null ? quyetDinh.QuyetDinhBoiDuong.NguonKinhPhi.TenNguonKinhPhi : "";
                    qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhBoiDuong.TruongHoTro) ? quyetDinh.QuyetDinhBoiDuong.TruongHoTro : "";
                    qd.TuNgayDaoTao = quyetDinh.QuyetDinhBoiDuong.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhBoiDuong.TuNgay.ToString("dd/MM/yyyy") : "";
                    qd.DenThangDaoTao = quyetDinh.QuyetDinhBoiDuong.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhBoiDuong.DenNgay.ToString("MM/yyyy") : "";
                }
                foreach (ChiTietTiepNhanBoiDuong item in quyetDinh.ListChiTietTiepNhanBoiDuong)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.HeSoLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                    
                    
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanBoiDuong.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanBoiDuongCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tiếp nhận bồi dưỡng trong hệ thống.");

        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTiepNhanBoiDuong> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhanBoiDuong>();
            Non_QuyetDinhTiepNhanBoiDuong qd;
            foreach (QuyetDinhTiepNhanBoiDuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanBoiDuong();
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

                
                qd.TruongDaoTao = quyetDinh.QuyetDinhBoiDuong.TruongDaoTao != null ? quyetDinh.QuyetDinhBoiDuong.TruongDaoTao : "";
                qd.QuocGia = quyetDinh.QuyetDinhBoiDuong.QuocGia != null ? quyetDinh.QuyetDinhBoiDuong.QuocGia.TenQuocGia : "";
                qd.NguonKinhPhi = quyetDinh.QuyetDinhBoiDuong.NguonKinhPhi != null ? quyetDinh.QuyetDinhBoiDuong.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhBoiDuong.TruongHoTro) ? quyetDinh.QuyetDinhBoiDuong.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";


                //master
                Non_ChiTietQuyetDinhTiepNhanBoiDuongMaster master = new Non_ChiTietQuyetDinhTiepNhanBoiDuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhTiepNhanBoiDuongDetail detail;
                quyetDinh.ListChiTietTiepNhanBoiDuong.Sorting.Clear();
                quyetDinh.ListChiTietTiepNhanBoiDuong.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietTiepNhanBoiDuong item in quyetDinh.ListChiTietTiepNhanBoiDuong)
                {
                    detail = new Non_ChiTietQuyetDinhTiepNhanBoiDuongDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    ChiTietDaoTao chiTietDaoTao = obs.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhBoiDuong=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhBoiDuong.Oid, item.ThongTinNhanVien.Oid));
                    detail.ChuyenMonDaoTao = chiTietDaoTao != null && chiTietDaoTao.ChuyenMonDaoTao != null ? chiTietDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTiepNhanBoiDuongTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTiepNhanBoiDuongTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanBoiDuongTapThe.rtf");
            if (merge[0] != null && merge[1] != null && merge[2] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanBoiDuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tiếp nhận bồi dưỡng trong hệ thống.");
        }
    }
}
