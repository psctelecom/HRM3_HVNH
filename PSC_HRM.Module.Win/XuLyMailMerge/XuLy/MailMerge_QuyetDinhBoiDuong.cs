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
    public class MailMerge_QuyetDinhBoiDuong : IMailMerge<IList<QuyetDinhBoiDuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhBoiDuong> quyetDinh)
        {
            var caNhan = from qd in quyetDinh
                         where qd.ListChiTietBoiDuong.Count == 1
                         select qd;

            var tapThe = from qd in quyetDinh
                         where qd.ListChiTietBoiDuong.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhBoiDuong> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhBoiDuongCaNhan>();
            var listThayTheTrongNuoc = new List<Non_QuyetDinhBoiDuongCaNhan>();
            var listNgoaiNuoc = new List<Non_QuyetDinhBoiDuongCaNhan>();
            var listThayTheNgoaiNuoc = new List<Non_QuyetDinhBoiDuongCaNhan>();
            Non_QuyetDinhBoiDuongCaNhan qd;
            foreach (QuyetDinhBoiDuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoiDuongCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
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

                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("d") : "";
                qd.ThoiGian = quyetDinh.ThoiGian;
                qd.ChungChi = quyetDinh.ChungChi != null ? quyetDinh.ChungChi.TenChungChi : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = quyetDinh.TruongHoTro != null ? quyetDinh.TruongHoTro : "";
                qd.SoCongVan = quyetDinh.SoCongVan; 
                qd.ChuongTrinhBoiDuong = "";
                qd.ChuyenNganhDaoTao = quyetDinh.ChuyenNganhDaoTao != null ? quyetDinh.ChuyenNganhDaoTao.TenTrinhDoChuyenMon : "";
                qd.MaNganhDaoTao = quyetDinh.ChuyenNganhDaoTao != null ? quyetDinh.ChuyenNganhDaoTao.MaQuanLy : "";
                qd.TruongDaoTao = quyetDinh.TruongDaoTao;
                qd.SoTien = quyetDinh.SoTien.ToString();
                if (string.IsNullOrEmpty(quyetDinh.SoTienBangChu))
                {
                    quyetDinh.SoTienBangChu = HamDungChung.DocTien(Math.Round(quyetDinh.SoTien, 0));
                }
                qd.SoTienBangChu = quyetDinh.SoTienBangChu;
                qd.NgayKhaiGiang = quyetDinh.NgayKhaiGiang.ToString("dd/MM/yyyy");
                if (quyetDinh.ChuongTrinhBoiDuong != null)
                {
                    qd.DonViToChucTheoChuongTrinh = quyetDinh.ChuongTrinhBoiDuong.DonViToChuc;
                    qd.DiaDiemTheoChuongTrinh = quyetDinh.ChuongTrinhBoiDuong.DiaDiem;
                    qd.ChuongTrinhBoiDuong = quyetDinh.ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong;
                }

                qd.DonViToChuc = quyetDinh.DonViToChuc;
                qd.DiaDiem = quyetDinh.NoiBoiDuong;
                qd.NoiBoiDuong = quyetDinh.NoiBoiDuong;
                qd.LoaiHinhBoiDuong = quyetDinh.LoaiHinhBoiDuong != null ? quyetDinh.LoaiHinhBoiDuong.TenLoaiHinhBoiDuong : "";
                qd.NoiDungBoiDuong = quyetDinh.NoiDungBoiDuong;
                foreach (ChiTietBoiDuong item in quyetDinh.ListChiTietBoiDuong)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.BoPhan);
                    qd.NgayVaoCoQuan = item.ThongTinNhanVien.NgayVaoCoQuan != null ? item.ThongTinNhanVien.NgayVaoCoQuan.ToString("dd/MM/yyyy") : "";
                    qd.NgayBoNhiemNgach = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach != DateTime.MinValue ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach.ToString("dd/MM/yyyy") : "";
                    qd.MaDonVi = item.BoPhan.MaQuanLy;
                 
                    if (item.NhanVienThayThe != null)
                    {
                        qd.NhanVienThayThe = item.NhanVienThayThe.HoTen;
                        qd.ChucDanhNhanVienThayThe = HamDungChung.GetChucDanh(item.NhanVienThayThe);
                        qd.ChucVuNhanVienThayThe = item.NhanVienThayThe.ChucVu != null ? item.NhanVienThayThe.ChucVu.TenChucVu : "";
                        qd.MaDonViNhanVienThayThe = item.BoPhanThayThe != null ? item.BoPhanThayThe.TenBoPhan : "";                        
                    }
                    break;
                }

                if (quyetDinh.QuocGia != null &&
                    HamDungChung.CauHinhChung.QuocGia != null &&
                    quyetDinh.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    if (quyetDinh.QuyetDinhBoiDuongThayThe != null)
                        listThayTheTrongNuoc.Add(qd);
                    else
                        listNgoaiNuoc.Add(qd);                        
                }
                else
                {
                    if (quyetDinh.QuyetDinhBoiDuongThayThe != null)
                        listThayTheNgoaiNuoc.Add(qd);                         
                    else
                        listTrongNuoc.Add(qd);                   
                }
            }

            MailMergeTemplate merge;
            if (listTrongNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongTrongNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuongCaNhan>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
            if (listThayTheTrongNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongThayTheTrongNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuongCaNhan>(listThayTheTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
            if (listNgoaiNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuongCaNhan>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
            if (listThayTheNgoaiNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongThayTheNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuongCaNhan>(listThayTheNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhBoiDuong> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhBoiDuong>();
            var listThayTheTrongNuoc = new List<Non_QuyetDinhBoiDuong>();
            var listNgoaiNuoc = new List<Non_QuyetDinhBoiDuong>();
            var listThayTheNgoaiNuoc = new List<Non_QuyetDinhBoiDuong>();
            Non_QuyetDinhBoiDuong qd;
            foreach (QuyetDinhBoiDuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhBoiDuong();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
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

                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("d") : "";
                qd.ThoiGian = quyetDinh.ThoiGian;
                qd.ChungChi = quyetDinh.ChungChi != null ? quyetDinh.ChungChi.TenChungChi : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = quyetDinh.TruongHoTro != null ? quyetDinh.TruongHoTro : "";
                qd.TruongDaoTao = quyetDinh.TruongDaoTao;
                qd.SoCongVan = quyetDinh.SoCongVan;
                qd.SoTien = quyetDinh.SoTien.ToString();
                if (string.IsNullOrEmpty(quyetDinh.SoTienBangChu))
                {
                    quyetDinh.SoTienBangChu = HamDungChung.DocTien(Math.Round(quyetDinh.SoTien, 0));
                }
                qd.SoTienBangChu = quyetDinh.SoTienBangChu;
                qd.NgayKhaiGiang = quyetDinh.NgayKhaiGiang.ToString("dd/MM/yyyy");
                if (quyetDinh.ChuongTrinhBoiDuong != null)
                {
                    qd.DonViToChucTheoChuongTrinh = quyetDinh.ChuongTrinhBoiDuong.DonViToChuc;
                    qd.DiaDiemTheoChuongTrinh = quyetDinh.ChuongTrinhBoiDuong.DiaDiem;
                    qd.ChuongTrinhBoiDuong = quyetDinh.ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong;
                }
                qd.DonViToChuc = quyetDinh.DonViToChuc;
                qd.DiaDiem = quyetDinh.NoiBoiDuong;
                qd.NoiDungBoiDuong = quyetDinh.NoiDungBoiDuong;
                qd.NoiBoiDuong = quyetDinh.NoiBoiDuong;
                qd.LoaiHinhBoiDuong = quyetDinh.LoaiHinhBoiDuong != null ? quyetDinh.LoaiHinhBoiDuong.TenLoaiHinhBoiDuong : "";

                if (quyetDinh.QuyetDinhBoiDuongThayThe != null)
                {
                    qd.SoQuyetDinhDieuChinh = quyetDinh.QuyetDinhBoiDuongThayThe.SoQuyetDinh;
                    qd.NgayQuyetDinhDieuChinh = quyetDinh.QuyetDinhBoiDuongThayThe.NgayQuyetDinh != DateTime.MinValue ? quyetDinh.QuyetDinhBoiDuongThayThe.NgayQuyetDinh.ToString("d") : "";
                }

                if (quyetDinh.NoiDung.Contains("công chức"))
                    qd.CuDoan = "công chức, viên chức";
                else if (quyetDinh.NoiDung.Contains("người lao động"))
                    qd.CuDoan = "viên chức, người lao động";
                else
                    qd.CuDoan = "viên chức";

                //master
                Non_ChiTietQuyetDinhBoiDuongMaster master = new Non_ChiTietQuyetDinhBoiDuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NgayQuyetDinhDate = qd.NgayQuyetDinhDate;
                master.CuDoan = qd.CuDoan;
                if (TruongConfig.MaTruong == "NEU")
                    master.NoiDungBoiDuong = quyetDinh.NoiDung.ToUpper();
                else
                    master.NoiDungBoiDuong = quyetDinh.NoiDungBoiDuong.ToUpper();
                master.LoaiHinhBoiDuong = qd.LoaiHinhBoiDuong.ToUpper();
                master.NoiDungVietHoa = qd.NoiDung.ToUpper();

                //detail
                Non_ChiTietQuyetDinhBoiDuongDetail detail;
                quyetDinh.ListChiTietBoiDuong.Sorting.Clear();
                quyetDinh.ListChiTietBoiDuong.Sorting.Add(new DevExpress.Xpo.SortProperty("NhiemVu", DevExpress.Xpo.DB.SortingDirection.Descending));
                int stt = 1;
                foreach (ChiTietBoiDuong item in quyetDinh.ListChiTietBoiDuong)
                {
                    detail = new Non_ChiTietQuyetDinhBoiDuongDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    if (TruongConfig.MaTruong == "NEU")
                        detail.ChucVu = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien);
                    else
                        detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : string.Empty;
                    detail.MaChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.MaQuanLy : string.Empty;
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy") : string.Empty;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.NhiemVu = item.NhiemVu;
                    detail.GhiChu = item.GhiChu;
                    if (item.ThongTinNhanVien.ChucVu != null)
                        detail.ChucVu_BoPhan = item.ThongTinNhanVien.ChucVu.TenChucVu + " - " + item.ThongTinNhanVien.BoPhan.MaQuanLy;
                    if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null)
                        detail.MaNgach = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy;
                    qd.Detail.Add(detail);
                    stt++;
                }

                //Lấy tổng số nhân viên
                master.TongNhanVien = stt-1;
                if (master.TongNhanVien < 10)
                    qd.SoLuongCanBo = String.Concat("0", master.TongNhanVien.ToString());
                else
                qd.SoLuongCanBo = master.TongNhanVien.ToString();
                //Đưa chi tiết vào master
                qd.Master.Add(master);
                //
                if (quyetDinh.QuocGia != null &&
                    HamDungChung.CauHinhChung.QuocGia != null &&
                    quyetDinh.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    if (quyetDinh.QuyetDinhBoiDuongThayThe != null)
                        listThayTheNgoaiNuoc.Add(qd);
                    else
                        listNgoaiNuoc.Add(qd);
                }
                else
                {
                    if (quyetDinh.QuyetDinhBoiDuongThayThe != null)
                        listThayTheTrongNuoc.Add(qd);
                    else                            
                        listTrongNuoc.Add(qd);
                }
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhBoiDuongTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhBoiDuongTapTheDetail.rtf")); ;
            if (listNgoaiNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongNgoaiNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuong>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
            if (listTrongNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongTrongNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuong>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
            if (listThayTheTrongNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongThayTheTrongNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuong>(listThayTheTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
            if (listThayTheNgoaiNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhBoiDuongThayTheNgoaiNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhBoiDuong>(listThayTheNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ bồi dưỡng trong hệ thống.");
            }
        }
    }
}
