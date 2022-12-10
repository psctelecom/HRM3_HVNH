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
    public class MailMerge_QuyetDinhDenBuChiPhiDaoTao : IMailMerge<IList<QuyetDinhDenBuChiPhiDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhDenBuChiPhiDaoTao> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietDenBuChiPhiDaoTao.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietDenBuChiPhiDaoTao.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDenBuChiPhiDaoTao> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhDenBuChiPhiDaoTaoCaNhan>();
            var listNgoaiNuoc = new List<Non_QuyetDinhDenBuChiPhiDaoTaoCaNhan>();
            Non_QuyetDinhDenBuChiPhiDaoTaoCaNhan qd;
            foreach (QuyetDinhDenBuChiPhiDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDenBuChiPhiDaoTaoCaNhan();
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
                qd.GhiChu = quyetDinh.GhiChu;

                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.SoTien = quyetDinh.SoTien;
                qd.SoTienBangChu = quyetDinh.SoTienBangChu;
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
                foreach (ChiTietDenBuChiPhiDaoTao item in quyetDinh.ListChiTietDenBuChiPhiDaoTao)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.HeSoLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");

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

                if (quyetDinh.QuocGia != null &&
                    HamDungChung.CauHinhChung.QuocGia != null &&
                    quyetDinh.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    qd.NuocDaoTao = "nước ngoài";
                    listNgoaiNuoc.Add(qd);
                }
                else
                {
                    qd.NuocDaoTao = "trong nước";
                    listTrongNuoc.Add(qd);
                }

                //list.Add(qd);
            }

            MailMergeTemplate merge;
            if (listTrongNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhDenBuChiPhiDaoTaoTrongNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDenBuChiPhiDaoTaoCaNhan>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");

            }
            if (listNgoaiNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhDenBuChiPhiDaoTaoNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDenBuChiPhiDaoTaoCaNhan>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");
            }

        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDenBuChiPhiDaoTao> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhDenBuChiPhiDaoTao>();
            var listNgoaiNuoc = new List<Non_QuyetDinhDenBuChiPhiDaoTao>();
            Non_QuyetDinhDenBuChiPhiDaoTao qd;
            foreach (QuyetDinhDenBuChiPhiDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDenBuChiPhiDaoTao();
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
                Non_ChiTietQuyetDinhDenBuChiPhiDaoTaoMaster master = new Non_ChiTietQuyetDinhDenBuChiPhiDaoTaoMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhDenBuChiPhiDaoTaoDetail detail;
                quyetDinh.ListChiTietDenBuChiPhiDaoTao.Sorting.Clear();
                quyetDinh.ListChiTietDenBuChiPhiDaoTao.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietDenBuChiPhiDaoTao item in quyetDinh.ListChiTietDenBuChiPhiDaoTao)
                {
                    detail = new Non_ChiTietQuyetDinhDenBuChiPhiDaoTaoDetail();
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

                if (quyetDinh.QuocGia != null &&
                    HamDungChung.CauHinhChung.QuocGia != null &&
                    quyetDinh.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    qd.NuocDaoTao = "nước ngoài";
                    listNgoaiNuoc.Add(qd);
                }
                else
                {
                    qd.NuocDaoTao = "trong nước";
                    listTrongNuoc.Add(qd);
                }
                //list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDenBuChiPhiDaoTaoTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDenBuChiPhiDaoTaoTapTheDetail.rtf")); ;
            if (listNgoaiNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDenBuChiPhiDaoTaoNgoaiNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDenBuChiPhiDaoTao>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");
            }
            if (listTrongNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDenBuChiPhiDaoTaoTrongNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDenBuChiPhiDaoTao>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");
            }
        }
    }
}
