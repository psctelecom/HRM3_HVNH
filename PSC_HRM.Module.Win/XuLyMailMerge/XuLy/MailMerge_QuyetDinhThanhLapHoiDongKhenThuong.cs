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
    public class MailMerge_QuyetDinhThanhLapHoiDongKhenThuong : IMailMerge<IList<QuyetDinhThanhLapHoiDongKhenThuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThanhLapHoiDongKhenThuong> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapHoiDongKhenThuong>();
            Non_QuyetDinhThanhLapHoiDongKhenThuong qd;
            foreach (QuyetDinhThanhLapHoiDongKhenThuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapHoiDongKhenThuong();
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
                if (quyetDinh.QuanLyKhenThuong != null)
                    master.NamHoc = quyetDinh.QuanLyKhenThuong.NamHoc.TenNamHoc;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhThanhLapHoiDongKhenThuongDetail detail;
                quyetDinh.ListHoiDongKhenThuong.Sorting.Clear();
                quyetDinh.ListHoiDongKhenThuong.Sorting.Add(new DevExpress.Xpo.SortProperty("ChucDanhHoiDongKhenThuong.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (HoiDongKhenThuong item in quyetDinh.ListHoiDongKhenThuong)
                {
                    detail = new Non_ChiTietQuyetDinhThanhLapHoiDongKhenThuongDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.ChucDanhHoiDongKhenThuong = item.ChucDanhHoiDongKhenThuong != null ? item.ChucDanhHoiDongKhenThuong.TenChucDanhHoiDongKhenThuong : "";
                    
                    detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.MaQuanLy :
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.MaQuanLy : "";
                    }

                    detail.DonVi = item.BoPhan.TenBoPhan;
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        detail.DonVi = item.BoPhan.MaQuanLy;
                    }

                    qd.Detail.Add(detail);
                    stt++;
                }
                master.TongNhanVien = stt - 1;
                list.Add(qd);
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapHoiDongKhenThuong.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongKhenThuongMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongKhenThuongDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapHoiDongKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập hội đồng khen thưởng trong hệ thống.");
        }
    }
}
