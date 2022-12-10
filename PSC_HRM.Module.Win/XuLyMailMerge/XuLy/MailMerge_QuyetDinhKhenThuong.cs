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
    public class MailMerge_QuyetDinhKhenThuong : IMailMerge<IList<QuyetDinhKhenThuong>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhKhenThuong> qdList)
        {
            var list1 = from q in qdList
                        where q.ListChiTietKhenThuongBoPhan.Count > 0 
                            && q.ListChiTietKhenThuongNhanVien.Count > 0
                        select q;
            var list2 = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count > 1
                            && q.ListChiTietKhenThuongBoPhan.Count == 0
                        select q;
            var list3 = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count == 0
                            && q.ListChiTietKhenThuongBoPhan.Count > 1
                        select q;
            var list4 = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count == 1
                            && q.ListChiTietKhenThuongBoPhan.Count == 0
                        select q;
            var list5 = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count == 0
                            && q.ListChiTietKhenThuongBoPhan.Count == 1
                        select q;

            if (list1.Count() > 0)
            {
                QuyetDinhCaNhanTapThe(obs, list1.ToList());
            }
            else
            {
                if (list2.Count() > 0)
                {
                    QuyetDinhDanhSachCaNhan(obs, list2.ToList());
                }
                else if (list3.Count() > 0)
                {
                    QuyetDinhDanhSachTapThe(obs, list3.ToList());
                }
                else if (list4.Count() > 0)
                {
                    QuyetDinhCaNhan(obs, list4.ToList());
                }
                else if (list5.Count() > 0)
                {
                    QuyetDinhTapThe(obs, list5.ToList());
                }
            }
        }

        private void QuyetDinhCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var list = new List<Non_QuyetDinhKhenThuongCaNhan>();
            Non_QuyetDinhKhenThuongCaNhan qd = new Non_QuyetDinhKhenThuongCaNhan();
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKhenThuongCaNhan();
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

                qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : "";
                qd.DanhHieu = quyetDinh.DanhHieuKhenThuong != null ? quyetDinh.DanhHieuKhenThuong.TenDanhHieu : "";
                qd.LyDo = quyetDinh.LyDo;
                qd.SoTienThuong = quyetDinh.SoTienKhenThuong.ToString("N0");
                qd.SoTienThuongBangChu = quyetDinh.SoTienBangChu;

                foreach (ChiTietKhenThuongNhanVien item in quyetDinh.ListChiTietKhenThuongNhanVien)
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    qd.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    qd.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    qd.LoaiNhanVien = HamDungChung.GetLoaiNhanVien(item.ThongTinNhanVien);
                    qd.NhanVien = item.ThongTinNhanVien.HoTen;
                    qd.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
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

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuong.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuongCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        private void QuyetDinhDanhSachCaNhan(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var list = new List<Non_QuyetDinhKhenThuong>();
            Non_QuyetDinhKhenThuong qd;
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKhenThuong();
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

                qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : "";
                qd.DanhHieu = quyetDinh.DanhHieuKhenThuong != null ? quyetDinh.DanhHieuKhenThuong.TenDanhHieu : "";
                qd.LyDo = quyetDinh.LyDo;
                qd.SoTienThuong = quyetDinh.SoTienKhenThuong.ToString("N0");
                qd.SoTienThuongBangChu = quyetDinh.SoTienBangChu;

                //master
                Non_ChiTietQuyetDinhKhenThuongMaster master = new Non_ChiTietQuyetDinhKhenThuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DanhHieu = qd.DanhHieu;
                master.NamHoc = qd.NamHoc;
                master.ChucVuNguoiKy = qd.ChucVuNguoiKy;

                //detail
                Non_ChiTietQuyetDinhKhenThuongCaNhanDetail detail;
                quyetDinh.ListChiTietKhenThuongNhanVien.Sorting.Clear();
                quyetDinh.ListChiTietKhenThuongNhanVien.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietKhenThuongNhanVien item in quyetDinh.ListChiTietKhenThuongNhanVien)
                {
                    detail = new Non_ChiTietQuyetDinhKhenThuongCaNhanDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.NhanVien = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                    detail.Chucvu = item.ThongTinNhanVien.ChucVu != null ? item.ThongTinNhanVien.ChucVu.TenChucVu : "";
                    if (TruongConfig.MaTruong == "BUH")
                    {
                        detail.Chucvu = item.ThongTinNhanVien.ChucVu != null ?
                                        item.ThongTinNhanVien.ChucVu.TenChucVu :
                                        item.ThongTinNhanVien.ChucDanh != null ?
                                            item.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                    }

                    qd.Detail.Add(detail);
                    stt++;
                }
                //Lấy tổng số nhân viên
                master.TongNhanVien = stt - 1;

                //Đưa chi tiết vào master
                qd.Master.Add(master);


                list.Add(qd);
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongDanhSachCaNhan.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        private void QuyetDinhTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var list = new List<Non_QuyetDinhKhenThuongTapThe>();
            Non_QuyetDinhKhenThuongTapThe qd;
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKhenThuongTapThe();
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
                qd.SoTienThuong = quyetDinh.SoTienKhenThuong.ToString("N0");
                qd.SoTienThuongBangChu = quyetDinh.SoTienBangChu;

                qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : "";
                qd.DanhHieu = quyetDinh.DanhHieuKhenThuong != null ? quyetDinh.DanhHieuKhenThuong.TenDanhHieu : "";
                qd.LyDo = quyetDinh.LyDo;

                foreach (ChiTietKhenThuongBoPhan item in quyetDinh.ListChiTietKhenThuongBoPhan)
                {
                    qd.DonVi = item.BoPhan.TenBoPhan;
                }
                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongTapThe.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuongTapThe>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        private void QuyetDinhDanhSachTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var list = new List<Non_QuyetDinhKhenThuong>();
            Non_QuyetDinhKhenThuong qd;
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKhenThuong();
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
                qd.SoTienThuong = quyetDinh.SoTienKhenThuong.ToString("N0");
                qd.SoTienThuongBangChu = quyetDinh.SoTienBangChu;

                qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : "";
                qd.DanhHieu = quyetDinh.DanhHieuKhenThuong != null ? quyetDinh.DanhHieuKhenThuong.TenDanhHieu : "";
                qd.LyDo = quyetDinh.LyDo;

                //master
                Non_ChiTietQuyetDinhKhenThuongMaster master = new Non_ChiTietQuyetDinhKhenThuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DanhHieu = qd.DanhHieu;
                master.NamHoc = qd.NamHoc;
                qd.Master1.Add(master);

                //detail
                Non_ChiTietQuyetDinhKhenThuongTapTheDetail detail;
                quyetDinh.ListChiTietKhenThuongBoPhan.Sorting.Clear();
                quyetDinh.ListChiTietKhenThuongBoPhan.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietKhenThuongBoPhan item in quyetDinh.ListChiTietKhenThuongBoPhan)
                {
                    detail = new Non_ChiTietQuyetDinhKhenThuongTapTheDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.DonVi = item.BoPhan.TenBoPhan;

                    qd.Detail1.Add(detail);
                    stt++;
                }
                list.Add(qd);
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuong.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster1.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail1.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        private void QuyetDinhCaNhanTapThe(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            var list = new List<Non_QuyetDinhKhenThuong>();
            Non_QuyetDinhKhenThuong qd;
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhKhenThuong();
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
                qd.SoTienThuong = quyetDinh.SoTienKhenThuong.ToString("N0");
                qd.SoTienThuongBangChu = quyetDinh.SoTienBangChu;

                qd.NamHoc = quyetDinh.NamHoc != null ? quyetDinh.NamHoc.TenNamHoc : "";
                qd.DanhHieu = quyetDinh.DanhHieuKhenThuong != null ? quyetDinh.DanhHieuKhenThuong.TenDanhHieu : "";
                qd.LyDo = quyetDinh.LyDo;

                //master
                Non_ChiTietQuyetDinhKhenThuongMaster master = new Non_ChiTietQuyetDinhKhenThuongMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.NamHoc = qd.NamHoc;
                master.DanhHieu = qd.DanhHieu;
                qd.Master.Add(master);
                qd.Master1.Add(master);

                //detail
                Non_ChiTietQuyetDinhKhenThuongCaNhanDetail detail;
                Non_ChiTietQuyetDinhKhenThuongTapTheDetail detail1;
                quyetDinh.ListChiTietKhenThuongNhanVien.Sorting.Clear();
                quyetDinh.ListChiTietKhenThuongNhanVien.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                quyetDinh.ListChiTietKhenThuongBoPhan.Sorting.Clear();
                quyetDinh.ListChiTietKhenThuongBoPhan.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietKhenThuongNhanVien item in quyetDinh.ListChiTietKhenThuongNhanVien)
                {
                    detail = new Non_ChiTietQuyetDinhKhenThuongCaNhanDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.NhanVien = item.ThongTinNhanVien.HoTen;
                    detail.DonVi = HamDungChung.GetTenBoPhan(item.BoPhan);
                    detail.ChucDanh = HamDungChung.GetChucDanh(item.ThongTinNhanVien);
                    detail.DanhXungVietHoa = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                    detail.DanhXungVietThuong = item.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";

                    qd.Detail.Add(detail);
                    stt++;
                }
                stt = 1;
                foreach (ChiTietKhenThuongBoPhan item in quyetDinh.ListChiTietKhenThuongBoPhan)
                {
                    detail1 = new Non_ChiTietQuyetDinhKhenThuongTapTheDetail();
                    detail1.Oid = quyetDinh.Oid.ToString();
                    detail1.STT = stt.ToString();
                    detail1.DonVi = item.BoPhan.TenBoPhan;

                    qd.Detail1.Add(detail1);
                    stt++;
                }
                list.Add(qd);
            }
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuong.rtf");
            MailMergeTemplate masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster.rtf"));
            MailMergeTemplate detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail.rtf"));
            MailMergeTemplate master1Template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster1.rtf"));
            MailMergeTemplate detail1Template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail1.rtf"));
            MailMergeTemplate[] merge = new MailMergeTemplate[5] { template, masterTemplate, detailTemplate, master1Template, detail1Template };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }
    }
}
