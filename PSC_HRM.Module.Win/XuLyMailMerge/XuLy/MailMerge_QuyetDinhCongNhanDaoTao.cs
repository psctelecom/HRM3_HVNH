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
    public class MailMerge_QuyetDinhCongNhanDaoTao : IMailMerge<IList<QuyetDinhCongNhanDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhCongNhanDaoTao> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietCongNhanDaoTao.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietCongNhanDaoTao.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhCongNhanDaoTao> qdList)
        {
            var list = new List<Non_QuyetDinhCongNhanDaoTaoCaNhan>();
            Non_QuyetDinhCongNhanDaoTaoCaNhan qd;
            foreach (QuyetDinhCongNhanDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhCongNhanDaoTaoCaNhan();
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
                    qd.DenThangDaoTao = quyetDinh.QuyetDinhDaoTao.DenNgay != DateTime.MinValue ? quyetDinh.QuyetDinhDaoTao.DenNgay.ToString("MM/yyyy") : "";
                }
                foreach (ChiTietCongNhanDaoTao item in quyetDinh.ListChiTietCongNhanDaoTao)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.HeSoLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
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

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhCongNhanDaoTao.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhCongNhanDaoTaoCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công nhận đào tạo trong hệ thống.");

        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhCongNhanDaoTao> qdList)
        {
            var list = new List<Non_QuyetDinhCongNhanDaoTao>();
            Non_QuyetDinhCongNhanDaoTao qd;
            foreach (QuyetDinhCongNhanDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhCongNhanDaoTao();
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
                Non_ChiTietQuyetDinhCongNhanDaoTaoMaster master = new Non_ChiTietQuyetDinhCongNhanDaoTaoMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhCongNhanDaoTaoDetail detail;
                quyetDinh.ListChiTietCongNhanDaoTao.Sorting.Clear();
                quyetDinh.ListChiTietCongNhanDaoTao.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietCongNhanDaoTao item in quyetDinh.ListChiTietCongNhanDaoTao)
                {
                    detail = new Non_ChiTietQuyetDinhCongNhanDaoTaoDetail();
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
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongNhanDaoTaoTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongNhanDaoTaoTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhCongNhanDaoTaoTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhCongNhanDaoTao>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công nhận đào tạo trong hệ thống.");
        }
    }
}
