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
    public class MailMerge_QuyetDinhTiepNhanDiNuocNgoai : IMailMerge<IList<QuyetDinhTiepNhanVienChucDiNuocNgoai>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTiepNhanVienChucDiNuocNgoai> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTiepNhanVienChucDiNuocNgoai> qdList)
        {
            var list = new List<Non_QuyetDinhTiepNhanDiNuocNgoaiCaNhan>();
            Non_QuyetDinhTiepNhanDiNuocNgoaiCaNhan qd;
            foreach (QuyetDinhTiepNhanVienChucDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanDiNuocNgoaiCaNhan();
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
                qd.SoQuyetDinhCu = quyetDinh.QuyetDinhDiNuocNgoai.SoQuyetDinh;
                qd.NgayhieuLucCu = quyetDinh.QuyetDinhDiNuocNgoai.NgayHieuLuc.ToString("dd/MM/yyyy");
                qd.TuNgayCu = quyetDinh.QuyetDinhDiNuocNgoai.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgayCu = quyetDinh.QuyetDinhDiNuocNgoai.DenNgay.ToString("dd/MM/yyyy");

                if (quyetDinh.QuyetDinhDiNuocNgoai != null)
                {
                    qd.QuocGia = quyetDinh.QuyetDinhDiNuocNgoai.QuocGia != null ? quyetDinh.QuyetDinhDiNuocNgoai.QuocGia.TenQuocGia : "";
                    qd.LyDo = quyetDinh.QuyetDinhDiNuocNgoai.LyDo ?? "";
                    qd.NguonKinhPhi = quyetDinh.QuyetDinhDiNuocNgoai.NguonKinhPhi != null ? quyetDinh.QuyetDinhDiNuocNgoai.NguonKinhPhi.TenNguonKinhPhi : "";
                    qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhDiNuocNgoai.TruongHoTro) ? quyetDinh.QuyetDinhDiNuocNgoai.TruongHoTro : "";
            
                }
                 qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");

                foreach (ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai item in quyetDinh.ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    //qd.MocNangLuongLanSau = item.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau.ToString("dd/MM/yyyy");
                    qd.MocNangLuong = item.MocNangLuong.ToString("dd/MM/yyyy");
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

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanDiNuocNgoai.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanDiNuocNgoaiCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi nước ngoài trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTiepNhanVienChucDiNuocNgoai> qdList)
        {
            int stt = 1;
            var list = new List<Non_QuyetDinhTiepNhanDiNuocNgoai>();
            Non_QuyetDinhTiepNhanDiNuocNgoai qd;
            foreach (QuyetDinhTiepNhanVienChucDiNuocNgoai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTiepNhanDiNuocNgoai();
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

                qd.QuocGia = quyetDinh.QuyetDinhDiNuocNgoai.QuocGia != null ? quyetDinh.QuyetDinhDiNuocNgoai.QuocGia.TenQuocGia : "";
                qd.NguonKinhPhi = quyetDinh.QuyetDinhDiNuocNgoai.NguonKinhPhi != null ? quyetDinh.QuyetDinhDiNuocNgoai.NguonKinhPhi.TenNguonKinhPhi : "";
                qd.TruongHoTro = !String.IsNullOrEmpty(quyetDinh.QuyetDinhDiNuocNgoai.TruongHoTro) ? quyetDinh.QuyetDinhDiNuocNgoai.TruongHoTro : "";
                qd.TuNgay = quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.LyDo = quyetDinh.QuyetDinhDiNuocNgoai.LyDo ?? "";

                //master
                Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiMaster master = new Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiDetail detail;
                quyetDinh.ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));

                foreach (ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai item in quyetDinh.ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai)
                {
                    detail = new Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    //detail.MocNangLuongLanSau = item.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau.ToString("dd/MM/yyyy");
                    detail.MocNangLuong = item.MocNangLuong.ToString("dd/MM/yyyy");

                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTiepNhanDiNuocNgoaiTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTiepNhanDiNuocNgoaiTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhTiepNhanDiNuocNgoaiTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTiepNhanDiNuocNgoai>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ đi nước ngoài trong hệ thống.");
        }
    }
}
