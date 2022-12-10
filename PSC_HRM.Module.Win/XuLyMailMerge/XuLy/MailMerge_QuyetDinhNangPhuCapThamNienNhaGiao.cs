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
    public class MailMerge_QuyetDinhNangPhuCapThamNienNhaGiao : IMailMerge<IList<QuyetDinhNangPhuCapThamNienNhaGiao>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhNangPhuCapThamNienNhaGiao> qdList)
        {
            var caNhan = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Count == 1
                         select qd;

            var tapThe = from qd in qdList
                         where qd.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Count > 1
                         select qd;

            if (caNhan.Count() > 0)
            {
                QuyetDinhCaNhan(obs, caNhan.ToList());
            }
            if (tapThe.Count() > 0 && !TruongConfig.MaTruong.Equals("QNU"))
            {
                QuyetDinhTapThe(obs, tapThe.ToList());
            }
            if (tapThe.Count() > 0 && TruongConfig.MaTruong.Equals("QNU"))
            {
                QuyetDinhTapThe_New(obs, tapThe.ToList());
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangPhuCapThamNienNhaGiao> qdList)
        {
            var list = new List<Non_QuyetDinhNangPhuCapThamNienNhaGiaoCaNhan>();
            Non_QuyetDinhNangPhuCapThamNienNhaGiaoCaNhan qd;
            foreach (QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangPhuCapThamNienNhaGiaoCaNhan();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan != null ? quyetDinh.TenCoQuan.ToUpper() : quyetDinh.ThongTinTruong.TenVietHoa;
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("d");
                qd.CanCu = quyetDinh.CanCu;
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;

                foreach (ChiTietQuyetDinhNangPhuCapThamNienNhaGiao item in quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    qd.MaNgachLuong = item.NgachLuong != null ? item.NgachLuong.MaQuanLy : "";
                    qd.NgachLuong = item.NgachLuong != null ? item.NgachLuong.TenNgachLuong : "";
                    qd.BacLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong.TenBacLuong;
                    qd.HeSoLuong = item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong.ToString("N2");
                    qd.ThamNienCu = item.ThamNienCu.ToString("N0");
                    qd.ThamNienMoi = item.ThamNienMoi.ToString("N0");
                    qd.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi.ToString("d");
                    if (TruongConfig.MaTruong == "QNU")
                    {
                        qd.ChucVu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.TenChucVu :
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    }
                }
                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangPhuCapThamNienNhaGiao.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangPhuCapThamNienNhaGiaoCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng thâm niên nhà giáo trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangPhuCapThamNienNhaGiao> qdList)
        {
            var list = new List<Non_QuyetDinhNangPhuCapThamNienNhaGiao>();
            Non_QuyetDinhNangPhuCapThamNienNhaGiao qd;
            foreach (QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangPhuCapThamNienNhaGiao();
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
                qd.NgayKy = DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                qd.SoLuongCanBo = (from x in quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao
                                   select x).Count().ToString("N0");
                qd.SoGiangVienDuocCongNhan = (from x in quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao
                                              where x.ThamNienMoi == 5
                                              select x).Count().ToString("N0");
                qd.SoGiangVienDuocNangThamNien = (from x in quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao
                                                  where x.ThamNienMoi > 5
                                                  select x).Count().ToString("N0");
                qd.TuThang = "1/" + quyetDinh.NgayHieuLuc.Year;
                qd.DenThang = "12/" + quyetDinh.NgayHieuLuc.Year;
                qd.NamQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'năm' yyyy");

                DateTime dauNamTruoc = HamDungChung.GetServerTime().AddYears(-1).SetTime(SetTimeEnum.StartYear);
                DateTime cuoiNamTruoc = dauNamTruoc.SetTime(SetTimeEnum.EndYear);
                QuyetDinhNangPhuCapThamNienNhaGiao qdNamTruoc = obs.FindObject<QuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("NgayHieuLuc>=? and NgayHieuLuc<=?", dauNamTruoc, cuoiNamTruoc));
                if (qdNamTruoc != null)
                    qd.QuyetDinhNangThamNienNamTruoc = qdNamTruoc.SoQuyetDinh + qdNamTruoc.NgayQuyetDinh.ToString(" 'ngày' dd 'tháng' MM 'năm' yyyy");
                else
                    qd.QuyetDinhNangThamNienNamTruoc = "783/QĐ-ĐHL ngày 15 tháng 5 năm 2013";

                //master
                Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMaster master = new Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NamQuyetDinh = qd.NamQuyetDinh.ToString();
                qd.Master.Add(master);

                //master 1
                Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiMaster master1 = new Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiMaster();
                master.Oid = quyetDinh.Oid.ToString();
                qd.Master1.Add(master1);

                //detail
                Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoDetail detail;
                Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiDetail detail1;
                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Add(new DevExpress.Xpo.SortProperty("ThongTinNhanVien.Ten", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1, stt1 = 1;
                foreach (ChiTietQuyetDinhNangPhuCapThamNienNhaGiao item in quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao)
                {
                    if (item.ThamNienMoi > 5)
                    {
                        detail = new Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoDetail();
                        detail.Oid = quyetDinh.Oid.ToString();
                        detail.STT = stt.ToString();
                        detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                        detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                        detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                        detail.HoTen = item.ThongTinNhanVien.HoTen;
                        detail.GioiTinh = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                        detail.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("d") : "";
                        detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                        detail.ThamNienCu = item.ThamNienCu.ToString("N0");
                        detail.NgayHuongThamNienCu = item.NgayHuongThamNienCu.ToString("d");
                        detail.ThamNienMoi = item.ThamNienMoi.ToString("N0");
                        detail.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi.ToString("d");
                        detail.MaNgachLuong = item.NgachLuong.MaQuanLy.ToString();
                        detail.NgachLuong = item.NgachLuong.TenNgachLuong.ToString();

                        qd.Detail.Add(detail);
                        stt++;
                    }
                    else
                    {
                        detail1 = new Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiDetail();
                        detail1.Oid = quyetDinh.Oid.ToString();
                        detail1.STT = stt1.ToString();
                        detail1.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                        detail1.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                        detail1.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                        detail1.HoTen = item.ThongTinNhanVien.HoTen;
                        detail1.GioiTinh = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                        detail1.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("d") : "";
                        detail1.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                        detail1.NgayTinhThamNien = item.ThongTinNhanVien.NgayTinhThamNienNhaGiao.ToString("d");
                        detail1.ThamNienMoi = item.ThamNienMoi.ToString("N0");
                        detail1.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi.ToString("d");
                        detail1.MaNgachLuong = item.NgachLuong.MaQuanLy.ToString();
                        detail1.NgachLuong = item.NgachLuong.TenNgachLuong.ToString();

                        qd.Detail1.Add(detail1);
                        stt1++;
                    }
                }

                list.Add(qd);
            }


            if (TruongConfig.MaTruong == "UEL")
            {
                MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhNangPhuCapThamNienNhaGiaoTapThe.rtf");
                if (merge != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhNangPhuCapThamNienNhaGiao>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng thâm niên nhà giáo trong hệ thống.");
            }
            else
            {
                MailMergeTemplate[] merge = new MailMergeTemplate[5];
                merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangPhuCapThamNienNhaGiaoTapTheMaster.rtf"));
                merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangPhuCapThamNienNhaGiaoTapTheDetail.rtf"));
                merge[3] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangPhuCapThamNienNhaGiaoTapThe1Master.rtf"));
                merge[4] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangPhuCapThamNienNhaGiaoTapThe1Detail.rtf"));
                merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhNangPhuCapThamNienNhaGiaoTapThe.rtf");
                if (merge[0] != null)
                    MailMergeHelper.ShowEditor<Non_QuyetDinhNangPhuCapThamNienNhaGiao>(list, obs, merge);
                else
                    HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng thâm niên nhà giáo trong hệ thống.");
            }
        }

        //1 QĐ 1 người nhiều đợt nâng thâm niên nhà giáo
        private void QuyetDinhTapThe_New(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhNangPhuCapThamNienNhaGiao> qdList)
        {
            var list = new List<Non_QuyetDinhNangPhuCapThamNienNhaGiao>();
            Non_QuyetDinhNangPhuCapThamNienNhaGiao qd;
            foreach (QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhNangPhuCapThamNienNhaGiao();
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

                //  
                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Add(new DevExpress.Xpo.SortProperty("ThamNienMoi", DevExpress.Xpo.DB.SortingDirection.Descending));
                qd.DanhXungVietThuong = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.DanhXungVietHoa = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DonVi = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].BoPhan.TenBoPhan;
                qd.HoTen = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.HoTen;
                if (TruongConfig.MaTruong == "QNU")
                {
                    qd.ChucVu = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.ChucVu != null ?
                                    quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.ChucVu.TenChucVu :
                                    quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.ChucDanh != null ?
                                        quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                }
                qd.NgayHuongThamNienMoi = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].NgayHuongThamNienMoi.ToString("d");
                qd.TenNgach = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong;
                qd.MaNgach = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy;

                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Add(new DevExpress.Xpo.SortProperty("ThamNienMoi", DevExpress.Xpo.DB.SortingDirection.Ascending));
                qd.ThamNienCu = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].ThamNienCu.ToString("N0");
                qd.NgayHuongThamNienCu = quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[0].NgayHuongThamNienCu.ToString("d");

                //master
                Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMaster master = new Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                qd.Master.Add(master);

                //detail
                Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoDetail detail;
                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Clear();
                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Sorting.Add(new DevExpress.Xpo.SortProperty("ThamNienMoi", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietQuyetDinhNangPhuCapThamNienNhaGiao item in quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao)
                {
                    detail = new Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.HoTen = item.ThongTinNhanVien.HoTen;
                    detail.GioiTinh = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Nam" : "Nữ";
                    detail.NgaySinh = item.ThongTinNhanVien.NgaySinh != DateTime.MinValue ? item.ThongTinNhanVien.NgaySinh.ToString("d") : "";
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ThamNienCu = item.ThamNienCu.ToString("N0");
                    detail.NgayHuongThamNienCu = item.NgayHuongThamNienCu.ToString("d");
                    detail.ThamNienMoi = item.ThamNienMoi.ToString("N0");
                    detail.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi.ToString("d");
                    if (TruongConfig.MaTruong == "QNU")
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
            merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangPhuCapThamNienNhaGiaoTapTheMaster.rtf"));
            merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhNangPhuCapThamNienNhaGiaoTapTheDetail.rtf"));
              merge[0] = HamDungChung.GetTemplate(obs, "QuyetDinhNangPhuCapThamNienNhaGiaoTapThe.rtf");
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhNangPhuCapThamNienNhaGiao>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ nâng thâm niên nhà giáo trong hệ thống.");
        }
    }
}
