using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.CauHinh;

namespace PSC_HRM.Module.BaoHiem
{
    public static class BienDongHelper
    {
        /// <summary>
        /// Get quan ly bien dong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ngay"></param>
        /// <returns></returns>
        public static QuanLyBienDong GetQuanLyBienDong(Session session, DateTime ngay)
        {
            DateTime tuNgay = ngay.SetTime(SetTimeEnum.StartMonth);
            DateTime denNgay = ngay.SetTime(SetTimeEnum.EndMonth);
            CriteriaOperator filter = CriteriaOperator.Parse("ThoiGian>=? and ThoiGian<=?",
                tuNgay, denNgay);
            QuanLyBienDong quanLyBienDong = session.FindObject<QuanLyBienDong>(filter);
            if (quanLyBienDong == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    quanLyBienDong = new QuanLyBienDong(uow);
                    quanLyBienDong.ThoiGian = ngay;

                    uow.CommitChanges();
                }
                quanLyBienDong = session.GetObjectByKey<QuanLyBienDong>(quanLyBienDong.Oid);
            }
            return quanLyBienDong;
        }

        /// <summary>
        /// Is exists ho so bao hiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExistsHoSoBaoHiem(Session session, ThongTinNhanVien nhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid);
            HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
            return hoSoBaoHiem != null;
        }

        /// <summary>
        /// create bien dong tang lao dong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void CreateBienDongTangLaoDong(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay)
        {
            QuanLyBienDong quanLy = GetQuanLyBienDong(session, ngay);
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                quyetDinh.ThongTinNhanVien, ngay);
            BienDong_TangLaoDong bienDong = session.FindObject<BienDong_TangLaoDong>(filter);
            if (bienDong == null)
            {
                bienDong = new BienDong_TangLaoDong(session);
                bienDong.QuanLyBienDong = quanLy;
                bienDong.BoPhan = quyetDinh.BoPhan;
                bienDong.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                bienDong.TuNgay = ngay;
            }
            bienDong.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// create bien dong tang lao dong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void CreateBienDongTangLaoDong(Session session, QuyetDinh.QuyetDinh quyetDinh, BoPhan boPhan, ThongTinNhanVien nhanVien, DateTime ngay)
        {
            QuanLyBienDong quanLy = GetQuanLyBienDong(session, ngay);
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                nhanVien, ngay);
            BienDong_TangLaoDong bienDong = session.FindObject<BienDong_TangLaoDong>(filter);
            if (bienDong == null)
            {
                bienDong = new BienDong_TangLaoDong(session);
                bienDong.QuanLyBienDong = quanLy;
                bienDong.BoPhan = boPhan;
                bienDong.ThongTinNhanVien = nhanVien;
                bienDong.TuNgay = ngay;
            }
            bienDong.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// set trang thai tham gia bhxh
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void SetTrangThaiThamGiaBHXH(Session session, QuyetDinhCaNhan quyetDinh, LyDoNghiEnum lyDo)
        {
            HoSoBaoHiem hoSo = session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", quyetDinh.ThongTinNhanVien.Oid));
            if (hoSo != null)
            {
                if (lyDo == LyDoNghiEnum.ThoiViec ||
                    lyDo == LyDoNghiEnum.ThuyenChuyen)
                    hoSo.TrangThai = TrangThaiThamGiaBaoHiemEnum.GiamHan;
                else
                    hoSo.TrangThai = TrangThaiThamGiaBaoHiemEnum.GiamTamThoi;
            }
        }

        /// <summary>
        /// Reset trang thai tham gia bhxh
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void ResetTrangThaiThamGiaBHXH(Session session, QuyetDinhCaNhan quyetDinh)
        {
            HoSoBaoHiem hoSo = session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", quyetDinh.ThongTinNhanVien.Oid));
            if (hoSo != null)
            {
                hoSo.TrangThai = TrangThaiThamGiaBaoHiemEnum.DangThamGia;
            }
        }

        /// <summary>
        /// Create bien dong giam lao dong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void CreateBienDongGiamLaoDong(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay, LyDoNghiEnum lyDo)
        {
            QuanLyBienDong quanLy = GetQuanLyBienDong(session, ngay);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                        quyetDinh.ThongTinNhanVien.Oid, ngay);
            BienDong_GiamLaoDong bienDong = session.FindObject<BienDong_GiamLaoDong>(filter);
            if (bienDong == null)
            {
                bienDong = new BienDong_GiamLaoDong(session);
                bienDong.QuanLyBienDong = quanLy;
                bienDong.BoPhan = quyetDinh.BoPhan;
                bienDong.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                bienDong.TuNgay = ngay;
            }
            bienDong.LyDo = lyDo;
            bienDong.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// Create bien dong giam lao dong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        public static void CreateBienDongGiamLaoDong(Session session, QuyetDinh.QuyetDinh quyetDinh, BoPhan boPhan, ThongTinNhanVien nhanVien, DateTime ngay, LyDoNghiEnum lyDo)
        {
            QuanLyBienDong quanLy = GetQuanLyBienDong(session, ngay);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                        nhanVien, ngay);
            BienDong_GiamLaoDong bienDong = session.FindObject<BienDong_GiamLaoDong>(filter);
            if (bienDong == null)
            {
                bienDong = new BienDong_GiamLaoDong(session);
                bienDong.QuanLyBienDong = quanLy;
                bienDong.BoPhan = boPhan;
                bienDong.ThongTinNhanVien = nhanVien;
                bienDong.TuNgay = ngay;
            }
            bienDong.LyDo = lyDo;
            bienDong.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// create biến động thay đổi lương
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="boPhan"></param>
        /// <param name="ngay"></param>
        /// <param name="soQuyetDinh"></param>
        /// <param name="heSoLuong"></param>
        /// <param name="hspcChucVu"></param>
        /// <param name="vuotKhung"></param>
        /// <param name="thamNien"></param>
        /// <param name="hspcKhac"></param>
        public static void CreateBienDongThayDoiLuong(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay, decimal heSoLuong, decimal hspcChucVu, int vuotKhung, decimal thamNien, decimal hspcKhac, bool huong85PhanTramLuong)
        {
            QuanLyBienDong qlBienDong = GetQuanLyBienDong(session, ngay);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                            quyetDinh.ThongTinNhanVien, ngay);
            BienDong_ThayDoiLuong bienDong = session.FindObject<BienDong_ThayDoiLuong>(filter);
            if (bienDong == null)
            {
                bienDong = new BienDong_ThayDoiLuong(session);
                bienDong.QuanLyBienDong = qlBienDong;
                bienDong.BoPhan = quyetDinh.BoPhan;
                bienDong.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                bienDong.TuNgay = ngay;
            }

            bienDong.TienLuongMoi = huong85PhanTramLuong ? Math.Round(heSoLuong * 0.85m, 3, MidpointRounding.AwayFromZero) : heSoLuong ;
            bienDong.PCCVMoi = hspcChucVu;
            bienDong.TNGDMoi = thamNien;
            bienDong.TNVKMoi = vuotKhung;
            bienDong.PCKMoi = hspcKhac;
            bienDong.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// create biến động thay đổi lương
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="boPhan"></param>
        /// <param name="ngay"></param>
        /// <param name="soQuyetDinh"></param>
        /// <param name="heSoLuong"></param>
        /// <param name="hspcChucVu"></param>
        /// <param name="vuotKhung"></param>
        /// <param name="thamNien"></param>
        /// <param name="hspcKhac"></param>
        public static void CreateBienDongThayDoiLuong(Session session, QuyetDinh.QuyetDinh quyetDinh, BoPhan boPhan, ThongTinNhanVien nhanVien, DateTime ngay, decimal heSoLuong, decimal hspcChucVu, int vuotKhung, decimal thamNien, decimal hspcKhac, bool huong85PhanTramLuong)
        {
            QuanLyBienDong qlBienDong = GetQuanLyBienDong(session, ngay);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                            nhanVien, ngay);
            BienDong_ThayDoiLuong bienDong = session.FindObject<BienDong_ThayDoiLuong>(filter);
            if (bienDong == null)
            {
                bienDong = new BienDong_ThayDoiLuong(session);
                bienDong.QuanLyBienDong = qlBienDong;
                bienDong.BoPhan = boPhan;
                bienDong.ThongTinNhanVien = nhanVien;
                bienDong.TuNgay = ngay;
            }
            bienDong.TienLuongMoi = huong85PhanTramLuong ? Math.Round(heSoLuong * 0.85m, 3, MidpointRounding.AwayFromZero) : heSoLuong;
            bienDong.PCCVMoi = hspcChucVu;
            bienDong.TNGDMoi = thamNien;
            bienDong.TNVKMoi = vuotKhung;
            bienDong.PCKMoi = hspcKhac;
            bienDong.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// Create bien dong thay doi chuc danh
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngay"></param>
        /// <param name="chucDanhMoi"></param>
        public static void CreateBienDongThayDoiChucDanh(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay, string chucDanhMoi)
        {
            QuanLyBienDong qlBienDong = GetQuanLyBienDong(session, ngay);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                         quyetDinh.ThongTinNhanVien, ngay);
            BienDong_ThayDoiChucDanh bienDong1 = session.FindObject<BienDong_ThayDoiChucDanh>(filter);
            if (bienDong1 == null)
            {
                bienDong1 = new BienDong_ThayDoiChucDanh(session);
                bienDong1.QuanLyBienDong = qlBienDong;
                bienDong1.BoPhan = quyetDinh.BoPhan;
                bienDong1.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                bienDong1.TuNgay = ngay;
            }
            bienDong1.ChucDanhCu = quyetDinh.ThongTinNhanVien.ChucVu != null ? String.Format("{0} {1}", quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu, quyetDinh.BoPhan.TenBoPhan) :
                "Nhân viên " + quyetDinh.BoPhan.TenBoPhan;
            bienDong1.ChucDanhMoi = chucDanhMoi;
            bienDong1.GhiChu = "QĐ số " + quyetDinh.SoQuyetDinh;
        }

        /// <summary>
        /// Xoa bien dong
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="ngay"></param>
        public static void DeleteBienDong<T>(Session session, ThongTinNhanVien nhanVien, DateTime ngay) where T : BienDong
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                            nhanVien, ngay);
            T bienDong = session.FindObject<T>(filter);
            if (bienDong != null)
            {
                session.Delete(bienDong);
                session.Save(bienDong);
            }
        }
    }
}
