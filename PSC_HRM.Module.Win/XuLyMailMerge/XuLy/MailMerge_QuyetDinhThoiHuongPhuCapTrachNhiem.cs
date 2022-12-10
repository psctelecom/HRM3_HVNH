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
    public class MailMerge_QuyetDinhThoiHuongPhuCapTrachNhiem : IMailMerge<IList<QuyetDinhThoiHuongPhuCapTrachNhiem>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhThoiHuongPhuCapTrachNhiem> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem.Count > 1
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

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhThoiHuongPhuCapTrachNhiem> qdList)
        {
            var list = new List<Non_QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan>();
            Non_QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan qd;
            foreach (QuyetDinhThoiHuongPhuCapTrachNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan();
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

                //Non_QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan

                foreach (ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem item in quyetDinh.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem)
                {
                    //Non_QuyetDinhCaNhan
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.NgaySinh = item.ThongTinNhanVien.NgaySinh.ToString("dd/MM/yyyy");
                    qd.MaNgachLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy : "";
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
                    qd.NgayThoiHuongHSPCTrachNhiemCu = item.NgayThoiHuongHSPCTrachNhiemCu.ToString("dd/MM/yyyy");
                    qd.HSPCTrachNhiemCu = item.HSPCTrachNhiemCu.ToString();
                    qd.LyDo = item.LyDo;
                    qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
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
            
            MailMergeTemplate merge;
            merge = HamDungChung.GetTemplate(obs, "QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThoiHuongPhuCapTrachNhiemCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng phụ cấp trách nhiệm.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhThoiHuongPhuCapTrachNhiem> qdList)
        {
            var list = new List<Non_QuyetDinhThoiHuongPhuCapTrachNhiem>();
            Non_QuyetDinhThoiHuongPhuCapTrachNhiem qd;
            foreach (QuyetDinhThoiHuongPhuCapTrachNhiem quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhThoiHuongPhuCapTrachNhiem();
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

                //master
                Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemMaster master = new Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.SoNguoi = quyetDinh.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem.Count.ToString();
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;

                qd.Master.Add(master);
             

                //detail
                Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemDetail detail;
                quyetDinh.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem item in quyetDinh.ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem)
                {
                    detail = new Non_ChiTietQuyetDinhThoiHuongPhuCapTrachNhiemDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);

                    detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        detail.ChucVu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.TenChucVu :
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    }


                    qd.Detail.Add(detail);
                    stt++;
                }

                list.Add(qd);
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThoiHuongPhuCapTrachNhiemTapTheMaster.rtf")); ;
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhThoiHuongPhuCapTrachNhiemTapTheDetail.rtf")); ;

            merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhThoiHuongPhuCapTrachNhiemTapThe.rtf");
            if (merge[0] != null && merge[1] != null && merge[2] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhThoiHuongPhuCapTrachNhiem>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ hưởng phụ cấp trách nhiệm nhiều người.");
            
        }
    }
}
