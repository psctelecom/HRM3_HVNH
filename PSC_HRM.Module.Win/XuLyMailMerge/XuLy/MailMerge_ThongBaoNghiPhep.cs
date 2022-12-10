using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.NghiPhep;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_ThongBaoNghiPhep : IMailMerge<IList<ChiTietThongTinNghiPhep>>
    {
        public void Merge(IObjectSpace obs, IList<ChiTietThongTinNghiPhep> tbList)
        {
            ThongTinTruong truong = HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session);
            var list = new List<Non_ThongBaoNghiPhep>();
            Non_ThongBaoNghiPhep qd;
            foreach (ChiTietThongTinNghiPhep obj in tbList)
            {
                if (obj != null && obj.ThongTinNghiPhep.ThongTinNhanVien != null && truong != null)
                {
                    qd = new Non_ThongBaoNghiPhep();
                    qd.Oid = obj.Oid.ToString();
                    qd.DonViChuQuan = truong.DonViChuQuan;
                    qd.TenTruongVietHoa = truong.TenBoPhan.ToUpper();
                    qd.TenTruongVietThuong = truong.TenBoPhan;
                    qd.NgayThongBao = HamDungChung.GetServerTime().ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayThongBaoDate = HamDungChung.GetServerTime().ToString("dd/MM/yyyy");
                    qd.HoTen = obj.ThongTinNghiPhep.ThongTinNhanVien.HoTen;
                    qd.GioiTinh = obj.ThongTinNghiPhep.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.NgaySinh = obj.ThongTinNghiPhep.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NoiSinh = obj.ThongTinNghiPhep.ThongTinNhanVien.NoiSinh != null ? obj.ThongTinNghiPhep.ThongTinNhanVien.NoiSinh.FullDiaChi : "";
                    qd.DonVi = HamDungChung.GetTenBoPhan(obj.ThongTinNghiPhep.BoPhan);
                    if (TruongConfig.MaTruong.Equals("NEU"))
                        qd.ChucVu = HamDungChung.GetChucVuNhanVien(obj.ThongTinNghiPhep.ThongTinNhanVien);
                    else
                        qd.ChucVu = obj.ThongTinNghiPhep.ThongTinNhanVien.ChucVu != null ? obj.ThongTinNghiPhep.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.NoiOHienNay = obj.ThongTinNghiPhep.ThongTinNhanVien.NoiOHienNay != null ? obj.ThongTinNghiPhep.ThongTinNhanVien.NoiOHienNay.FullDiaChi : "";
                    ThongTinNhanVien hieuTruong = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ? and ThongTinTruong=?", "hiệu trưởng", "đang làm việc", truong.Oid));
                    ThongTinNhanVien truongPhongTCCB = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ? and BoPhan.TenBoPhan like ?", "Trưởng phòng", "đang làm việc", "Phòng Tổ chức cán bộ"));
                    if (truongPhongTCCB != null)
                    {
                        qd.TruongPhongTCCB = truongPhongTCCB.HoTen;
                        qd.ChucDanhTPTCCB = HamDungChung.GetChucDanh(truongPhongTCCB);
                    }
                    if (hieuTruong != null)
                    {
                        qd.ChucDanhNguoiKy = HamDungChung.GetChucDanh(hieuTruong);
                        qd.HieuTruong = hieuTruong.HoTen;
                    }
                    qd.ChucDanh = HamDungChung.GetChucDanh(obj.ThongTinNghiPhep.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(obj.ThongTinNghiPhep.ThongTinNhanVien);
                    qd.NgachLuong = obj.ThongTinNghiPhep.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? obj.ThongTinNghiPhep.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    qd.DanhXungVietHoa = obj.ThongTinNghiPhep.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = obj.ThongTinNghiPhep.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";

                    //foreach(ChiTietThongTinNghiPhep ct in obj.ListChiTietThongTinNghiPhep)
                    //{
                    //    if(ct != null)
                    //    {
                            qd.LyDo = obj.LyDoNghi;
                            qd.NgayBatDauNghiPhep = obj.TuNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                            qd.NgayKetThucNghiPhep = obj.DenNgay.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                            qd.ThoiGianNghiPhep = obj.SoNgay.ToString("N1");
                            qd.DiaDiemNghi = obj.NoiNghiPhep;
                    //    }
                    //}

                    list.Add(qd);
                }
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "ThongBaoNghiPhep.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_ThongBaoNghiPhep>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in thông báo nghỉ hưu trong hệ thống.");
        }
    }
}
