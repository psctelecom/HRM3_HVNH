using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhThanhLapHoiDongNangLuong : IMailMerge<IList<QuyetDinhThanhLapHoiDongNangLuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThanhLapHoiDongNangLuong> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapHoiDongNangLuong>();
            Non_QuyetDinhThanhLapHoiDongNangLuong qd;
            foreach (QuyetDinhThanhLapHoiDongNangLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapHoiDongNangLuong();
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

                //master
                Non_ChiTietQuyetDinhThanhLapHoiDongMaster master = new Non_ChiTietQuyetDinhThanhLapHoiDongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NamHoc = quyetDinh.Nam.ToString("####");
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;

                //detail
                Non_ChiTietQuyetDinhThanhLapHoiDongNangLuongDetail detail;
                quyetDinh.ListChiTietQuyetDinhThanhLapHoiDongNangLuong.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhThanhLapHoiDongNangLuong.Sorting.Add(new DevExpress.Xpo.SortProperty("ChucDanhHoiDongNangLuong.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhThanhLapHoiDongNangLuong item in quyetDinh.ListChiTietQuyetDinhThanhLapHoiDongNangLuong)
                {
                    detail = new Non_ChiTietQuyetDinhThanhLapHoiDongNangLuongDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    detail.ChucDanhHoiDongNangLuong = item.ChucDanhHoiDongNangLuong != null ? item.ChucDanhHoiDongNangLuong.TenChucDanh : "";

                    qd.Detail.Add(detail);
                    stt++;
                }
                //Lấy tổng số nhân viên
                master.TongNhanVien = stt-1;

                //Đưa chi tiết vào master
                qd.Master.Add(master);

                list.Add(qd);
                
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapHoiDongNangLuong.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongNangLuongMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongNangLuongDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapHoiDongNangLuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập hội đồng nâng lương trong hệ thống.");
        }
    }
}
