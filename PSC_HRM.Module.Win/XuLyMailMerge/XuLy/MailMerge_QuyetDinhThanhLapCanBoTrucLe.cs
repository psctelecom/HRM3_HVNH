﻿using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.KhenThuong;
using PSC_HRM.Module.KyLuat;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhThanhLapCanBoTrucLe : IMailMerge<IList<QuyetDinhTrucLe>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTrucLe> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapCanBoTrucLe>();
            Non_QuyetDinhThanhLapCanBoTrucLe qd;
            foreach (QuyetDinhTrucLe quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThanhLapCanBoTrucLe();
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
                qd.NgayDeNghi = quyetDinh.NgayDeNghi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.TuNgay = quyetDinh.TuNgay.ToString("dd/MM/yyyy");
                qd.DenNgay = quyetDinh.DenNgay.ToString("dd/MM/yyyy");
                qd.NoiDungVietThuong = quyetDinh.NoiDungTrucLe;
                qd.NoiDungVietHoa = quyetDinh.NoiDungTrucLe.ToUpper();


                //master
                Non_ChiTietQuyetDinhThanhLapHoiDongMaster master = new Non_ChiTietQuyetDinhThanhLapHoiDongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NoiDungTrucLe = qd.NoiDungVietHoa;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhThanhLapCanBoTrucLeDetail detail;
                quyetDinh.ListCanBoTrucLe.Sorting.Clear();
                quyetDinh.ListCanBoTrucLe.Sorting.Add(new DevExpress.Xpo.SortProperty("ChucDanhCanBoTrucLe.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (CanBoTrucLe item in quyetDinh.ListCanBoTrucLe)
                {
                    detail = new Non_ChiTietQuyetDinhThanhLapCanBoTrucLeDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    detail.ChucDanhCanBoTrucLe = item.ChucDanhCanBoTrucLe != null ? item.ChucDanhCanBoTrucLe.TenChucDanhCanBoTrucLe : "";
                    detail.VaiTroCanBoTrucLe = item.VaiTroDamNhiem;
                    qd.Detail.Add(detail);
                    stt++;
                }
                master.TongNhanVien = stt - 1;
                list.Add(qd);
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhTrucLe.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTrucLeMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTrucLeDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapCanBoTrucLe>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập hội đồng kỷ luật trong hệ thống.");
        }
    }
}
