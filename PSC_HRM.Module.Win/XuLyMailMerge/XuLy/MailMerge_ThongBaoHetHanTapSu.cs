using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.TapSu;
using DevExpress.Xpo;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_ThongBaoHetHanTapSu : IMailMerge<IList<DanhSachHetHanTapSu>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<DanhSachHetHanTapSu> list)
        {

            var tapThe = from qd in list
                         where qd.ListHetHanTapSu.Count > 0
                         select qd;

            if (tapThe.Count() > 0)
            {
                XuLy(obs, tapThe.ToList());
            }
        }

        private void XuLy(DevExpress.ExpressApp.IObjectSpace obs, List<DanhSachHetHanTapSu> list)
        {
            bool ktrachon = false;
            var danhsach = new List<Non_ThongBaoHetHanTapSu>();
            Non_ThongBaoHetHanTapSu qd;
            foreach (DanhSachHetHanTapSu item in list)
            {
                qd = new Non_ThongBaoHetHanTapSu();
                qd.Oid = item.Oid.ToString();
                qd.TimKiemTuNgay = item.TuNgay != DateTime.MinValue ? item.TuNgay.ToString("d") : "";
                qd.TimKiemDenNgay = item.DenNgay != DateTime.MinValue ? item.DenNgay.ToString("d") : "";
                qd.NgayQuyetDinh = HamDungChung.GetServerTime().ToString("'ngày' dd 'tháng' MM 'năm' yyyy");

                //master
                Non_ChiTietThongBaoHetHanTapSuMaster master = new Non_ChiTietThongBaoHetHanTapSuMaster();
                master.Oid = item.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                qd.Master.Add(master);

                Non_ChiTietThongBaoHetHanTapSuDetail detail;
                int stt = 1;
                foreach (HetHanTapSu item2 in item.ListHetHanTapSu)
                {
                    if (item2.Chon == true)
                    {
                        ktrachon = true;
                        detail = new Non_ChiTietThongBaoHetHanTapSuDetail();
                        detail.Oid = item.Oid.ToString();
                        detail.STT = stt.ToString();
                        detail.HoTen = item2.ThongTinNhanVien.HoTen;
                        detail.DonVi = HamDungChung.GetTenBoPhan(item2.BoPhan);
                        detail.CanBoHuongDan = item2.CanBoHuongDan != null ? item2.CanBoHuongDan.HoTen : "";

                        ChiTietQuyetDinhHuongDanTapSu chitiet = obs.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu =? and ThongTinNhanVien=?", item2.QuyetDinhHuongDanTapSu.Oid, item2.ThongTinNhanVien.Oid));
                        detail.TuNgay = chitiet.TuNgay != DateTime.MinValue ? chitiet.TuNgay.ToString("d") : "";
                        detail.DenNgay = chitiet.DenNgay != DateTime.MinValue ? chitiet.DenNgay.ToString("d") : "";

                        qd.Detail.Add(detail);
                        stt++;
                    }
                }
                danhsach.Add(qd);
            }
            if (ktrachon == true)
            {
                MailMergeTemplate[] merge = new MailMergeTemplate[3];
                merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "ThongBaoHetHanTapSuMaster.rtf")); ;
                merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "ThongBaoHetHanTapSuDetail.rtf")); ;
                merge[0] = HamDungChung.GetTemplate(obs, "ThongBaoHetHanTapSu.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_ThongBaoHetHanTapSu>(danhsach, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in thông báo hết hạn tập sự trong hệ thống.");
            }
            else
                HamDungChung.ShowWarningMessage("Chưa chọn hết hạn tập sự.");
        }
    }
}
