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
    public class MailMerge_QuyetDinhNangThamNienTangThem : IMailMerge<IList<QuyetDinhNangThamNienTangThem>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNangThamNienTangThem> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangThamNienTangThem.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangThamNienTangThem.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangThamNienTangThem> qdList)
        {
            var list = new List<Non_QuyetDinhNangThamNienTangThemCaNhan>();
            Non_QuyetDinhNangThamNienTangThemCaNhan qd;
            foreach (QuyetDinhNangThamNienTangThem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangThamNienTangThemCaNhan();
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

                foreach (ChiTietQuyetDinhNangThamNienTangThem item in quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaNgachLuong = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.HSLTangThemTheoThamNienCu = item.HSLTangThemTheoThamNienCu.HeSoPhuCap.ToString("N2");
                    qd.HSLTangThemTheoThamNienMoi = item.HSLTangThemTheoThamNienMoi.HeSoPhuCap.ToString("N2");
                    qd.MocHuongThamNienTangThemMoi = item.MocHuongThamNienTangThemMoi.ToString("d");
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.TenChucVu :
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    }
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangThamNienTangThemCaNhan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangThamNienTangThemCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng thâm niên tăng thêm trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangThamNienTangThem> qdList)
        {
            var list = new List<Non_QuyetDinhNangThamNienTangThem>();
            Non_QuyetDinhNangThamNienTangThem qd;
            foreach (QuyetDinhNangThamNienTangThem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangThamNienTangThem();
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
                qd.SoLuongCanBo = (from x in quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem
                                   select x).Count().ToString("N0");
                qd.Nam = quyetDinh.NgayQuyetDinh.Year.ToString("####");

                //master
                Non_ChiTietQuyetDinhNangThamNienTangThemMaster master = new Non_ChiTietQuyetDinhNangThamNienTangThemMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);
                                
                //detail
                Non_ChiTietQuyetDinhNangThamNienTangThemDetail detail;
                quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhNangThamNienTangThem item in quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem)
                {
                    detail = new Non_ChiTietQuyetDinhNangThamNienTangThemDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.GioiTinh = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                    detail.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("d") : "";
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.HSLTangThemTheoThamNienCu = item.HSLTangThemTheoThamNienCu.HeSoPhuCap.ToString("N2");
                    detail.HSLTangThemTheoThamNienMoi = item.HSLTangThemTheoThamNienMoi.HeSoPhuCap.ToString("N2");
                    detail.MocHuongThamNienTangThemCu = item.MocHuongThamNienTangThemCu.ToString("d");
                    detail.MocHuongThamNienTangThemMoi = item.MocHuongThamNienTangThemMoi.ToString("d");
                    detail.MaNgachLuong = item.NgachLuong.MaQuanLy.ToString();
                    detail.NgachLuong = item.NgachLuong.TenNgachLuong.ToString();

                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangThamNienTangThemTapTheMaster.rtf"));
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangThamNienTangThemTapTheDetail.rtf"));
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhNangThamNienTangThemTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangThamNienTangThem>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng thâm niên tăng thêm trong hệ thống.");
        }
    }
}
