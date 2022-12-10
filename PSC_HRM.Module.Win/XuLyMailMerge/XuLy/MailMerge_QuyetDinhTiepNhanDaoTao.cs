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
    public class MailMerge_QuyetDinhTiepNhanDaoTao : IMailMerge<IList<QuyetDinhTiepNhanDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTiepNhanDaoTao> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietTiepNhanDaoTao.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietTiepNhanDaoTao.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTiepNhanDaoTao> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhanDaoTaoCaNhan>();
            Non_QuyetDinhTiepNhanDaoTaoCaNhan qd;
            foreach (QuyetDinhTiepNhanDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanDaoTaoCaNhan();
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
                qd.SoQuyetDinhGiaHanDaoTao = quyetDinh.QuyetDinhGiaHanDaoTao != null ? quyetDinh.QuyetDinhGiaHanDaoTao.SoQuyetDinh : "";
                //qd.NgayKyGH = quyetDinh.QuyetDinhGiaHanDaoTao.NgayHieuLuc != DateTime.MinValue ? quyetDinh.QuyetDinhGiaHanDaoTao.NgayHieuLuc.ToString("dd/MM/yyyy") : "";
                qd.NgayKyGH = quyetDinh.QuyetDinhGiaHanDaoTao !=null ? quyetDinh.QuyetDinhGiaHanDaoTao.NgayHieuLuc.ToString("dd/MM/yyyy") : "";
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                if (quyetDinh.QuyetDinhDaoTao != null)
                {
                    qd.HinhThucDaoTao = quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao != null ? quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao.TenHinhThucDaoTao : "";
                    qd.HinhThucDaoTao += quyetDinh.QuyetDinhDaoTao.DaoTaoTapTrung ? " (Tập trung)" : " (Không tập trung)";
                    qd.TruongDaoTao = quyetDinh.QuyetDinhDaoTao.TruongDaoTao != null ? quyetDinh.QuyetDinhDaoTao.TruongDaoTao.TenTruongDaoTao : "";
                    qd.QuocGia = quyetDinh.QuyetDinhDaoTao.QuocGia != null ? quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia : "";
                    qd.TrinhDoChuyenMon = quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon != null ? quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    qd.NganhDaoTao = quyetDinh.QuyetDinhDaoTao.NganhDaoTao != null ? quyetDinh.QuyetDinhDaoTao.NganhDaoTao.TenNganhDaoTao : "";
                    qd.KhoaDaoTao = quyetDinh.QuyetDinhDaoTao.KhoaDaoTao != null ? quyetDinh.QuyetDinhDaoTao.KhoaDaoTao.TenKhoaDaoTao : "";
                    qd.NguonKinhPhi = quyetDinh.QuyetDinhDaoTao.NguonKinhPhi != null ? quyetDinh.QuyetDinhDaoTao.NguonKinhPhi.TenNguonKinhPhi : "";
                    qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhDaoTao.TruongHoTro) ? quyetDinh.QuyetDinhDaoTao.TruongHoTro : "";
                    qd.TuNgayDaoTao = quyetDinh.QuyetDinhDaoTao.TuNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.TuNgay.ToString("dd/MM/yyyy") : "";
                    qd.DenNgayDaoTao = quyetDinh.QuyetDinhDaoTao.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.DenNgay.ToString("dd/MM/yyyy") : "";
                    qd.DenThangDaoTao = quyetDinh.QuyetDinhDaoTao.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.DenNgay.ToString("MM/yyyy") : "";
                    qd.NgayTroVeTruong = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                    qd.ThoiGianDaoTao = HamDungChung.GetThoiGian(quyetDinh.QuyetDinhDaoTao.TuNgay, quyetDinh.QuyetDinhDaoTao.DenNgay);
                    qd.NamDaoTao = quyetDinh.QuyetDinhDaoTao.NgayQuyetDinh.Year.ToString("####");
                    qd.SoCMND=quyetDinh.SoCongVan!=null ? quyetDinh.SoCongVan:"";

                }
                foreach (ChiTietTiepNhanDaoTao item in quyetDinh.ListChiTietTiepNhanDaoTao)
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
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.BoPhan);
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                    if (quyetDinh.QuyetDinhDaoTao != null)
                    {
                        ChiTietDaoTao chiTiet = obs.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhDaoTao.Oid, item.ThongTinNhanVien.Oid));
                        qd.ChuyenNganhDaoTao = chiTiet != null && chiTiet.ChuyenMonDaoTao != null ? chiTiet.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                        break;
                    }
                    
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanDaoTao.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanDaoTaoCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tiếp nhận đào tạo trong hệ thống.");

        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTiepNhanDaoTao> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhanDaoTao>();
            Non_QuyetDinhTiepNhanDaoTao qd;
            foreach (QuyetDinhTiepNhanDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanDaoTao();
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

                qd.HinhThucDaoTao = quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao != null ? quyetDinh.QuyetDinhDaoTao.HinhThucDaoTao.TenHinhThucDaoTao : "";
                qd.HinhThucDaoTao += quyetDinh.QuyetDinhDaoTao.DaoTaoTapTrung ? " (Tập trung)" : " (Không tập trung)";
                qd.TruongDaoTao = quyetDinh.QuyetDinhDaoTao.TruongDaoTao != null ? quyetDinh.QuyetDinhDaoTao.TruongDaoTao.TenTruongDaoTao : "";
                qd.QuocGia = quyetDinh.QuyetDinhDaoTao.QuocGia != null ? quyetDinh.QuyetDinhDaoTao.QuocGia.TenQuocGia : "";
                qd.TrinhDoChuyenMon = quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon != null ? quyetDinh.QuyetDinhDaoTao.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                qd.NganhDaoTao = quyetDinh.QuyetDinhDaoTao.NganhDaoTao != null ? quyetDinh.QuyetDinhDaoTao.NganhDaoTao.TenNganhDaoTao : "";
                qd.KhoaDaoTao = quyetDinh.QuyetDinhDaoTao.KhoaDaoTao != null ? quyetDinh.QuyetDinhDaoTao.KhoaDaoTao.TenKhoaDaoTao : "";
                qd.NguonKinhPhi = quyetDinh.QuyetDinhDaoTao.NguonKinhPhi != null ? quyetDinh.QuyetDinhDaoTao.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhDaoTao.TruongHoTro) ? quyetDinh.QuyetDinhDaoTao.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";


                //master
                Non_ChiTietQuyetDinhTiepNhanDaoTaoMaster master = new Non_ChiTietQuyetDinhTiepNhanDaoTaoMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhTiepNhanDaoTaoDetail detail;
                quyetDinh.ListChiTietTiepNhanDaoTao.Sorting.Clear();
                quyetDinh.ListChiTietTiepNhanDaoTao.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietTiepNhanDaoTao item in quyetDinh.ListChiTietTiepNhanDaoTao)
                {
                    detail = new Non_ChiTietQuyetDinhTiepNhanDaoTaoDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    ChiTietDaoTao chiTietDaoTao = obs.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.QuyetDinhDaoTao.Oid, item.ThongTinNhanVien.Oid));
                    detail.ChuyenMonDaoTao = chiTietDaoTao != null && chiTietDaoTao.ChuyenMonDaoTao != null ? chiTietDaoTao.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTiepNhanDaoTaoTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTiepNhanDaoTaoTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanDaoTaoTapThe.rtf");
            if (merge[0] != null && merge[1] != null && merge[2] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanDaoTao>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tiếp nhận đào tạo trong hệ thống.");
        }
    }
}
