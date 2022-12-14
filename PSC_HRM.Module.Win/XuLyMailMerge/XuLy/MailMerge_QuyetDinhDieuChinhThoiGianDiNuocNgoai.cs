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
    public class MailMerge_QuyetDinhDieuChinhThoiGianDiNuocNgoai : IMailMerge<IList<QuyetDinhDieuChinhThoiGianDiNuocNgoai>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhDieuChinhThoiGianDiNuocNgoai> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai.Count > 1
                         select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhDieuChinhThoiGianCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                QuyetDinhDieuChinhThoiGianTapThe(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhDieuChinhThoiGianCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDieuChinhThoiGianDiNuocNgoai> qdList)
        {
            var list = new List<Non_QuyetDinhDieuChinhThoiGianDiNuocNgoaiCaNhan>();
            Non_QuyetDinhDieuChinhThoiGianDiNuocNgoaiCaNhan qd;
            foreach (QuyetDinhDieuChinhThoiGianDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDieuChinhThoiGianDiNuocNgoaiCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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

                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.LyDo = quyetDinh.LyDo ?? "";
                qd.GhiChu = quyetDinh.GhiChu;
                qd.GhiChuTG = quyetDinh.GhiChuTG;
                qd.TuNgayDC = quyetDinh.TuNgayDC.ToString("dd/MM/yyyy");
                qd.DenNgayDC = quyetDinh.DenNgayDC.ToString("dd/MM/yyyy");
                //qd.SoQD = quyetDinh.QuyetDinhDiNuocNgoai.SoQuyetDinh;
                qd.NgayHieuLucCu = quyetDinh.QuyetDinhDiNuocNgoai.NgayHieuLuc.ToString("dd/MM/yyyy");
                qd.SoQuyetDinhCu = quyetDinh.QuyetDinhDiNuocNgoai.SoQuyetDinh;
                //qd.NgayHieuLucCu = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");

               


                qd.SoCongVan = quyetDinh.SoCongVan;
                qd.DiaDiem = quyetDinh.DiaDiem;
                qd.DonViToChuc = quyetDinh.DonViToChuc;
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");


                foreach (ChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai item in quyetDinh.ListChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.SoHieuCongChuc = item.ThongTinNhanVien.SoHieuCongChuc;
                    qd.SoHoSo = item.ThongTinNhanVien.SoHoSo;
                    qd.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ?
                        item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
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

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhDieuChinhThoiGianDiNuocNgoai.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhDieuChinhThoiGianDiNuocNgoaiCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ điều chỉnh thời gian đi nước ngoài trong hệ thống.");
        }

        private void QuyetDinhDieuChinhThoiGianTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDieuChinhThoiGianDiNuocNgoai> qdList)
        {
            int stt = 1;
            var list = new List<Non_QuyetDinhDieuChinhThoiGianDiNuocNgoai>();
            Non_QuyetDinhDieuChinhThoiGianDiNuocNgoai qd;
            foreach (QuyetDinhDieuChinhThoiGianDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDieuChinhThoiGianDiNuocNgoai();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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

                qd.QuocGia = quyetDinh.QuocGia != null ? quyetDinh.QuocGia.TenQuocGia : "";
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.TuNgayDC = quyetDinh.TuNgayDC.ToString("dd/MM/yyyy");
                qd.DenNgayDC = quyetDinh.DenNgayDC.ToString("dd/MM/yyyy");
                qd.LyDo = quyetDinh.LyDo ?? "";
                qd.GhiChu = quyetDinh.GhiChu;
                qd.SoCongVan = quyetDinh.SoCongVan;
                qd.DiaDiem = quyetDinh.DiaDiem;
                qd.DonViToChuc = quyetDinh.DonViToChuc;
                qd.SoQuyetDinhCu = quyetDinh.QuyetDinhDiNuocNgoai.SoQuyetDinh;
                qd.NgayHieuLucCu = quyetDinh.QuyetDinhDiNuocNgoai.NgayHieuLuc.ToString("dd/MM/yyyy");

                

                //master
                Non_ChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoaiMaster master = new Non_ChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoaiMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoaiDetail detail;
                quyetDinh.ListChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));

                foreach (ChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai item in quyetDinh.ListChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoai)
                {
                    detail = new Non_ChiTietQuyetDinhDieuChinhThoiGianDiNuocNgoaiDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ViTriCongTac = item.ViTriCongTac.TenViTriCongTac;
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        detail.DanhXungVietThuong = "ông";
                    else
                        detail.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        detail.DanhXungVietHoa = "Ông";
                    else
                        detail.DanhXungVietHoa = "Bà";
                    detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : string.Empty;

                    qd.Detail.Add(detail);
                    stt++;
                }
                master.TongNhanVien = stt - 1;
                if (master.TongNhanVien < 10)
                    qd.SoLuongCanBo = String.Concat("0", master.TongNhanVien.ToString());
                else
                    qd.SoLuongCanBo = master.TongNhanVien.ToString();

                list.Add(qd);
            }


            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDieuChinhThoiGianDiNuocNgoaiTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDieuChinhThoiGianDiNuocNgoaiTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDieuChinhThoiGianDiNuocNgoaiTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhDieuChinhThoiGianDiNuocNgoai>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ điều chỉnh thời gian đi nước ngoài trong hệ thống.");

            //else
            //{
            //    MailMergeTemplate[] merge = new MailMergeTemplate[3];
            //    merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDiNuocNgoaiTapThe3NguoiMaster.rtf")); ;
            //    merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDiNuocNgoaiTapThe3NguoiDetail.rtf")); ;
            //    merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDiNuocNgoaiTapThe3Nguoi.rtf");
            //    if (merge[0] != null)
            //        MailMergeHelper.ShowEditor<Non_QuyetDinhDiNuocNgoai>(list, obs, merge);
            //    else
            //        HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi nước ngoài trong hệ thống.");
            //}
        }
    }
}
