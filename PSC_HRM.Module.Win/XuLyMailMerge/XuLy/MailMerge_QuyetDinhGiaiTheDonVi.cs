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
    public class MailMerge_QuyetDinhGiaiTheDonVi : IMailMerge<IList<QuyetDinhGiaiTheDonVi>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhGiaiTheDonVi> qdList)
        {
            var list = new List<Non_QuyetDinhGiaiTheDonVi>();
            Non_QuyetDinhGiaiTheDonVi qd;
            foreach (QuyetDinhGiaiTheDonVi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhGiaiTheDonVi();
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

                qd.DonVi = quyetDinh.BoPhan.TenBoPhan;
                qd.QuyetDinhThanhLapDonVi = String.Format("{0} ngày {1:d} của {2} {3}", quyetDinh.QuyetDinhThanhLapDonVi.SoQuyetDinh, quyetDinh.QuyetDinhThanhLapDonVi.NgayHieuLuc, quyetDinh.QuyetDinhThanhLapDonVi.ChucVuNguoiKy.TenChucVu, quyetDinh.QuyetDinhThanhLapDonVi.TenCoQuan);
                qd.ThoiHanBanGiao = quyetDinh.ThoiHanBanGiao != DateTime.MinValue ? quyetDinh.ThoiHanBanGiao.ToString("d") : "";
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhGiaiTheDonVi.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhGiaiTheDonVi>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ giải thể đơn vị trong hệ thống.");
        }
    }
}
