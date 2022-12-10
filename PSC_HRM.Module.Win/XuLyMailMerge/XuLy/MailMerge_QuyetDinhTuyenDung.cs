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
    public class MailMerge_QuyetDinhTuyenDung : IMailMerge<IList<QuyetDinhTuyenDung>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTuyenDung> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhTuyenDung.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhTuyenDung.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTuyenDung> qdList)
        {
            var list = new List<Non_QuyetDinhTuyenDungCaNhan>();
            Non_QuyetDinhTuyenDungCaNhan qd;
            foreach (QuyetDinhTuyenDung quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTuyenDungCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("d");
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("d");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                if (quyetDinh.QuanLyTuyenDung != null)
                {
                    qd.DotTuyenDung = quyetDinh.QuanLyTuyenDung.DotTuyenDung;
                    qd.NamTuyenDung = quyetDinh.QuanLyTuyenDung.NamHoc.TenNamHoc;
                }
              
                foreach (ChiTietQuyetDinhTuyenDung item in quyetDinh.ListChiTietQuyetDinhTuyenDung)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NgaySinhDate = item.ThongTinNhanVien.NgaySinh.ToString("d");
                    qd.NamSinh = item.ThongTinNhanVien.NgaySinh.ToString("yyyy");
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    qd.ChuyenMonDaoTao = item.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? item.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
                    qd.MaNgach = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    qd.BacLuong = item.BacLuong != null ? item.BacLuong.MaQuanLy : "";
                    qd.HeSoLuong = item.HeSoLuong.ToString("N2");
                    qd.Huong85PhanTramMucLuong = item.Huong85PhanTramLuong ? "(85%)" : "(100%)";
                    qd.NgayHuongLuong = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("d") : "";
                    qd.ThoiGianTapSu = item.ThoiGianTapSu.ToString("N0");
                    break;
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTuyenDung.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTuyenDungCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tuyển dụng trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhTuyenDung> qdList)
        {
            var list = new List<Non_QuyetDinhTuyenDung>();
            Non_QuyetDinhTuyenDung qd;
            foreach (QuyetDinhTuyenDung quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTuyenDung();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : "";
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
                if (quyetDinh.QuanLyTuyenDung != null)
                {
                    qd.DotTuyenDung = quyetDinh.QuanLyTuyenDung.DotTuyenDung;
                    qd.NamTuyenDung = quyetDinh.QuanLyTuyenDung.NamHoc.TenNamHoc;
                }              
                qd.SoLuongCanBo = quyetDinh.ListChiTietQuyetDinhTuyenDung.Count.ToString();

                //master
                Non_ChiTietQuyetDinhTuyenDungMaster master = new Non_ChiTietQuyetDinhTuyenDungMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DotTuyenDung = qd.DotTuyenDung;
                master.NamTuyenDung = qd.NamTuyenDung;
                
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhTuyenDungDetail detail;
                quyetDinh.ListChiTietQuyetDinhTuyenDung.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhTuyenDung.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhTuyenDung item in quyetDinh.ListChiTietQuyetDinhTuyenDung)
                {
                    detail = new Non_ChiTietQuyetDinhTuyenDungDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = item.BoPhan.TenBoPhan;
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        detail.DonVi = item.BoPhan.MaQuanLy;    
                    }
                    detail.MaNgach = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    detail.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    detail.BacLuong = item.BacLuong != null ? item.BacLuong.MaQuanLy : "";
                    detail.HeSoLuong = item.HeSoLuong.ToString("N2");
                    detail.Huong85PhanTramMucLuong = item.Huong85PhanTramLuong ? "(85%)" : "(100%)";
                    detail.NgayHuongLuong = item.NgayHuongLuong != DateTime.MinValue ? item.NgayHuongLuong.ToString("d") : "";
                    detail.ThoiGianTapSu = item.ThoiGianTapSu.ToString("N0");
                    detail.NamSinh = item.ThongTinNhanVien.NgaySinh.Year.ToString();
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nu)
                        detail.GioiTinh = "Nữ";
                    else
                        detail.GioiTinh = "Nam";
                    
                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTuyenDungTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhTuyenDungTapTheDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhTuyenDungTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTuyenDung>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tuyển dụng trong hệ thống.");
        }
    }
}
