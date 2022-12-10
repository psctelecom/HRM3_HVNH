using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;
using PSC_HRM.Module.NghiHuu;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_ThongBaoNghiHuuTimKiem : IMailMerge<IList<DenTuoiNghiHuu>>
    {
        public void Merge(IObjectSpace obs, IList<DenTuoiNghiHuu> qdList)
        {
            ThongTinTruong truong = HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session);
            var list = new List<Non_ThongBaoNghiHuu>();
            Non_ThongBaoNghiHuu qd;
            foreach (DenTuoiNghiHuu obj in qdList)
            {
                if (obj != null && obj.ThongTinNhanVien != null && truong != null)
                {
                    qd = new Non_ThongBaoNghiHuu();
                    qd.Oid = obj.Oid.ToString();
                    qd.DonViChuQuan = truong.DonViChuQuan;
                    qd.TenTruongVietHoa = truong.TenBoPhan.ToUpper();
                    qd.TenTruongVietThuong = truong.TenBoPhan;
                    qd.NgayThongBao = HamDungChung.GetServerTime().ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayThongBaoDate = HamDungChung.GetServerTime().ToString("dd/MM/yyyy");
                    qd.HoTen = obj.ThongTinNhanVien.HoTen;
                    qd.GioiTinh = obj.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.NgaySinh = obj.ThongTinNhanVien.NgaySinh.ToString("d");
                    qd.NoiSinh = obj.ThongTinNhanVien.NoiSinh != null ? obj.ThongTinNhanVien.NoiSinh.FullDiaChi : "";
                    qd.DonVi = HamDungChung.GetTenBoPhan(obj.BoPhan);
                    qd.ChucVu = obj.ThongTinNhanVien.ChucVu != null ? obj.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.NoiOHienNay = obj.ThongTinNhanVien.NoiOHienNay != null ? obj.ThongTinNhanVien.NoiOHienNay.FullDiaChi : "";
                    qd.NgayNghiHuu = obj.NgaySeNghiHuu.ToString("d");
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
                        qd.HieuTruong = hieuTruong != null ? hieuTruong.HoTen : "";
                    }
                    list.Add(qd);
                }
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "ThongBaoNghiHuu.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_ThongBaoNghiHuu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in thông báo nghỉ hưu trong hệ thống.");
        }
    }
}
