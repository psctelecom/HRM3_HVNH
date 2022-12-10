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
    public class MailMerge_QuyetDinhDiNuocNgoai : IMailMerge<IList<QuyetDinhDiNuocNgoai>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhDiNuocNgoai> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhDiNuocNgoai.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhDiNuocNgoai.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDiNuocNgoai> qdList)
        {
            var list = new List<Non_QuyetDinhDiNuocNgoaiCaNhan>();
            Non_QuyetDinhDiNuocNgoaiCaNhan qd;
            foreach (QuyetDinhDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDiNuocNgoaiCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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
                qd.NguonKinhPhi = quyetDinh.NguonKinhPhi != null ? quyetDinh.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.TruongHoTro) ? quyetDinh.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("dd/MM/yyyy");                
                qd.LyDo = quyetDinh.LyDo ?? "";
                qd.GhiChu = quyetDinh.GhiChu;
                qd.GhiChuTG = quyetDinh.GhiChuTG;
                qd.SoCongVan = quyetDinh.SoCongVan;
                qd.DiaDiem = quyetDinh.DiaDiem;
                qd.DonViToChuc = quyetDinh.DonViToChuc;
                qd.NgayXinDi = quyetDinh.NgayXinDi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayXinDiDate = quyetDinh.NgayXinDi.ToString("dd/MM/yyyy");  

                foreach (ChiTietQuyetDinhDiNuocNgoai item in quyetDinh.ListChiTietQuyetDinhDiNuocNgoai)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.ChucDanhVietThuong = HamDungChung.GetChucDanhVietThuong(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaDonVi = item.BoPhan != null ? item.BoPhan.MaQuanLy : "";
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.BoPhan);
                    qd.ChucVuNhanVienVietThuong = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien).ToLower();
                    qd.ChucVuNhanVienVietHoa = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien).ToUpper();
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
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

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhDiNuocNgoai.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhDiNuocNgoaiCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi nước ngoài trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhDiNuocNgoai> qdList)
        {
            int stt = 1;
            var list = new List<Non_QuyetDinhDiNuocNgoai>();
            Non_QuyetDinhDiNuocNgoai qd;
            foreach (QuyetDinhDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhDiNuocNgoai();
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
                qd.SoCongVan = quyetDinh.SoCongVan;
                qd.DiaDiem = quyetDinh.DiaDiem;
                qd.DonViToChuc = quyetDinh.DonViToChuc;

                //master
                Non_ChiTietQuyetDinhDiNuocNgoaiMaster master = new Non_ChiTietQuyetDinhDiNuocNgoaiMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhDiNuocNgoaiDetail detail;
                quyetDinh.ListChiTietQuyetDinhDiNuocNgoai.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhDiNuocNgoai.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));

                foreach (ChiTietQuyetDinhDiNuocNgoai item in quyetDinh.ListChiTietQuyetDinhDiNuocNgoai)
                {
                    detail = new Non_ChiTietQuyetDinhDiNuocNgoaiDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ChucVu = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.BoPhan);
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
                list.Add(qd);
            }


            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDiNuocNgoaiTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhDiNuocNgoaiTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhDiNuocNgoaiTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhDiNuocNgoai>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi nước ngoài trong hệ thống.");

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
