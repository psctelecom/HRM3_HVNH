using DevExpress.Data.Filtering;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhHuongCheDoDiHocTrongNuoc : IMailMerge<IList<QuyetDinhHuongCheDoDiHocTrongNuoc>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhHuongCheDoDiHocTrongNuoc> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhHuongCheDoDiHocTrongNuoc> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhHuongCheDoDiHocCaNhan>();
            var listNgoaiNuoc = new List<Non_QuyetDinhHuongCheDoDiHocCaNhan>();
            Non_QuyetDinhHuongCheDoDiHocCaNhan qd;
            foreach (QuyetDinhHuongCheDoDiHocTrongNuoc quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuongCheDoDiHocCaNhan();
                //Non_QuyetDinh
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.NoiDung = quyetDinh.NoiDung;
                qd.CanCu = quyetDinh.CanCu;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                //Non_QuyetDinhDaoTaoCaNhan
                //qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0}, năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
                qd.HinhThucDaoTao = quyetDinh.HinhThucDaoTao != null ? quyetDinh.HinhThucDaoTao.TenHinhThucDaoTao : "";
                //qd.HinhThucDaoTao += quyetDinh.DaoTaoTapTrung ? " (Tập trung)" : " (Không tập trung)";
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.TruongDaoTaoQD = quyetDinh.TruongDaoTao != null ? quyetDinh.TruongDaoTao.TenTruongDaoTao : "";
                qd.TrinhDoChuyenMonQD = quyetDinh.TrinhDoChuyenMon != null ? quyetDinh.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                //qd.ChuyenMonDaoTaoQD = quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : "";
                //qd.NganhDaoTao = String.Format("{0} {1}", GetTrinhDo(quyetDinh.TrinhDoChuyenMon), (quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : ""));
                //qd.KhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.TenKhoaDaoTao : "";
                //qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                //qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.ThoiGianDaoTao = quyetDinh.ThoiGianDaoTao;

                qd.TuThang = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("MM/yyyy") : "";
                qd.DenThang = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("MM/yyyy") : "";

                foreach (ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc item in quyetDinh.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc)
                {
                    //Non_QuyetDinhCaNhan
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.ChuyenMonDaoTaoQD = item.ChuyenMonDaoTao != null ? item.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.SoHieuCongChuc = item.ThongTinNhanVien.SoHieuCongChuc;
                    qd.SoHoSo = item.ThongTinNhanVien.SoHoSo;
                    qd.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                    break;
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
                
            }

            MailMergeTemplate merge;
            if (listTrongNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuongCheDoDiHocTrongNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuongCheDoDiHocCaNhan>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng chế độ đi học trong nước trong hệ thống.");

            }
            if (listNgoaiNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuongCheDoDiHocNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuongCheDoDiHocCaNhan>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng chế độ đi học ngoài nước trong hệ thống.");
            }
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhHuongCheDoDiHocTrongNuoc> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhHuongCheDoDiHoc>();
            var listNgoaiNuoc = new List<Non_QuyetDinhHuongCheDoDiHoc>();
            Non_QuyetDinhHuongCheDoDiHoc qd;
            foreach (QuyetDinhHuongCheDoDiHocTrongNuoc quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuongCheDoDiHoc();
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

                qd.HinhThucDaoTao = quyetDinh.HinhThucDaoTao != null ? quyetDinh.HinhThucDaoTao.TenHinhThucDaoTao : "";
                //qd.HinhThucDaoTao += quyetDinh.DaoTaoTapTrung ? " (Tập trung)" : " (Không tập trung)";
                qd.TruongDaoTao = quyetDinh.TruongDaoTao != null ? quyetDinh.TruongDaoTao.TenTruongDaoTao : "";
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                //qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0}, năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
                qd.TrinhDoChuyenMon = quyetDinh.TrinhDoChuyenMon != null ? quyetDinh.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                //qd.NganhDaoTao = String.Format("{0} {1}", GetTrinhDo(quyetDinh.TrinhDoChuyenMon), (quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : ""));
                //qd.KhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.TenKhoaDaoTao : "";
                //qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0} năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
                //qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                //qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.ThoiGianDaoTao = quyetDinh.ThoiGianDaoTao;
                
                //master
                Non_ChiTietQuyetDinhHuongCheDoDiHocMaster master = new Non_ChiTietQuyetDinhHuongCheDoDiHocMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.SoNguoi = quyetDinh.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Count.ToString();
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhHuongCheDoDiHocDetail detail;
                quyetDinh.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc item in quyetDinh.ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc)
                {
                    detail = new Non_ChiTietQuyetDinhHuongCheDoDiHocDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ChuyenNganhDaoTao = item.ChuyenMonDaoTao != null ? item.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";

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
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhHuongCheDoDiHocTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhHuongCheDoDiHocTapTheDetail.rtf")); ;
            if (listNgoaiNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhHuongCheDoDiHocTrongNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuongCheDoDiHoc>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng chế độ đi học trong hệ thống.");
            }
            if (listTrongNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhHuongCheDoDiHocNgoaiNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhHuongCheDoDiHoc>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng chế độ đi học trong hệ thống.");
            }
        }

        private string GetTrinhDo(TrinhDoChuyenMon trinhDo)
        {
            if (trinhDo != null)
            {
                if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("tiến"))
                    return "Nghiên cứu sinh";
                else if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("thạc"))
                    return "Cao học";
                else
                    return "Cử nhân";
            }
            return "";
        }
    }
}
