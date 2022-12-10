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
    public class MailMerge_QuyetDinhChiTienThuongTienSi
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhChiTienThuongTienSi> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietChiTienThuongTienSi.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietChiTienThuongTienSi.Count > 1
                         select qd;

            if (caNhan.Count() > 0 )
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0)
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChiTienThuongTienSi> qdList)
        {
            var list = new List<Non_QuyetDinhChiTienThuongTienSiCaNhan>();
            Non_QuyetDinhChiTienThuongTienSiCaNhan qd;
            foreach (QuyetDinhChiTienThuongTienSi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChiTienThuongTienSiCaNhan();
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
                qd.SoTien = String.Format("{0:n0}", quyetDinh.PhuCapTienSi); ;
                qd.SoTienBangChu = HamDungChung.DocTien(quyetDinh.PhuCapTienSi);
                qd.HocVi = "Tiến sĩ";

                foreach (ChiTietChiTienThuongTienSi item in quyetDinh.ListChiTietChiTienThuongTienSi)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    if(item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                    qd.DanhXungVietThuong = "ông";
                    else
                    qd.DanhXungVietThuong = "bà";
                    if (item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        qd.DanhXungVietHoa = "Ông";
                    else
                        qd.DanhXungVietHoa = "Bà";

                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhChiTienThuongTienSi.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChiTienThuongTienSiCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chi tiền thưởng tiến sĩ trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhChiTienThuongTienSi> qdList)
        {
            var list = new List<Non_QuyetDinhChiTienThuongTienSi>();
            Non_QuyetDinhChiTienThuongTienSi qd;
            foreach (QuyetDinhChiTienThuongTienSi quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhChiTienThuongTienSi();
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
                qd.SoTien = String.Format("{0:n0}", quyetDinh.PhuCapTienSi); ;
                qd.SoTienBangChu = HamDungChung.DocTien(quyetDinh.PhuCapTienSi);
                qd.HocVi = "Tiến sĩ";

                //master
                Non_ChiTietQuyetDinhChiTienThuongTienSiMaster master = new Non_ChiTietQuyetDinhChiTienThuongTienSiMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhChiTienThuongTienSiDetail detail;
                quyetDinh.ListChiTietChiTienThuongTienSi.Sorting.Clear();
                quyetDinh.ListChiTietChiTienThuongTienSi.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                decimal tongTien = 0;
                foreach (ChiTietChiTienThuongTienSi item in quyetDinh.ListChiTietChiTienThuongTienSi)
                {
                    detail = new Non_ChiTietQuyetDinhChiTienThuongTienSiDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.TrinhDo = "Tiến sĩ";
                    detail.ChuyenNganhDaoTao = item.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null ? item.ThongTinNhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.TenChuyenMonDaoTao : string.Empty;
                    detail.NoiDaoTao = item.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao != null ? item.ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao.TenTruongDaoTao : string.Empty;
                    detail.NgayCapBang = item.ThongTinNhanVien.NhanVienTrinhDo.NgayCapBang != DateTime.MinValue ? String.Format("{0:dd/MM/yyyy}", item.ThongTinNhanVien.NhanVienTrinhDo.NgayCapBang) : string.Empty ;
                    detail.NgayTroLaiCongTac = item.ThongTinNhanVien.NhanVienTrinhDo.NgayCapBang != DateTime.MinValue ? String.Format("{0:dd/MM/yyyy}", item.ThongTinNhanVien.NhanVienTrinhDo.NgayCongTac) : string.Empty;
                    detail.SoTien = String.Format("{0:n0}", quyetDinh.PhuCapTienSi);
                    detail.SoTienBangChu = HamDungChung.DocTien(quyetDinh.PhuCapTienSi);
                    //
                    tongTien += quyetDinh.PhuCapTienSi;
                    //
                    qd.Detail.Add(detail);
                    stt++;
                }

                master.SoTien = String.Format("{0:N0}", tongTien);
                master.SoTienBangChu = HamDungChung.DocTien(tongTien);

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChiTienThuongTienSiMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhChiTienThuongTienSiDetail.rtf")); ;
            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhChiTienThuongTienSiTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhChiTienThuongTienSi>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ chi tiền thưởng tiến sĩ trong hệ thống.");
        }
    }
}
