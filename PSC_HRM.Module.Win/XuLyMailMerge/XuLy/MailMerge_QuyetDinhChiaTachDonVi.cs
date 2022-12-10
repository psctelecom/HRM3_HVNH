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
    public class MailMerge_QuyetDinhChiaTachDonVi : IMailMerge<IList<QuyetDinhChiaTachDonVi>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChiaTachDonVi> qdList)
        {
            var list = new List<Non_QuyetDinhChiaTachDonVi>();
            Non_QuyetDinhChiaTachDonVi qd;
            foreach (QuyetDinhChiaTachDonVi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChiaTachDonVi();
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
                qd.ChucDanhNguoiKy = PSC_HRM.Module.HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                //master
                Non_ChiTietQuyetDinhChiaTachDonViMaster master = new Non_ChiTietQuyetDinhChiaTachDonViMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DonVi = quyetDinh.BoPhanMoi.TenBoPhan.ToUpper();
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhChiaTachDonViDetail detail;
                quyetDinh.ListChiTietChiaTachDonVi.Sorting.Clear();
                quyetDinh.ListChiTietChiaTachDonVi.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.HoTen", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietChiaTachDonVi item in quyetDinh.ListChiTietChiaTachDonVi)
                {
                    detail = new Non_ChiTietQuyetDinhChiaTachDonViDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.HocVi = HamDungChung.GetHocVi(item.ThongTinNhanVien);
                    detail.GhiChu = item.GhiChu;

                    qd.Detail.Add(detail);
                    stt++;
                }
                list.Add(qd);
            }

            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhChiaTachDonVi.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChiaTachDonViMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChiaTachDonViDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChiaTachDonVi>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chia tách đơn vị trong hệ thống.");
        }
    }
}
