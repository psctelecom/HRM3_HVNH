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
    public class MailMerge_QuyetDinhNangPhuCap : IMailMerge<IList<QuyetDinhNangPhuCap>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNangPhuCap> qdList)
        {
            var list = new List<Non_QuyetDinhNangPhuCap>();
            Non_QuyetDinhNangPhuCap qd;
            foreach (QuyetDinhNangPhuCap quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangPhuCap();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");

                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                ChiTietQuyetDinhNangPhuCap chitiet = quyetDinh.ListChiTietQuyetDinhNangPhuCap[0];

                qd.NhanVien = chitiet.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(chitiet.BoPhan);
                qd.ChucDanh = HamDungChung.GetChucDanh(chitiet.ThongTinNhanVien);
                qd.ChucVu = chitiet.ThongTinNhanVien.ChucDanh != null ? chitiet.ThongTinNhanVien.ChucDanh.TenChucDanh : null;
                qd.DanhXungVietHoa = chitiet.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = chitiet.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(chitiet.ThongTinNhanVien);
                qd.SoTienPhuCap = (chitiet.PhuCapTienXangMoi
                                   + chitiet.PhuCapDienThoaiMoi
                                   + chitiet.PhuCapTrachNhiemCongViecMoi).ToString("N0");
                qd.NgayHuongPhuCap = chitiet.NgayHuongPhuCapMoi.ToString("d"); ;

                if (chitiet.PhuCapDienThoaiCu != chitiet.PhuCapDienThoaiMoi)
                    qd.CanCu = "Phụ cấp điện thoại";
                else if (chitiet.PhuCapTienXangCu != chitiet.PhuCapTienXangMoi)
                    qd.CanCu = "Phụ cấp tiền xăng";
                else
                    qd.CanCu = "Phụ cấp chức vụ";

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangPhuCap.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangPhuCap>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng phụ trong hệ thống.");
        }

    }
}
