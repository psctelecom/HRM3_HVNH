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
    public class MailMerge_QuyetDinhNghiThaiSan : IMailMerge<IList<QuyetDinhNghiThaiSan>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNghiThaiSan> qdList)
        {
            var list = new List<Non_QuyetDinhNghiThaiSan>();
            Non_QuyetDinhNghiThaiSan qd;
            foreach (QuyetDinhNghiThaiSan quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNghiThaiSan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
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

                qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.LoaiNhanVien = quyetDinh.ThongTinNhanVien.LoaiNhanSu != null ? quyetDinh.ThongTinNhanVien.LoaiNhanSu.TenLoaiNhanSu : "";
                

                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("'Ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NoiSinh = quyetDinh.ThongTinNhanVien.NoiSinh.FullDiaChi != null ? quyetDinh.ThongTinNhanVien.NoiSinh.FullDiaChi : "";

                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                if (TruongConfig.MaTruong == "BUH")
                {
                    qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ?
                                    quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu :
                                    quyetDinh.ThongTinNhanVien.ChucDanh != null ?
                                        quyetDinh.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                }


                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                qd.DenNgay = quyetDinh.DenNgay != DateTime.MinValue ? quyetDinh.DenNgay.ToString("d") : "";
                qd.SoSoBHXH = quyetDinh.SoSoBHXH != null ? quyetDinh.SoSoBHXH : "";

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNghiThaiSan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNghiThaiSan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nghỉ thai sản trong hệ thống.");
        }
    }
}
