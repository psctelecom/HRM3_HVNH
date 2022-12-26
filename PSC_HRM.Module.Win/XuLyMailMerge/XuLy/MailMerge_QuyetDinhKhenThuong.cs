﻿using DevExpress.Data.Filtering;
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
        bool giayKhen = false;
        bool phanVien = false;
        string tenPhanVien = null;
        
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhKhenThuong> qdList)
        {
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                if (quyetDinh.DanhHieuKhenThuong.TenDanhHieu.Contains("Giấy khen"))
                {
                    giayKhen = true;
                }
            }

            //List NV 1, list BP trống (ok)
            var listA = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count == 1
                            && q.ListChiTietKhenThuongBoPhan.Count == 0
                        select q;
            //List NV hơn 1, list BP trống (ok)
            var listB = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count > 1
                            && q.ListChiTietKhenThuongBoPhan.Count == 0
                        select q;
            //List NV trống, list BP 1 (ok)
            var listC = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count == 0
                            && q.ListChiTietKhenThuongBoPhan.Count == 1
                        select q;
            //List NV trống, list BP hơn 1 (ok)
            var listD = from q in qdList
                        where q.ListChiTietKhenThuongNhanVien.Count == 0
                            && q.ListChiTietKhenThuongBoPhan.Count > 1
                        select q;
            //List NV có, list BP có (ok)
            var listE = from q in qdList
                        where q.ListChiTietKhenThuongBoPhan.Count > 0
                            && q.ListChiTietKhenThuongNhanVien.Count > 0
                        select q;
            
            if (listE.Count() > 0)
            {
                //QuyetDinhCaNhanTapThe(obs, listE.ToList());
                QuyetDinhCaNhanTapTheGiayKhen(obs, listE.ToList());
            }
            else
            {
                if (listA.Count() > 0)
                {
                    QuyetDinhCaNhan(obs, listA.ToList());
                }
                else if (listB.Count() > 0)
                {
                    QuyetDinhDanhSachCaNhan(obs, listB.ToList());
                }
                else if (listC.Count() > 0)
                {
                    QuyetDinhTapThe(obs, listC.ToList());
                }
                else if (listD.Count() > 0)
                {
                    QuyetDinhDanhSachTapThe(obs, listD.ToList());
                }
            }
        }

        //List NV 1, list BP trống (ok)
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
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongCaNhan.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuongCaNhan>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        //List NV hơn 1, list BP trống (ok)
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

                    detail.GhiChu = qd.LyDo;

                    qd.Detail.Add(detail);
                    stt++;
                }
                //Lấy tổng số nhân viên
                master.TongNhanVien = stt - 1;

                qd.SoLuongCanBo = master.TongNhanVien.ToString();

                //Đưa chi tiết vào master
                qd.Master.Add(master);

                list.Add(qd);
            }

            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongDanhSachCaNhan.rtf");
            MailMergeTemplate masterTemplate = null;
            MailMergeTemplate detailTemplate = null;

            if (giayKhen)
            {
                masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenMaster.rtf"));
                detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenDetail.rtf"));
            }
            else
            {
                masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster.rtf"));
                detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail.rtf"));
            }
            
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        //List NV trống, list BP 1 (ok)
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
                    if (item.BoPhan.BoPhanCha.TenBoPhan.Contains("Phân viện"))
                    {
                        phanVien = true;
                        tenPhanVien = item.BoPhan.BoPhanCha.TenBoPhan;
                    }

                    qd.DonVi = item.BoPhan.TenBoPhan;
                }

                qd.TenPhanVien = tenPhanVien;

                list.Add(qd);
            }
            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongTapThe.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuongTapThe>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        //List NV trống, list BP hơn 1 (ok)
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
                Non_ChiTietQuyetDinhKhenThuongTapTheMaster master = new Non_ChiTietQuyetDinhKhenThuongTapTheMaster();
                master.Oid = quyetDinh.Oid.ToString();
                master.DonViChuQuan = qd.DonViChuQuan;
                master.TenTruongVietHoa = qd.TenTruongVietHoa;
                master.TenTruongVietThuong = qd.TenTruongVietThuong;
                master.SoQuyetDinh = qd.SoQuyetDinh;
                master.NguoiKy = qd.NguoiKy;
                master.NgayQuyetDinh = qd.NgayQuyetDinh;
                master.DanhHieu = qd.DanhHieu;
                master.NamHoc = qd.NamHoc;

                //detail
                Non_ChiTietQuyetDinhKhenThuongTapTheDetail detail;
                quyetDinh.ListChiTietKhenThuongBoPhan.Sorting.Clear();
                quyetDinh.ListChiTietKhenThuongBoPhan.Sorting.Add(new DevExpress.Xpo.SortProperty("BoPhan.STT", DevExpress.Xpo.DB.SortingDirection.Ascending));
                int stt = 1;
                foreach (ChiTietKhenThuongBoPhan item in quyetDinh.ListChiTietKhenThuongBoPhan)
                {
                    if (item.BoPhan.BoPhanCha.TenBoPhan.Contains("Phân viện"))
                    {
                        phanVien = true;
                        tenPhanVien = item.BoPhan.BoPhanCha.TenBoPhan;
                    }

                    detail = new Non_ChiTietQuyetDinhKhenThuongTapTheDetail();
                    detail.Oid = quyetDinh.Oid.ToString();
                    detail.STT = stt.ToString();
                    detail.DonVi = item.BoPhan.TenBoPhan;

                    detail.GhiChu = qd.LyDo;

                    qd.Detail1.Add(detail);
                    stt++;
                }

                master.TongNhanVien = stt - 1;

                qd.SoLuongCanBo = master.TongNhanVien.ToString();

                qd.TenPhanVien = tenPhanVien;

                qd.Master1.Add(master);
              
                list.Add(qd);
            }

            MailMergeTemplate masterTemplate = null;
            MailMergeTemplate detailTemplate = null;
            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongDanhSachTapThe.rtf");

            if (phanVien)
            {
                masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongPhanVienMaster1.rtf"));
                detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongPhanVienDetail1.rtf"));
            }
            else
            {
                if (giayKhen)
                {
                    masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenMaster1.rtf"));
                    detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenDetail1.rtf"));
                }
                else
                {
                    masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster1.rtf"));
                    detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail1.rtf"));
                }
            }
            
            MailMergeTemplate[] merge = new MailMergeTemplate[3] { template, masterTemplate, detailTemplate };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }

        //List Nv có, list BP có (ok)
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
                
                //
                Non_ChiTietQuyetDinhKhenThuongTapTheMaster master1 = new Non_ChiTietQuyetDinhKhenThuongTapTheMaster();
                master1.Oid = quyetDinh.Oid.ToString();
                master1.DonViChuQuan = qd.DonViChuQuan;
                master1.TenTruongVietHoa = qd.TenTruongVietHoa;
                master1.TenTruongVietThuong = qd.TenTruongVietThuong;
                master1.SoQuyetDinh = qd.SoQuyetDinh;
                master1.NguoiKy = qd.NguoiKy;
                master1.NgayQuyetDinh = qd.NgayQuyetDinh;
                master1.NamHoc = qd.NamHoc;
                master1.DanhHieu = qd.DanhHieu;

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

                qd.SLCaNhan = (stt - 1).ToString();
                qd.Master.Add(master);

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

                qd.SLTapThe = (stt - 1).ToString();
                qd.Master1.Add(master1);

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

        //Bao gồm Giấy khen và danh hiệu khác
        private void QuyetDinhCaNhanTapTheGiayKhen(DevExpress.ExpressApp.IObjectSpace obs, List<QuyetDinhKhenThuong> qdList)
        {
            //bool giayKhen = false;
            var list = new List<Non_QuyetDinhKhenThuong>();
            Non_QuyetDinhKhenThuong qd;
            foreach (QuyetDinhKhenThuong quyetDinh in qdList)
            {
                //if (quyetDinh.DanhHieuKhenThuong.TenDanhHieu.Contains("Giấy khen"))
                //{
                //    giayKhen = true;
                //}              

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

                //
                Non_ChiTietQuyetDinhKhenThuongTapTheMaster master1 = new Non_ChiTietQuyetDinhKhenThuongTapTheMaster();
                master1.Oid = quyetDinh.Oid.ToString();
                master1.DonViChuQuan = qd.DonViChuQuan;
                master1.TenTruongVietHoa = qd.TenTruongVietHoa;
                master1.TenTruongVietThuong = qd.TenTruongVietThuong;
                master1.SoQuyetDinh = qd.SoQuyetDinh;
                master1.NguoiKy = qd.NguoiKy;
                master1.NgayQuyetDinh = qd.NgayQuyetDinh;
                master1.NamHoc = qd.NamHoc;
                master1.DanhHieu = qd.DanhHieu;

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
                    detail.Chucvu = item.ThongTinNhanVien.ChucVu.TenChucVu;

                    qd.Detail.Add(detail);
                    stt++;
                }

                qd.SLCaNhan = (stt - 1).ToString();
                qd.Master.Add(master);

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

                qd.SLTapThe = (stt - 1).ToString();
                qd.Master1.Add(master1);

                list.Add(qd);
            }

            MailMergeTemplate masterTemplate = null;
            MailMergeTemplate detailTemplate = null;
            MailMergeTemplate master1Template = null;
            MailMergeTemplate detail1Template = null;

            MailMergeTemplate template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuong.rtf");

            if (giayKhen)
            {
                //template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuongGiayKhen.rtf");
                masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenMaster.rtf"));
                detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenDetail.rtf"));
                master1Template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenMaster1.rtf"));
                detail1Template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongGiayKhenDetail1.rtf"));
            }
            else//danh hiệu khác
            {
                //template = HamDungChung.GetTemplate(obs, "QuyetDinhKhenThuong.rtf");
                masterTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster.rtf"));
                detailTemplate = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail.rtf"));
                master1Template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongMaster1.rtf"));
                detail1Template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "QuyetDinhKhenThuongDetail1.rtf"));
            }
                       
            MailMergeTemplate[] merge = new MailMergeTemplate[5] { template, masterTemplate, detailTemplate, master1Template, detail1Template };
            if (merge[0] != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhKhenThuong>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ khen thưởng trong hệ thống.");
        }
    }
}
