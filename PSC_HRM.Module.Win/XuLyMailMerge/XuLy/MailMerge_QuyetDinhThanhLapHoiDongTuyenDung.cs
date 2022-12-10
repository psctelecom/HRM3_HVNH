using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.TuyenDung;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhThanhLapHoiDongTuyenDung : IMailMerge<IList<QuyetDinhThanhLapHoiDongTuyenDung>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThanhLapHoiDongTuyenDung> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapHoiDongTuyenDung>();
            Non_QuyetDinhThanhLapHoiDongTuyenDung qd;
            foreach (QuyetDinhThanhLapHoiDongTuyenDung quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapHoiDongTuyenDung();
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
                qd.NamHoc = quyetDinh.QuanLyTuyenDung.NamHoc.NgayBatDau.Year.ToString("####");
                qd.Dot = quyetDinh.QuanLyTuyenDung.DotTuyenDung.ToString("N0");
                qd.BoPhan = quyetDinh.BoPhan.ToString();
                qd.CanBo = quyetDinh.ThongTinNhanVien.ToString();
                qd.NgaySinh = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                //master
                Non_ChiTietQuyetDinhThanhLapHoiDongMaster master = new Non_ChiTietQuyetDinhThanhLapHoiDongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NamHoc = qd.NamHoc;
                master.Dot = qd.Dot;
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                
                //detail
                Non_ChiTietQuyetDinhThanhLapHoiDongTuyenDungDetail detail;
                quyetDinh.ListHoiDongTuyenDung.Sorting.Clear();
                quyetDinh.ListHoiDongTuyenDung.Sorting.Add(new DevExpress.Xpo.SortProperty("ChucDanh.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (HoiDongTuyenDung item in quyetDinh.ListHoiDongTuyenDung)
                {
                    detail = new Non_ChiTietQuyetDinhThanhLapHoiDongTuyenDungDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    detail.ChucDanhHoiDongTuyenDung = item.ChucDanh != null ? item.ChucDanh.TenChucDanh : "";

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
                //Lấy tổng số nhân viên
                master.TongNhanVien = stt - 1;

                //Đưa chi tiết vào master
                qd.Master.Add(master);

                list.Add(qd);
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapHoiDongTuyenDung.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongTuyenDungMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongTuyenDungDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapHoiDongTuyenDung>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập hội đồng tuyển dụng trong hệ thống.");
        }
    }
}
