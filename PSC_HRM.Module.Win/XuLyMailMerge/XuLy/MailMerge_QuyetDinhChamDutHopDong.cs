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
    public class MailMerge_QuyetDinhChamDutHopDong : IMailMerge<IList<QuyetDinhChamDutHopDong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChamDutHopDong> qdList)
        {
            if (qdList != null && !qdList[0].IsThongBaoChamDutHopDong)
            {
                var list = new List<Non_QuyetDinhChamDutHopDong>();
                Non_QuyetDinhChamDutHopDong qd;
                foreach (QuyetDinhChamDutHopDong quyetDinh in qdList)
                {
                    qd = new Non_QuyetDinhChamDutHopDong();
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
                    qd.NgayHieuLucDate=quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    if (quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                    qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : string.Empty;
                   // qd.SoCMND = quyetDinh.HopDong.CMND;
                    qd.NgayCapCMND=quyetDinh.HopDong.NgayCap.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NoiCapCMND = quyetDinh.HopDong.NoiCap.ToString();
                    qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                    qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                    qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                    qd.LoaiNhanVien = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                    qd.SoHopDong = quyetDinh.HopDong != null ? quyetDinh.HopDong.SoHopDong : string.Empty;
                    qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("dd/MM/yyyy") : string.Empty;
                    list.Add(qd);

                    MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhChamDutHopDong.rtf");
                    if (merge != null)
                        MailMergeHelper.ShowEditor<Non_QuyetDinhChamDutHopDong>(list, obs, merge);
                    else
                        HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chấm dứt hợp đồng trong hệ thống.");
                }
            }
            else
            {
                qdList[0].IsThongBaoChamDutHopDong = false;
                //
                var list = new List<Non_ThongBaoChamDutHopDong>();
                Non_ThongBaoChamDutHopDong qd;
                foreach (QuyetDinhChamDutHopDong quyetDinh in qdList)
                {
                    qd = new Non_ThongBaoChamDutHopDong();
                    qd.Oid = quyetDinh.Oid.ToString();
                    qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                    qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
                    qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                    qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                    qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                    qd.CanCu = quyetDinh.CanCu;
                    qd.NoiDung = quyetDinh.NoiDung;
                    qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                    qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                    qd.NguoiKy = quyetDinh.NguoiKy1;
                    if (quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";
                    qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : string.Empty;

                    qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                    qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                    qd.LoaiNhanVien = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                    qd.SoHopDong = quyetDinh.HopDong != null ? quyetDinh.HopDong.SoHopDong : string.Empty;
                    qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : string.Empty;
                    list.Add(qd);

                    MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "ThongBaoChamDutHopDong.rtf");

                    if (merge != null)
                        MailMergeHelper.ShowEditor<Non_ThongBaoChamDutHopDong>(list, obs, merge);
                    else
                        HamDungChung.ShowWarningMessage("Không tìm thấy thông báo chấm dứt hợp đồng trong hệ thống.");
                }
            }
            
        }
    }
}
