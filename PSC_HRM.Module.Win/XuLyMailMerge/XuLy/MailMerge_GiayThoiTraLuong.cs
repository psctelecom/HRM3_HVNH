using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_GiayThoiTraLuong : IMailMerge<IList<QuyetDinhChuyenCongTac>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChuyenCongTac> qdList)
        {
            var list = new List<Non_GiayThoiTraLuong>();

            Non_GiayThoiTraLuong qd;
            foreach (QuyetDinhChuyenCongTac obj in qdList)
            {
                qd = new Non_GiayThoiTraLuong();
                qd.Oid = obj.Oid.ToString();
                qd.DonViChuQuan = obj.ThongTinTruong != null ? obj.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = obj.TenCoQuan != null ? obj.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = obj.TenCoQuan;
                qd.SoQuyetDinh = obj.SoQuyetDinh;
                qd.NgayQuyetDinh = obj.NgayQuyetDinh != DateTime.MinValue ? obj.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.NgayHieuLuc = obj.NgayHieuLuc != DateTime.MinValue ? obj.NgayHieuLuc.ToString("d") : "";
                qd.NoiDung = obj.NoiDung;
                qd.ChucVuNguoiKy = obj.ChucVuNguoiKy != null ? obj.ChucVuNguoiKy.TenChucVu : "";
                NguoiSuDung user = HamDungChung.CurrentUser();
                qd.NguoiKy = string.Empty;// (user != null && user.NguoiKyTen != null) ? user.NguoiKyTen.PhongKHTC : "";

                qd.ChucDanh = HamDungChung.GetChucDanh(obj.ThongTinNhanVien);
                qd.HoTen = obj.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(obj.BoPhan);
                qd.ChucVu = obj.ThongTinNhanVien.ChucVu != null ? String.Format("{0} {1}", obj.ThongTinNhanVien.ChucVu.TenChucVu, HamDungChung.GetTenBoPhan(obj.BoPhan)) : "";
                qd.MaNgach = obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                qd.NgachLuong = obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                qd.BacLuong = obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong != null ? obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong.TenBacLuong : "";
                qd.HeSoLuong = obj.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
                qd.MocNangLuong = obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong != DateTime.MinValue ? obj.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong.ToString("d") : "";
                qd.HSPCChucVu = obj.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu.ToString("N2");
                qd.PhuCapVuotKhung = obj.ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung.ToString("N0");
                qd.PhuCapThamNien = obj.ThongTinNhanVien.NhanVienThongTinLuong.ThamNien.ToString("N0");
                qd.PhuCapUuDai = obj.ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai.ToString("N0");
                qd.DongBaoHiemDenHetNgay = obj.TuNgay != DateTime.MinValue ? obj.TuNgay.AddDays(-1).ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.DeNghiDongBaoHiemTuNgay = obj.TuNgay != DateTime.MinValue ? obj.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                qd.SoNgayNghiPhepConLai = NghiPhep.NghiPhepHelper.SoNgayNghiPhepConLai(((XPObjectSpace)obs).Session, obj.ThongTinNhanVien).ToString("0#");
                HoSoBaoHiem baoHiem = obs.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", obj.ThongTinNhanVien.Oid));
                if (baoHiem != null)
                {
                    qd.SoSoBHXH = baoHiem.SoSoBHXH;
                }
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "GiayThoiTraLuong.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_GiayThoiTraLuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in giấy thôi trả lương trong hệ thống.");
        }
    }
}
