using DevExpress.Data.Filtering;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhDiCongTac : IMailMerge<IList<QuyetDinhDiCongTac>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhDiCongTac> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhDiCongTac.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhDiCongTac.Count > 1
                         select qd;

            var tapTheHaiNguoi = from qd in qdList
                         where qd.ListChiTietQuyetDinhDiCongTac.Count == 2
                         select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                if (TruongConfig.MaTruong.Equals("NEU") && tapTheHaiNguoi.Count() == 1)
                    QuyetDinhTapTheOrCaNhan(obs, tapTheHaiNguoi.ToList());
                else
                    QuyetDinhTapThe(obs, tapThe.ToList());                
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDiCongTac> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhDiCongTacCaNhan>();
            var listNgoaiNuoc = new List<Non_QuyetDinhDiCongTacCaNhan>();
            Non_QuyetDinhDiCongTacCaNhan qd;
            foreach (QuyetDinhDiCongTac quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDiCongTacCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
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
                qd.NgayKy = DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.DiaDiem = quyetDinh.DiaDiem ?? "";
                qd.SoCongVan = quyetDinh.SoCongVan ?? "";
                qd.DonViToChuc = quyetDinh.DonViToChuc ?? "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.TuNgayDate = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgayDate = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.GhiChuTG = quyetDinh.GhiChuTG;
                if (quyetDinh.TuNgay != DateTime.MinValue && quyetDinh.DenNgay != DateTime.MinValue)
                    qd.ThoiGian = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
                qd.LyDo = quyetDinh.LyDo ?? "";               
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayXinDiDate = quyetDinh.NgayXinDi.ToString("dd/MM/yyyy");
                foreach (ChiTietQuyetDinhDiCongTac item in quyetDinh.ListChiTietQuyetDinhDiCongTac)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.ChucDanhVietThuong = HamDungChung.GetChucDanhVietThuong(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaDonVi = item.BoPhan != null ? item.BoPhan.MaQuanLy : "";
                    qd.ViTri = item.ViTriCongTac != null ? item.ViTriCongTac.TenViTriCongTac : "";
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.BoPhan);
                    qd.ChucVuNhanVienVietThuong = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien).ToLower();
                    qd.ChucVuNhanVienVietHoa = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien).ToUpper();
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.ChucVuNhanVienVietHoa = HamDungChung.GetChucVuDonVi(item.ThongTinNhanVien);
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                }
                if (quyetDinh.QuocGia != null &&
                    HamDungChung.CauHinhChung.QuocGia != null &&
                    quyetDinh.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    listNgoaiNuoc.Add(qd);
                }
                else
                {
                    listTrongNuoc.Add(qd);
                }
            }

            MailMergeTemplate merge;           
            if (listTrongNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhDiCongTacTrongNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDiCongTacCaNhan>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công tác trong hệ thống.");
            }

            if (listNgoaiNuoc.Count > 0)
            {
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhDiCongTacNgoaiNuoc.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDiCongTacCaNhan>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ công tác trong hệ thống.");
            }
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDiCongTac> qdList)
        {
            var listTrongNuoc = new List<Non_QuyetDinhDiCongTac>();
            var listNgoaiNuoc = new List<Non_QuyetDinhDiCongTac>();
            Non_QuyetDinhDiCongTac qd;
            foreach (QuyetDinhDiCongTac quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDiCongTac();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc != DateTime.MinValue)
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
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                qd.DiaDiem = quyetDinh.DiaDiem ?? "";
                qd.SoCongVan = quyetDinh.SoCongVan ?? "";
                qd.DonViToChuc = quyetDinh.DonViToChuc ?? "";
                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.TuNgayDate = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgayDate = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                if (quyetDinh.TuNgay != DateTime.MinValue && quyetDinh.DenNgay != DateTime.MinValue)
                    qd.ThoiGian = HamDungChung.GetThoiGian(quyetDinh.TuNgay, quyetDinh.DenNgay);
                qd.LyDo = quyetDinh.LyDo ?? "";
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayXinDiDate = quyetDinh.NgayXinDi.ToString("dd/MM/yyyy");
                qd.GhiChuTG = quyetDinh.GhiChuTG;
                if (quyetDinh.NoiDung.Contains("công chức"))
                    qd.CuDoan = "công chức, viên chức";
                else if (quyetDinh.NoiDung.Contains("người lao động"))
                    qd.CuDoan = "viên chức, người lao động";
                else
                    qd.CuDoan = "viên chức";
                
                //master
                Non_ChiTietQuyetDinhDiCongTacMaster master = new Non_ChiTietQuyetDinhDiCongTacMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NgayHieuLuc = qd.NgayHieuLuc;
                master.CuDoanVietHoa = qd.CuDoan.ToUpper();
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhDiCongTacDetail detail;
                quyetDinh.ListChiTietQuyetDinhDiCongTac.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhDiCongTac.Sorting.Add(new DevExpress.Xpo.SortProperty("ViTriCongTac.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhDiCongTac item in quyetDinh.ListChiTietQuyetDinhDiCongTac)
                {
                    detail = new Non_ChiTietQuyetDinhDiCongTacDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ViTri = item.ViTriCongTac!= null ? item.ViTriCongTac.TenViTriCongTac : string.Empty;
                    if (TruongConfig.MaTruong.Equals("NEU"))
                        detail.ChucVu = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien);
                    else
                        detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : string.Empty;
                    qd.Detail.Add(detail);
                    stt++;
                }
                master.TongNhanVien = stt - 1;

                if (quyetDinh.QuocGia != null &&
                   HamDungChung.CauHinhChung.QuocGia != null &&
                   quyetDinh.QuocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                {
                    listNgoaiNuoc.Add(qd);
                }
                else
                {
                    listTrongNuoc.Add(qd);
                }
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDiCongTacTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDiCongTacTapTheDetail.rtf")); ;
            if (listNgoaiNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDiCongTacNgoaiNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDiCongTac>(listNgoaiNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");
            }
            if (listTrongNuoc.Count > 0)
            {
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDiCongTacTrongNuocTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhDiCongTac>(listTrongNuoc, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đào tạo trong hệ thống.");
            }

            //MailMergeTemplate[] merge = new MailMergeTemplate[3];
            //merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongTacTapTheMaster.rtf")); ;
            //merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongTacTapTheDetail.rtf")); ;
            //merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhCongTacTapThe.rtf");
            //if (merge[0] != null)
            //    MailMergeHelper.ShowEditor<Non_QuyetDinhDiCongTac>(list, obs, merge);
            //else
            //    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi công tác trong hệ thống.");
        }

        private void QuyetDinhTapTheOrCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDiCongTac> qdList)
        {
            int stt = 1;
            var list = new List<Non_QuyetDinhDiCongTac>();
            Non_QuyetDinhDiCongTac qd;
            foreach (QuyetDinhDiCongTac quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDiCongTac();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc != DateTime.MinValue)
                {
                    qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                }
                qd.NgayKy = DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
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
                qd.TuNgayDate = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgayDate = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayXinDiDate = quyetDinh.NgayXinDi.ToString("dd/MM/yyyy");
                qd.LyDo = quyetDinh.LyDo ?? "";
                qd.GhiChuTG = quyetDinh.GhiChuTG;
                if (quyetDinh.NoiDung.Contains("công chức"))
                    qd.CuDoan = "công chức, viên chức";
                else if (quyetDinh.NoiDung.Contains("người lao động"))
                    qd.CuDoan = "viên chức, người lao động";
                else
                    qd.CuDoan = "viên chức";

                //master
                Non_ChiTietQuyetDinhDiCongTacMaster master = new Non_ChiTietQuyetDinhDiCongTacMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NgayHieuLuc = qd.NgayHieuLuc;
                master.CuDoanVietHoa = qd.CuDoan.ToUpper();
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhDiCongTacDetail detail;
                quyetDinh.ListChiTietQuyetDinhDiCongTac.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhDiCongTac.Sorting.Add(new DevExpress.Xpo.SortProperty("ViTriCongTac.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                       
                foreach (ChiTietQuyetDinhDiCongTac item in quyetDinh.ListChiTietQuyetDinhDiCongTac)
                {
                    detail = new Non_ChiTietQuyetDinhDiCongTacDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = string.Concat(stt.ToString(),".");
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.ChucVu = string.Concat("-",HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien));
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ViTri = string.Concat("-",item.ViTriCongTac != null ? item.ViTriCongTac.TenViTriCongTac : string.Empty);                   
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        detail.DanhXungVietThuong = "ông";
                    else
                        detail.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        detail.DanhXungVietHoa = "Ông";
                    else
                        detail.DanhXungVietHoa = "Bà";

                    qd.Detail.Add(detail);
                    stt++;
                   
                }

                master.TongNhanVien = stt - 1;               
                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongTacTapTheMaster.rtf"));
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhCongTacTapTheDetail.rtf"));
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhCongTacTapTheNgoaiNuoc.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhDiCongTac>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi công tác trong hệ thống.");
        }
    }
}
