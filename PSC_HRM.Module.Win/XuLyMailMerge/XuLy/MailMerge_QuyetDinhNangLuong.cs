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
    public class MailMerge_QuyetDinhNangLuong : IMailMerge<IList<QuyetDinhNangLuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNangLuong> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangLuong.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangLuong.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangLuong> qdList)
        {
            var list = new List<Non_QuyetDinhNangLuongCaNhan>();
            Non_QuyetDinhNangLuongCaNhan qd;
            MailMergeTemplate merge = null;
            foreach (QuyetDinhNangLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangLuongCaNhan();
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

                foreach (ChiTietQuyetDinhNangLuong item in quyetDinh.ListChiTietQuyetDinhNangLuong)
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
                    qd.NhomNgach = item.NgachLuong != null ? (item.NgachLuong.NhomNgachLuong != null ? item.NgachLuong.NhomNgachLuong.MaQuanLy : string.Empty) : string.Empty;
                    qd.TenNgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : string.Empty;
                    qd.MaNgachLuong = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";

                    qd.MaBacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.MaQuanLy : "";
                    qd.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    qd.HeSoLuongCu = item.HeSoLuongCu.ToString("N2");
                    qd.VuotKhungCu = item.VuotKhungCu.ToString();

                    qd.MaBacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.MaQuanLy : ""; 
                    qd.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    qd.HeSoLuongMoi = item.HeSoLuongMoi.ToString("N2");
                    qd.NgayHuongLuong = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.MocNangLuongMoi = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("'ngày' dd 'tháng' MM 'năm' yyyy") : "";
                    qd.VuotKhungMoi = item.VuotKhungMoi.ToString();
                    //
                    qd.NgayHuongLuongDate = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("dd/MM/yyyy") : "";
                    qd.MocNangLuongMoiDate = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("dd/MM/yyyy") : "";
                    qd.MocNangLuongCuDate = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("dd/MM/yyyy") : "";
                    //
                    qd.LyDo = item.LyDo;
                    qd.SoThang = item.SoThang.ToString();
                    //        
                    if (item.VuotKhungMoi != 0)
                    {
                        if (item.VuotKhungCu != 0 && TruongConfig.MaTruong.Equals("NEU"))
                            merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuongVuotKhungTu5PT.rtf");
                        else                        
                            merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuongVuotKhung.rtf");
                        
                        qd.LoaiNangLuong = "nâng phụ cấp thâm niên vượt khung";
                    }
                    else
                    {
                        if (item.NangLuongTruocHan == true)
                        {
                            qd.LoaiNangLuong = "nâng bậc lương trước hạn";
                            merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuongTruocHan.rtf");
                        }
                        else if (item.NangLuongTruocKhiNghiHuu == true)
                        {
                            qd.LoaiNangLuong = "nâng bậc lương trước khi nghỉ hưu";
                            merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuongTruocKhiNghiHuu.rtf");
                        }
                        else if (item.NangLuongTruocHan == false)
                        {
                            qd.LoaiNangLuong = "nâng bậc lương";
                            merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuong.rtf");
                        }
                    }
                }
                list.Add(qd);
            }

            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangLuongCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng lương trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangLuong> qdList)
        {
            var list = new List<Non_QuyetDinhNangLuong>();
            Non_QuyetDinhNangLuong qd;
            foreach (QuyetDinhNangLuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangLuong();
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
                Non_ChiTietQuyetDinhNangLuongMaster master = new Non_ChiTietQuyetDinhNangLuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhNangLuongDetail detail;
                quyetDinh.ListChiTietQuyetDinhNangLuong.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangLuong.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                
                int stt = 1, soLuongNangThuongXuyen=0, soLuongNangVuotKhung = 0, soLuongNangTruocThoiHan=0, soLuongNangTruocNghiHuu=0;                
                foreach (ChiTietQuyetDinhNangLuong item in quyetDinh.ListChiTietQuyetDinhNangLuong)
                {
                    detail = new Non_ChiTietQuyetDinhNangLuongDetail();
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

                        detail.SoLuongNangThuongXuyen = soLuongNangThuongXuyen.ToString("N0");
                        detail.SoLuongNangVuotKhung = soLuongNangVuotKhung.ToString("N0");
                        detail.SoLuongNangTruocHan = soLuongNangTruocThoiHan.ToString("N0");
                        detail.SoLuongNangTruocNghiHuu = soLuongNangTruocNghiHuu.ToString("N0");
                    }

                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.MaNgachLuong = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    detail.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    detail.BacLuongCu = item.BacLuongCu != null ? item.BacLuongCu.TenBacLuong : "";
                    detail.HeSoLuongCu = item.HeSoLuongCu.ToString("N2");
                    detail.VuotKhungCu = item.VuotKhungCu.ToString();
                    detail.BacLuongMoi = item.BacLuongMoi != null ? item.BacLuongMoi.TenBacLuong : "";
                    detail.HeSoLuongMoi = item.HeSoLuongMoi.ToString("N2");
                    detail.NgayHuongLuong = item.NgayHuongLuongMoi != DateTime.MinValue ? item.NgayHuongLuongMoi.ToString("d") : "";
                    detail.MocNangLuongMoi = item.MocNangLuongMoi != DateTime.MinValue ? item.MocNangLuongMoi.ToString("d") : "";
                    detail.MocNangLuongCu = item.MocNangLuongCu != DateTime.MinValue ? item.MocNangLuongCu.ToString("d") : "";
                    detail.VuotKhungMoi = item.VuotKhungMoi.ToString();
                    detail.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    detail.NamSinh = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");
                    if (item.ThongTinNhanVien.GioiTinh==GioiTinhEnum.Nu)
                        detail.NamSinhNu = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");
                    else                    
                        detail.NamSinhNam = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");
                   
                    if (item.VuotKhungMoi == 0)
                    {
                        if (item.NangLuongTruocHan == true)
                        {
                            detail.LoaiNangLuong = "CÁN BỘ VIÊN CHỨC ĐƯỢC NÂNG BẬC LƯƠNG TRƯỚC THỜI HẠN DO LẬP THÀNH TÍCH XUẤT SẮC TRONG THỰC HIỆN NHIỆM VỤ";
                            soLuongNangTruocThoiHan++;
                        }
                        else if (item.NangLuongTruocKhiNghiHuu == true)
                        {
                            detail.LoaiNangLuong = "CÁN BỘ VIÊN CHỨC ĐƯỢC NÂNG BẬC LƯƠNG TRƯỚC THỜI HẠN ĐỂ CHUẨN BỊ NGHỈ HƯU";
                            soLuongNangTruocNghiHuu++;
                        }
                        else
                        {
                            detail.LoaiNangLuong = "CÁN BỘ VIÊN CHỨC ĐƯỢC NÂNG BẬC LƯƠNG THƯỜNG XUYÊN";
                            soLuongNangThuongXuyen++;
                        }
                    }
                    else
                    {
                        detail.LoaiNangLuong = "CÁN BỘ VIÊN CHỨC ĐƯỢC TÍNH HƯỞNG THÊM PHỤ CẤP THÂM NIÊN VƯỢT KHUNG";
                        soLuongNangVuotKhung++;
                    }

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

            //MailMergeTemplate[] merge = new MailMergeTemplate[1];
              //merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangLuongTapTheMaster.rtf")); 
              //merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangLuongTapTheDetail.rtf")); 
            //merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuongTapThe.rtf");
            //if (merge[0] != null)
                //MailMergeHelper.ShowEditor<Non_QuyetDinhNangLuong>(list, obs, merge);
              //else
               //    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng lương trong hệ thống.");

            MailMergeTemplate[] merge1 = new MailMergeTemplate[3];
            merge1[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangLuongTapTheMaster.rtf")); ;
            merge1[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangLuongTapTheDetail.rtf")); ;
            merge1[0] = HamDungChung.GetTemplate(obs, "QuyetDinhNangLuongTapThe1.rtf");
            if (merge1[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangLuong>(list, obs, merge1);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng lương trong hệ thống.");          
        }
    }
}
