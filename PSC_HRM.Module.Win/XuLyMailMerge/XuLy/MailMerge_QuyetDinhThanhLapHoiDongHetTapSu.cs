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
    public class MailMerge_QuyetDinhThanhLapHoiDongHetTapSu : IMailMerge<IList<QuyetDinhThanhLapHoiDongDanhGiaHetTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThanhLapHoiDongDanhGiaHetTapSu> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapHoiDongHetTapSu>();
            Non_QuyetDinhThanhLapHoiDongHetTapSu qd;
            foreach (QuyetDinhThanhLapHoiDongDanhGiaHetTapSu quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapHoiDongHetTapSu();
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
                qd.TenTapSu = quyetDinh.TenTapSu;
                qd.DonViTapSuVietHoa = quyetDinh.DonViTapSu.ToUpper();
                qd.DonViTapSuVietThuong = quyetDinh.DonViTapSu;
                
                //qd.NamHoc = quyetDinh.QuanLyTuyenDung.NamHoc.NgayBatDau.Year.ToString("####");
                //qd.Dot = quyetDinh.QuanLyTuyenDung.DotTuyenDung.ToString("N0");
                
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
                master.TenTapSu = qd.TenTapSu;
                master.DonViTapSu = qd.DonViTapSuVietHoa;
                
                
                //detail
                Non_ChiTietQuyetDinhThanhLapHoiDongHetTapSuDetail detail;
                quyetDinh.ListHoiDongHetTapSu.Sorting.Clear();
                quyetDinh.ListHoiDongHetTapSu.Sorting.Add(new DevExpress.Xpo.SortProperty("ChucDanh.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (HoiDongHetTapSu item in quyetDinh.ListHoiDongHetTapSu)
                {
                    detail = new Non_ChiTietQuyetDinhThanhLapHoiDongHetTapSuDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.VaiTro = item.VaiTroDamNhiem;
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
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapHoiDongHetTapSu.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongHetTapSuMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapHoiDongHetTapSuDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapHoiDongHetTapSu>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập hội đồng tuyển dụng trong hệ thống.");
        }
    }
}
