using DevExpress.Data.Filtering;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhHuongPhuCapUuDai : IMailMerge<IList<QuyetDinhHuongPhuCapUuDai>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhHuongPhuCapUuDai> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietHuongPhuCapUuDai.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietHuongPhuCapUuDai.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhHuongPhuCapUuDai> qdList)
        {
            var list = new List<Non_QuyetDinhHuongPhuCapUuDaiCaNhan>();
            Non_QuyetDinhHuongPhuCapUuDaiCaNhan qd;
            foreach (QuyetDinhHuongPhuCapUuDai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuongPhuCapUuDaiCaNhan();
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

                //Non_QuyetDinhHuongPhuCapUuDaiCaNhan
                qd.Nam = quyetDinh.Nam.ToString("####");

                foreach (ChiTietHuongPhuCapUuDai item in quyetDinh.ListChiTietHuongPhuCapUuDai)
                {
                    //Non_QuyetDinhCaNhan
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "";
                    qd.SoHieuCongChuc = item.ThongTinNhanVien.SoHieuCongChuc;
                    qd.SoHoSo = item.ThongTinNhanVien.SoHoSo;
                    qd.TrinhDoChuyenMon = item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietThuong = "ông";
                    else
                        qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";

                    qd.PhuCapUuDai = item.PhuCapUuDai.ToString("N0");

                    list.Add(qd);
                    break;
                }
            }

            MailMergeTemplate merge;
            merge = HamDungChung.GetTemplate(obs, "QuyetDinhHuongPhuCapUuDaiCaNhan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhHuongPhuCapUuDaiCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng phụ cấp ưu đãi trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhHuongPhuCapUuDai> qdList)
        {
            var list = new List<Non_QuyetDinhHuongPhuCapUuDai>();
            Non_QuyetDinhHuongPhuCapUuDai qd;
            foreach (QuyetDinhHuongPhuCapUuDai quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhHuongPhuCapUuDai();
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

                qd.SoLuongCanBo = quyetDinh.ListChiTietHuongPhuCapUuDai.Count.ToString();
                qd.Nam = quyetDinh.Nam.ToString("####");

                qd.Muc25 = (from x in quyetDinh.ListChiTietHuongPhuCapUuDai
                            where x.PhuCapUuDaiMoi == 25
                            select x).Count().ToString("N0");
                qd.Muc40 = (from x in quyetDinh.ListChiTietHuongPhuCapUuDai
                            where x.PhuCapUuDaiMoi == 40
                            select x).Count().ToString("N0");
                qd.Muc45 = (from x in quyetDinh.ListChiTietHuongPhuCapUuDai
                            where x.PhuCapUuDaiMoi == 45
                            select x).Count().ToString("N0");

                //master
                Non_ChiTietHuongPhuCapUuDaiMaster master = new Non_ChiTietHuongPhuCapUuDaiMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.SoNguoi = quyetDinh.ListChiTietHuongPhuCapUuDai.Count.ToString();
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;
                qd.Master.Add(master);

                //detail
                Non_ChiTietHuongPhuCapUuDaiDetail detail;
                quyetDinh.ListChiTietHuongPhuCapUuDai.Sorting.Clear();
                quyetDinh.ListChiTietHuongPhuCapUuDai.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietHuongPhuCapUuDai item in quyetDinh.ListChiTietHuongPhuCapUuDai)
                {
                    detail = new Non_ChiTietHuongPhuCapUuDaiDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.PhuCapUuDai = item.PhuCapUuDai.ToString("N0");

                    qd.Detail.Add(detail);
                    stt++;
                }
                list.Add(qd);
              
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhHuongPhuCapUuDaiTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhHuongPhuCapUuDaiTapTheDetail.rtf")); ;

            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhHuongPhuCapUuDaiTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhHuongPhuCapUuDai>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng phụ cấp ưu đãi trong hệ thống.");
        }
    }
}
