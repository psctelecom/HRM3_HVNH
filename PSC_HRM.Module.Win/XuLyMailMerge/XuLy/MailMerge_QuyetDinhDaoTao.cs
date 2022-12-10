using DevExpress.Data.Filtering;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhDaoTao : IMailMerge<IList<QuyetDinhDaoTao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhDaoTao> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietDaoTao.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietDaoTao.Count >= 2
                         select qd;

            //var tapTheHaiNguoi = from qd in qdList
            //                     where qd.ListChiTietDaoTao.Count == 2
            //                     select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
            //if (tapTheHaiNguoi.Count() > 0)
            //{
            //    QuyetDinhTapTheHaiNguoi(obs, tapTheHaiNguoi.ToList());
            //}
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDaoTao> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhDaoTaoCaNhan>();
            var listNgoaiNuoc = new List<Non_QuyetDinhDaoTaoCaNhan>();
            Non_QuyetDinhDaoTaoCaNhan qd;
            foreach (QuyetDinhDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDaoTaoCaNhan();
                //Non_QuyetDinh
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NamQuyetDinh = quyetDinh.NgayQuyetDinh.Year.ToString("####");
                qd.MaNganhDaoTao = quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.MaQuanLy : "";
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                }
                qd.NoiDung = quyetDinh.NoiDung;
                qd.CanCu = quyetDinh.CanCu;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.HoTenTCCB = quyetDinh.NguoiKyHD != null ? quyetDinh.NguoiKyHD.HoTen : "";
                qd.SDTTCCB = quyetDinh.NguoiKyHD != null ? quyetDinh.NguoiKyHD.DienThoaiDiDong : "";
                qd.DiachiTCCB = quyetDinh.NguoiKyHD != null ? quyetDinh.NguoiKyHD.DiaChiThuongTru.FullDiaChi : "";
                qd.HocHamTCCB = quyetDinh.NguoiKyHD != null ? quyetDinh.NguoiKyHD.NhanVienTrinhDo.HocHam.MaQuanLy : "";
                qd.HocViTCCB = quyetDinh.NguoiKyHD != null ? quyetDinh.NguoiKyHD.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy : "";
                //Non_QuyetDinhDaoTaoCaNhan
                
                qd.HinhThucDaoTao = quyetDinh.HinhThucDaoTao != null ? quyetDinh.HinhThucDaoTao.TenHinhThucDaoTao : "";
                qd.NamQuyetDinh = quyetDinh.DuyetDangKyDaoTao != null ? quyetDinh.DuyetDangKyDaoTao.QuanLyDaoTao.NamHoc.TenNamHoc.ToString(): "";
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.TruongDaoTaoQD = quyetDinh.TruongDaoTao != null ? quyetDinh.TruongDaoTao.TenTruongDaoTao : "";
                qd.TrinhDoChuyenMonQD = quyetDinh.TrinhDoChuyenMon != null ? quyetDinh.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                qd.ChuyenMonDaoTaoQD = quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : "";
                qd.NganhDaoTao = String.Format("{0} {1}", GetTrinhDo(quyetDinh.TrinhDoChuyenMon), (quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : ""));
                qd.KhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.TenKhoaDaoTao : "";
                qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0}, năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.ThoiGianDaoTao = quyetDinh.TuNgay.TinhSoNam(quyetDinh.DenNgay).ToString();
                qd.TuThang = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("MM/yyyy") : "";
                qd.DenThang = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("MM/yyyy") : "";
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.GhiChu = quyetDinh.GhiChu;
                qd.SoTien = quyetDinh.SoTien.ToString();
                qd.SoCongVan = quyetDinh.SoCongVan;
                if (string.IsNullOrEmpty(quyetDinh.SoTienBangChu))
                {
                    quyetDinh.SoTienBangChu = HamDungChung.DocTien(Math.Round(quyetDinh.SoTien, 0));
                }
                qd.SoTienBangChu = quyetDinh.SoTienBangChu;
                qd.NgayKhaiGiang = quyetDinh.NgayKhaiGiang.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU"))
                {
                    if (quyetDinh.TuNgay != DateTime.MinValue && quyetDinh.DenNgay != DateTime.MinValue)
                        qd.ThoiGianDaoTao = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
                    if (quyetDinh.TruongDaoTao != null && quyetDinh.TruongDaoTao.MaQuanLy == TruongConfig.MaTruong)
                        qd.TruongDaoTaoQD = "Trường";
                    qd.HinhThucDaoTao += quyetDinh.DaoTaoTapTrung ? " tập trung" : " không tập trung";
                    qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.Ten : "";
                }

                foreach (ChiTietDaoTao item in quyetDinh.ListChiTietDaoTao)
                {
                    //Non_QuyetDinhCaNhan
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.ChucDanhVietThuong = HamDungChung.GetChucDanhVietThuong(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.ChuyenMonDaoTao = item.ChuyenMonDaoTao != null ? item.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    if (TruongConfig.MaTruong.Equals("QNU"))
                    { qd.ChuyenMonDaoTaoQD = item.ChuyenMonDaoTao != null ? item.ChuyenMonDaoTao.TenChuyenMonDaoTao : ""; }
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.ChucVuNhanVienVietThuong = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien).ToLower();
                    qd.ChucVuNhanVienVietHoa = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien).ToUpper();
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    qd.SoHieuCongChuc = item.ThongTinNhanVien.SoHieuCongChuc;
                    qd.SoHoSo = item.ThongTinNhanVien.SoHoSo;
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.BoPhan);
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
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhDaoTaoTrongNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDaoTaoCaNhan>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");

            }
            if (listNgoaiNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhDaoTaoNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDaoTaoCaNhan>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");
            }
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDaoTao> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhDaoTao>();
            var listNgoaiNuoc = new List<Non_QuyetDinhDaoTao>();
            Non_QuyetDinhDaoTao qd;
            foreach (QuyetDinhDaoTao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDaoTao();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.SoCongVan = quyetDinh.SoCongVan != null ? quyetDinh.SoCongVan : "";
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                qd.NamQuyetDinh = quyetDinh.NgayQuyetDinh.Year.ToString("####");
             
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                }
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                qd.HinhThucDaoTao = quyetDinh.HinhThucDaoTao != null ? quyetDinh.HinhThucDaoTao.TenHinhThucDaoTao : "";
                qd.NamQuyetDinh = quyetDinh.DuyetDangKyDaoTao != null ? quyetDinh.DuyetDangKyDaoTao.QuanLyDaoTao.NamHoc.TenNamHoc.ToString() : "";
                if (!TruongConfig.MaTruong.Equals("NEU"))
                    qd.HinhThucDaoTao += quyetDinh.DaoTaoTapTrung ? " (Tập trung)" : " (Không tập trung)";
                qd.TruongDaoTao = quyetDinh.TruongDaoTao != null ? quyetDinh.TruongDaoTao.TenTruongDaoTao : "";
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0}, năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
                qd.TrinhDoChuyenMon = quyetDinh.TrinhDoChuyenMon != null ? quyetDinh.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                qd.NganhDaoTao = String.Format("{0} {1}", GetTrinhDo(quyetDinh.TrinhDoChuyenMon), (quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : ""));
                qd.KhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.TenKhoaDaoTao : "";
                qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0} năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.TuNgayDate = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : "";
                qd.DenNgayDate = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("dd/MM/yyyy") : "";
                qd.TuThang = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.Month.ToString("##") : "";
                qd.DenThang = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.Month.ToString("##") : "";
                qd.ThoiGianDaoTao = quyetDinh.ThoiGianDaoTao;
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.GhiChu = quyetDinh.GhiChu;
                qd.SoTien = quyetDinh.SoTien.ToString();
                if (string.IsNullOrEmpty(quyetDinh.SoTienBangChu))
                {
                    quyetDinh.SoTienBangChu = HamDungChung.DocTien(Math.Round(quyetDinh.SoTien, 0));
                }
                qd.SoTienBangChu = quyetDinh.SoTienBangChu;
                qd.NgayKhaiGiang = quyetDinh.NgayKhaiGiang.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU"))
                {
                    if (quyetDinh.TuNgay != DateTime.MinValue && quyetDinh.DenNgay != DateTime.MinValue)
                        qd.ThoiGianDaoTao = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
                    //if (quyetDinh.TruongDaoTao != null && quyetDinh.TruongDaoTao.MaQuanLy == TruongConfig.MaTruong)
                    //    qd.TruongDaoTaoQD = "Trường";
                    qd.HinhThucDaoTao += quyetDinh.DaoTaoTapTrung ? " tập trung" : " không tập trung";
                    qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.Ten : "";
                }
                if (quyetDinh.NoiDung.Contains("công chức"))
                    qd.CuDoan = "công chức, viên chức";
                else if (quyetDinh.NoiDung.Contains("người lao động"))
                    qd.CuDoan = "viên chức, người lao động";
                else
                    qd.CuDoan = "viên chức";

                //master
                Non_ChiTietQuyetDinhDaoTaoMaster master = new Non_ChiTietQuyetDinhDaoTaoMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TieuDe = qd.NoiDung.ToUpper();
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NgayHieuLuc = qd.NgayHieuLuc;
                master.SoNguoi = quyetDinh.ListChiTietDaoTao.Count.ToString();
                master.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu : "";
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhDaoTaoDetail detail;
                quyetDinh.ListChiTietDaoTao.Sorting.Clear();
                quyetDinh.ListChiTietDaoTao.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietDaoTao item in quyetDinh.ListChiTietDaoTao)
                {
                    detail = new Non_ChiTietQuyetDinhDaoTaoDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = string.Concat(stt.ToString(),".");
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.ChucVu = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.MaDonVi = item.ThongTinNhanVien.BoPhan != null ? item.ThongTinNhanVien.BoPhan.MaQuanLy : "";
                    detail.TenVietTatDonVi = item.ThongTinNhanVien.BoPhan != null ? item.ThongTinNhanVien.BoPhan.TenBoPhanVietTat : "";
                    detail.ChuyenNganhDaoTao = item.ChuyenMonDaoTao != null ? item.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    detail.NhiemVu = "Học viên";

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
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDaoTaoTapTheMaster.rtf")); 
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDaoTaoTapTheDetail.rtf")); 
            if (listNgoaiNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDaoTaoNgoaiNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDaoTao>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo (nước ngoài nhiều người) trong hệ thống.");
            }
            if (listTrongNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDaoTaoTrongNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDaoTao>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo (trong nước nhiều người) trong hệ thống.");    
            }
            //if (listTrongNuoc.Count > 0 &&TruongConfig.MaTruong.Equals("NEU"))
            //{
            //    MailMergeTemplate[] merge1 = new MailMergeTemplate[3];
            //    merge1[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDaoTaoTapTheMaster.rtf"));
            //    merge1[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDaoTaoTapTheDetail.rtf"));
            //    merge1[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDaoTaoTrongNuocTapThe1.rtf");

            //    if (merge1[0] != null)
            //        MailMergeHelper.ShowEditor<Non_QuyetDinhDaoTao>(listTrongNuoc, obs, merge1);
            //    else
            //        HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo (trong nước nhiều người) trong hệ thống.");
            //}
        }

        //private void QuyetDinhTapTheHaiNguoi(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDaoTao> qdList)
        //{
        //    int stt = 1;
        //    var list = new List<Non_QuyetDinhDaoTao>();
        //    Non_QuyetDinhDaoTao qd;
        //    foreach (QuyetDinhDaoTao quyetDinh in qdList)
        //    {
        //        qd = new Non_QuyetDinhDaoTao();
        //        qd.Oid = quyetDinh.Oid.ToString();
        //        qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
        //        qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
        //        qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
        //        qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
        //        qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
        //        qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
        //        qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
        //        qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
        //        if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc != DateTime.MinValue)
        //        {
        //            qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
        //            qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
        //        }
        //        else
        //        {
        //            qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
        //            qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
        //        }
        //        qd.CanCu = quyetDinh.CanCu;
        //        qd.NoiDung = quyetDinh.NoiDung;
        //        qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
        //        qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
        //        qd.NguoiKy = quyetDinh.NguoiKy1;
        //        qd.HinhThucDaoTao = quyetDinh.HinhThucDaoTao != null ? quyetDinh.HinhThucDaoTao.TenHinhThucDaoTao : "";
        //        qd.NamQuyetDinh = quyetDinh.DuyetDangKyDaoTao != null ? quyetDinh.DuyetDangKyDaoTao.QuanLyDaoTao.NamHoc.TenNamHoc.ToString() : "";
        //        qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
        //        qd.TruongDaoTao = quyetDinh.TruongDaoTao != null ? quyetDinh.TruongDaoTao.TenTruongDaoTao : "";
        //        qd.TrinhDoChuyenMon = quyetDinh.TrinhDoChuyenMon != null ? quyetDinh.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
        //        qd.NganhDaoTao = quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : "";
        //        //qd.NganhDaoTao = String.Format("{0} {1}", GetTrinhDo(quyetDinh.TrinhDoChuyenMon), (quyetDinh.NganhDaoTao != null ? quyetDinh.NganhDaoTao.TenNganhDaoTao : ""));
        //        qd.KhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.TenKhoaDaoTao : "";
        //        qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? String.Format("{0}, năm {1:####}", quyetDinh.KhoaDaoTao.Ten, quyetDinh.KhoaDaoTao.TuNam) : "";
        //        qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
        //        qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
        //        qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
        //        qd.DenNgay = quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
        //        qd.TuNgayDate = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
        //        qd.DenNgayDate = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
        //        qd.TuThang = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("MM/yyyy") : "";
        //        qd.DenThang = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("MM/yyyy") : "";
        //        qd.GhiChu = quyetDinh.GhiChu;
        //        if (TruongConfig.MaTruong.Equals("NEU"))
        //        {
        //            if (quyetDinh.TuNgay != DateTime.MinValue && quyetDinh.DenNgay != DateTime.MinValue)
        //                qd.ThoiGianDaoTao = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
        //            //if (quyetDinh.TruongDaoTao != null && quyetDinh.TruongDaoTao.MaQuanLy == TruongConfig.MaTruong)
        //            //    qd.TruongDaoTaoQD = "Trường";
        //            //qd.HinhThucDaoTao += quyetDinh.DaoTaoTapTrung ? " tập trung" : " không tập trung";
        //            qd.TenKhoaDaoTao = quyetDinh.KhoaDaoTao != null ? quyetDinh.KhoaDaoTao.Ten : "";
        //        }
        //        if (quyetDinh.NoiDung.Contains("công chức"))
        //            qd.CuDoan = "công chức, viên chức";
        //        else if (quyetDinh.NoiDung.Contains("người lao động"))
        //            qd.CuDoan = "viên chức, người lao động";
        //        else
        //            qd.CuDoan = "viên chức";

        //        //master
        //        Non_ChiTietQuyetDinhDaoTaoMaster master = new Non_ChiTietQuyetDinhDaoTaoMaster();
        //        master.Oid = quyetDinh.Oid.ToString();
        //        master.DonViChuQuan = qd.DonViChuQuan;
        //        master.TenTruongVietHoa = qd.TenTruongVietHoa;
        //        master.TenTruongVietThuong = qd.TenTruongVietThuong;
        //        master.SoQuyetDinh = qd.SoQuyetDinh;
        //        master.NguoiKy = qd.NguoiKy;
        //        master.NgayQuyetDinh = qd.NgayQuyetDinh;
        //        master.NgayHieuLuc = qd.NgayHieuLuc;
        //        //master.CuDoanVietHoa = qd.CuDoan.ToUpper();
        //        qd.Master.Add(master);

        //        //detail
        //        Non_ChiTietQuyetDinhDaoTaoDetail detail;
        //        quyetDinh.ListChiTietDaoTao.Sorting.Clear();
        //        quyetDinh.ListChiTietDaoTao.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.HoTen", DevExpress.Xpo.DB.SortingDirection.Ascending));

        //        foreach (ChiTietDaoTao item in quyetDinh.ListChiTietDaoTao)
        //        {
        //            detail = new Non_ChiTietQuyetDinhDaoTaoDetail();
        //            detail.Oid = quyetDinh.Oid.ToString();
        //            detail.STT = string.Concat(stt.ToString(), ".");
        //            detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
        //            detail.ChucVu = string.Concat("-", HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien));
        //            detail.HoTen = item.ThongTinNhanVien.HoTen;
        //            detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
        //            if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
        //                detail.DanhXungVietThuong = "ông";
        //            else
        //                detail.DanhXungVietThuong = "bà";
        //            if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
        //                detail.DanhXungVietHoa = "Ông";
        //            else
        //                detail.DanhXungVietHoa = "Bà";

        //            qd.Detail.Add(detail);
        //            stt++;

        //        }

        //        master.SoNguoi = (stt - 1).ToString();
        //        list.Add(qd);
        //    }

        //    MailMergeTemplate[] merge = new MailMergeTemplate[3];
        //    merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDaoTaoHaiNguoiMaster.rtf"));
        //    merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDaoTaoHaiNguoiDetail.rtf"));
        //    merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDaoTaoHaiNguoiTrongNuoc.rtf");
        //    if (merge[0] != null)
        //        MailMergeHelper.ShowEditor<Non_QuyetDinhDaoTao>(list, obs, merge);
        //    else
        //        HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi đào tạo (hai người trong nước) trong hệ thống.");
        //}

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
