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
    public class MailMerge_QuyetDinhThanhLapKhac : IMailMerge<IList<QuyetDinhThanhLapKhac>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThanhLapKhac> qdList)
        {
            var tapThe = from qd in qdList
                         //where qd.ListChiTietThanhLapKhac_ThanhVien.Count > 1
                         select qd;
            if (tapThe.Count() > 0)
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhThanhLapKhac> qdList)
        {
            var list = new List<Non_QuyetDinhThanhLapKhac>();
            int dinhkem = 0;
            Non_QuyetDinhThanhLapKhac qd;
            foreach (QuyetDinhThanhLapKhac quyetDinh in qdList)
            {
                //Đính kèm
                dinhkem = quyetDinh.DinhKem == true ? 1 : 0;

                qd = new Non_QuyetDinhThanhLapKhac();
                //Non_QuyetDinh
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.NoiDung = quyetDinh.NoiDung;
                qd.CanCu = quyetDinh.CanCu;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.NamQuyetDinh = quyetDinh.NgayQuyetDinh.Year.ToString("####");
                qd.GhiChu = quyetDinh.GhiChu;
                //Non_QuyetDinhThanhLapKhac
                
                //master
                Non_ChiTietQuyetDinhThanhLapKhacMaster master = new Non_ChiTietQuyetDinhThanhLapKhacMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                master.VeViec = qd.NoiDung.ToUpper();

                //master 1
                Non_ChiTietQuyetDinhThanhLapKhacMaster master1 = new Non_ChiTietQuyetDinhThanhLapKhacMaster();
                master1.Oid = quyetDinh.Oid.ToString();
                master1.DonViChuQuan = qd.DonViChuQuan;
                master1.TenTruongVietHoa = qd.TenTruongVietHoa;
                master1.TenTruongVietThuong = qd.TenTruongVietThuong;
                master1.SoQuyetDinh = qd.SoQuyetDinh;
                master1.NguoiKy = qd.NguoiKy;
                master1.NgayQuyetDinh = qd.NgayQuyetDinh;
                master1.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                master1.VeViec = qd.NoiDung.ToUpper(); 
                
                qd.Master1.Add(master1);


                //detail
                Non_ChiTietQuyetDinhThanhLapKhacDetail detail;
                Non_ChiTietQuyetDinhThanhLapKhacBoPhanDetail detail1;
                int stt = 1;
                quyetDinh.ListChiTietThanhLapKhac_ToChuc.Sorting.Clear();
                quyetDinh.ListChiTietThanhLapKhac_ToChuc.Sorting.Add(new DevExpress.Xpo.SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                foreach (ChiTietThanhLapKhac_ToChuc tochuc in quyetDinh.ListChiTietThanhLapKhac_ToChuc)
                {
                    tochuc.ListChiTietThanhLapKhac_ThanhVien.Sorting.Clear();
                    DevExpress.Xpo.SortProperty[] sortList = new DevExpress.Xpo.SortProperty[3];
                    sortList[0] = new DevExpress.Xpo.SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    sortList[1] = new DevExpress.Xpo.SortProperty("ChucDanhHoiDong.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    sortList[2] = new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //
                    tochuc.ListChiTietThanhLapKhac_ThanhVien.Sorting.AddRange(sortList);

                    foreach (ChiTietThanhLapKhac_ThanhVien item in tochuc.ListChiTietThanhLapKhac_ThanhVien)
                    {
                        detail = new Non_ChiTietQuyetDinhThanhLapKhacDetail();
                        detail.STT = stt.ToString();
                        detail.HoTen = item.HoTenText;
                        detail.ChucVu = item.ChucVuText;
                        detail.ChucDanhHoiDong = item.ChucDanhHoiDong != null ? item.ChucDanhHoiDong.TenChucDanhHoiDong : "";
                        
                        qd.Detail.Add(detail);
                        stt++;
                    }

                    tochuc.ListChiTietThanhLapKhac_BoPhan.Sorting.Clear();
                    DevExpress.Xpo.SortProperty[] sortList1 = new DevExpress.Xpo.SortProperty[1];
                    sortList1[0] = new DevExpress.Xpo.SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //
                    tochuc.ListChiTietThanhLapKhac_BoPhan.Sorting.AddRange(sortList1);

                    foreach (ChiTietThanhLapKhac_BoPhan item in tochuc.ListChiTietThanhLapKhac_BoPhan)
                    {
                        detail1 = new Non_ChiTietQuyetDinhThanhLapKhacBoPhanDetail();
                        detail1.STT = stt.ToString();
                        detail1.DonVi = item.BoPhanText;

                        qd.Detail1.Add(detail1);
                    }
                }
                list.Add(qd);

                //Đưa chi tiết vào master
                master.TongNhanVien = stt - 1;
                qd.Master.Add(master);
            }

            if (dinhkem == 1)
            {
                MailMergeTemplate[] merge = new MailMergeTemplate[5];
                merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacThanhVienMaster_DinhKem.rtf"));
                merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacThanhVienDetail_DinhKem.rtf"));
                merge[3] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacBoPhanMaster_DinhKem.rtf"));
                merge[4] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacBoPhanDetail_DinhKem.rtf"));
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapKhac.rtf");
                if (merge[0] != null && merge[1] != null && merge[2] != null && merge[3] != null && merge[4] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapKhac>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập trong hệ thống.");
            }
            else
            {
                MailMergeTemplate[] merge = new MailMergeTemplate[5];
                merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacThanhVienMaster.rtf"));
                merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacThanhVienDetail.rtf"));
                merge[3] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacBoPhanMaster.rtf"));
                merge[4] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacBoPhanDetail.rtf"));
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapKhac.rtf");
                if (merge[0] != null && merge[1] != null && merge[2] != null && merge[3] != null && merge[4] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapKhac>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập trong hệ thống.");

                
                /*
                MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhThanhLapKhacNhieuNguoi.rtf");
                MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacNhieuNguoiMaster.rtf"));
                MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThanhLapKhacNhieuNguoiDetail.rtf"));
                MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhThanhLapKhac>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ thành lập hội đồng tuyển dụng trong hệ thống.");
                 */
            }
        }
    }
}
