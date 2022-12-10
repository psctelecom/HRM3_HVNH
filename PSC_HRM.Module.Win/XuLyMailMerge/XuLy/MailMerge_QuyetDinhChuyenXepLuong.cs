using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhChuyenXepLuong : IMailMerge<IList<QuyetDinhChuyenXepLuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChuyenXepLuong> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhchuyenXepLuong.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhchuyenXepLuong.Count > 1
                         select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChuyenXepLuong> qdList)
        {
            var list = new List<Non_QuyetDinhChuyenXepLuongCaNhan>();
            Non_QuyetDinhChuyenXepLuongCaNhan qd;
            MailMergeTemplate merge = null;
            foreach (QuyetDinhChuyenXepLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChuyenXepLuongCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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

                qd.NgayHopHoiDongLuongDate = quyetDinh.NgayHopHoiDongLuong.ToString("d");
                qd.NamQuyetDinh = quyetDinh.NgayHieuLuc.ToString("yyyy");
                switch (quyetDinh.NgayHieuLuc.Month)
                {
                    case 1: case 2: case 3:
                        qd.QuyQuyetDinh = "I";
                        break;
                    case 4: case 5: case 6:
                        qd.QuyQuyetDinh = "II";
                        break;
                    case 7: case 8: case 9:
                        qd.QuyQuyetDinh = "III";
                        break;
                    case 10: case 11: case 12:
                        qd.QuyQuyetDinh = "IV";
                        break;
                }

                foreach (ChiTietQuyetDinhChuyenXepLuong item in quyetDinh.ListChiTietQuyetDinhchuyenXepLuong)
                {
                    qd.ChucVu = HamDungChung.GetChucVuNhanVien(item.ThongTinNhanVien);
                    qd.ChucVuTruongDonVi = HamDungChung.GetChucVuCaoNhatTrongDonVi(item.ThongTinNhanVien.BoPhan);
                    qd.ChucVuNhanVienVietHoa = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    if (TruongConfig.MaTruong == "QNU")
                    {
                        qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.TenChucVu : 
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    }

                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("d") : "";


                    qd.MucLuongCu = item.MucLuongCu.ToString("N0");
                    qd.MucLuongMoi = item.MucLuongMoi.ToString("N0");
                    qd.NgayHuongLuong = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    
                    qd.LyDo = item.LyDo;

                }
                list.Add(qd);
            }
                merge = HamDungChung.GetTemplate(obs, "QuyetDinhChuyenXepLuong.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChuyenXepLuongCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chuyển xếp lương trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChuyenXepLuong> qdList)
        {
            var list = new List<Non_QuyetDinhChuyenXepLuong>();
            Non_QuyetDinhChuyenXepLuong qd;
            foreach (QuyetDinhChuyenXepLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChuyenXepLuong();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
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
                qd.NamQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("yyyy");
                qd.NgayHopHoiDongLuongDate = quyetDinh.NgayHopHoiDongLuong.ToString("d");

                //master
                Non_ChiTietQuyetDinhChuyenXepLuongMaster master = new Non_ChiTietQuyetDinhChuyenXepLuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhChuyenXepLuongDetail detail;
                quyetDinh.ListChiTietQuyetDinhchuyenXepLuong.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhchuyenXepLuong.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                
                int stt = 1, soLuongNangThuongXuyen=0, soLuongNangVuotKhung = 0, soLuongNangTruocThoiHan=0, soLuongNangTruocNghiHuu=0;                
                foreach (ChiTietQuyetDinhChuyenXepLuong item in quyetDinh.ListChiTietQuyetDinhchuyenXepLuong)
                {
                    detail = new Non_ChiTietQuyetDinhChuyenXepLuongDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    
                    //NEU
                    if (TruongConfig.MaTruong == "NEU")
                    {
                        detail.DonViChuQuan = qd.DonViChuQuan;
                        detail.TenTruongVietHoa = qd.TenTruongVietHoa;
                        detail.TenTruongVietThuong = qd.TenTruongVietThuong;
                        detail.SoQuyetDinh = qd.SoQuyetDinh;
                        detail.SoPhieuTrinh = qd.SoPhieuTrinh;
                        detail.NgayPhieuTrinh = quyetDinh.NgayPhieuTrinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                        detail.SoBanSao = quyetDinh.SoBanSao;
                        detail.NgayQuyetDinh = qd.NgayQuyetDinh;
                        detail.NgayHieuLuc = qd.NgayHieuLuc;
                        detail.CanCu = qd.CanCu;
                        detail.NoiDung = qd.NoiDung;
                        detail.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                        detail.ChucDanhNguoiKy = qd.ChucDanhNguoiKy;
                        detail.NguoiKy = qd.NguoiKy;
                        detail.NamQuyetDinh = qd.NamQuyetDinh;
                        detail.NgayHopHoiDongLuongDate = qd.NamQuyetDinh;
                        detail.ChucDanhNguoiKyBanSao = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKyBanSao);
                        detail.ChucVuNguoiKyBanSao = quyetDinh.ChucVuNguoiKyBanSao != null ? quyetDinh.ChucVuNguoiKyBanSao.TenChucVu.ToUpper() + "TCCB" : "";
                        detail.NguoiKyBanSao = quyetDinh.NguoiKyBanSao != null ? quyetDinh.NguoiKyBanSao.HoTen : "";

                    }

                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    detail.NamSinh = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");
                    detail.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    detail.MucLuongCu = item.MucLuongCu.ToString("N0");
                    detail.MucLuongMoi = item.MucLuongMoi.ToString("N0");
                    detail.NgayHuongLuong = item.NgayHuongLuongMoi.ToString("dd/MM/yyyy");
                    detail.ViTriViecLam = item.ThongTinNhanVien.CongViecHienNay != null ? item.ThongTinNhanVien.CongViecHienNay.TenCongViec : "";
                    if (item.ThongTinNhanVien.GioiTinh==GioiTinhEnum.Nu)
                        detail.NamSinhNu = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");
                    else                    
                        detail.NamSinhNam = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");

                    qd.Detail.Add(detail);
                    stt++;                    
                }               

                qd.SoLuongCanBo = (stt-1).ToString("N0");
                qd.SoLuongNangThuongXuyen = soLuongNangThuongXuyen.ToString("N0");
                qd.SoLuongNangVuotKhung = soLuongNangVuotKhung.ToString("N0");
                qd.SoLuongNangTruocHan = soLuongNangTruocThoiHan.ToString("N0");
                qd.SoLuongNangTruocNghiHuu = soLuongNangTruocNghiHuu.ToString("N0");

                list.Add(qd);               
            }

            MailMergeTemplate[] merge1 = new MailMergeTemplate[3];
            merge1[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChuyenXepLuongTapTheMaster.rtf")); ;
            merge1[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChuyenXepLuongTapTheDetail.rtf")); ;
            merge1[0] = HamDungChung.GetTemplate(obs, "QuyetDinhChuyenXepLuongTapThe.rtf");
            if (merge1[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChuyenXepLuong>(list, obs, merge1);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng lương trong hệ thống.");          
        }
    }
}
