using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.BoNhiem
{
    public static class BoNhiemHelper
    {
        /// <summary>
        /// Create quan ly bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ngay"></param>
        /// <returns></returns>
        public static QuanLyBoNhiem CreateQuanLyBoNhiem(Session session, DateTime ngay)
        {
            Guid oid = new ThucHienQuyTrinhBoNhiem().DaBatDau(session);
            QuanLyBoNhiem quanLy = null;
            if (oid != Guid.Empty)
                quanLy = session.GetObjectByKey<QuanLyBoNhiem>(oid);

            if (quanLy == null)
            {
                NamHoc namHoc = HamDungChung.SearchNamHoc(session, ngay);
                ThongTinTruong truong = HamDungChung.ThongTinTruong(session);
                quanLy = session.FindObject<QuanLyBoNhiem>(CriteriaOperator.Parse("ThongTinTruong=? and NamHoc=?", truong, namHoc));
                if (quanLy == null)
                {
                    using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                    {
                        quanLy = new QuanLyBoNhiem(uow);
                        quanLy.NamHoc = uow.GetObjectByKey<NamHoc>(namHoc.Oid);
                        uow.CommitChanges();
                    }
                    quanLy = session.GetObjectByKey<QuanLyBoNhiem>(quanLy.Oid);
                }
            }

            return quanLy;
        }

        /// <summary>
        /// Create thong tin bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateBoNhiem(Session session, QuyetDinhCaNhan quyetDinh, ChucVu chucVu, bool kiemNhiem)
        {
            QuanLyBoNhiem qlBoNhiem = CreateQuanLyBoNhiem(session, quyetDinh.NgayHieuLuc);
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyBoNhiem=? and QuyetDinh=?",
                    qlBoNhiem, quyetDinh);
            ChiTietBoNhiem boNhiem = session.FindObject<ChiTietBoNhiem>(filter);
            if (boNhiem == null)
            {
                boNhiem = new ChiTietBoNhiem(session);
                boNhiem.QuanLyBoNhiem = qlBoNhiem;
                boNhiem.BoPhan = quyetDinh.BoPhan;
                boNhiem.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                boNhiem.QuyetDinh = quyetDinh;
                boNhiem.ChucVu = chucVu;
                boNhiem.KiemNhiem = kiemNhiem;
            }
        }

        /// <summary>
        /// Create thong tin mien nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateMienNhiem(Session session, QuyetDinhCaNhan quyetDinh, ChucVu chucVu, bool kiemNhiem)
        {
            QuanLyBoNhiem qlBoNhiem = CreateQuanLyBoNhiem(session, quyetDinh.NgayHieuLuc);
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyBoNhiem=? and QuyetDinh=?",
                    qlBoNhiem, quyetDinh);
            ChiTietMienNhiem miemNhiem = session.FindObject<ChiTietMienNhiem>(filter);
            if (miemNhiem == null)
            {
                miemNhiem = new ChiTietMienNhiem(session);
                miemNhiem.QuanLyBoNhiem = qlBoNhiem;
                miemNhiem.BoPhan = quyetDinh.BoPhan;
                miemNhiem.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                miemNhiem.QuyetDinh = quyetDinh;
                miemNhiem.ChucVu = chucVu;
                miemNhiem.KiemNhiem = kiemNhiem;
            }
        }

        /// <summary>
        /// Delete bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteBoNhiem<T>(Session session, QuyetDinhCaNhan quyetDinh) where T : BaseObject
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=?",
                    quyetDinh);
            T obj = session.FindObject<T>(filter);
            if (obj != null)
            {
                session.Delete(obj);
                session.Save(obj);
            }
        }

        public static void DeleteChucVuKiemNhiem<T>(Session session, QuyetDinhBoNhiemKiemNhiem quyetdinh) where T : BaseObject
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien =? AND QuyetDinhBoNhiemKiemNhiem = ?",
                 quyetdinh.ThongTinNhanVien.Oid, quyetdinh.Oid);
            T obj = session.FindObject<T>(filter);
            if (obj != null)
            {
                session.Delete(obj);
                session.Save(obj);
            }
        }
    }
}
