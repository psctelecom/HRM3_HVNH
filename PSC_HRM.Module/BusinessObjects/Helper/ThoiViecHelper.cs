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
using PSC_HRM.Module.XuLyQuyTrinh.ThoiViec;

namespace PSC_HRM.Module.ThoiViec
{
    public static class ThoiViecHelper
    {
        /// <summary>
        /// Create quan ly bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ngay"></param>
        /// <returns></returns>
        public static QuanLyThoiViec CreateQuanLyThoiViec(Session session, DateTime ngay)
        {
            QuanLyThoiViec quanLy = session.FindObject<QuanLyThoiViec>(CriteriaOperator.Parse("NamHoc.NgayBatDau<=? and NamHoc.NgayKetThuc>=?", ngay, ngay));

            if (quanLy == null)
            {
                NamHoc namHoc = HamDungChung.SearchNamHoc(session, ngay);
                ThongTinTruong truong = HamDungChung.ThongTinTruong(session);
                quanLy = session.FindObject<QuanLyThoiViec>(CriteriaOperator.Parse("ThongTinTruong=? and NamHoc=?", truong, namHoc));
                if (quanLy == null)
                {
                    using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                    {
                        quanLy = new QuanLyThoiViec(uow);
                        quanLy.NamHoc = uow.GetObjectByKey<NamHoc>(namHoc.Oid);
                        uow.CommitChanges();
                    }
                    quanLy = session.GetObjectByKey<QuanLyThoiViec>(quanLy.Oid);
                }
            }

            return quanLy;
        }

        /// <summary>
        /// Create thong tin thoi viec
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateThoiViec(Session session, QuyetDinhCaNhan quyetDinh, string lyDo, DateTime ngayNghi)
        {
            QuanLyThoiViec qlThoiViec = CreateQuanLyThoiViec(session, quyetDinh.NgayHieuLuc);
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyThoiViec=? and QuyetDinhThoiViec=?",
                    qlThoiViec, quyetDinh);
            ChiTietThoiViec thoiViec = session.FindObject<ChiTietThoiViec>(filter);
            if (thoiViec == null)
            {
                thoiViec = new ChiTietThoiViec(session);
                thoiViec.QuanLyThoiViec = qlThoiViec;
                thoiViec.BoPhan = quyetDinh.BoPhan;
                thoiViec.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                thoiViec.QuyetDinhThoiViec = quyetDinh;
                thoiViec.LyDo = lyDo;
                thoiViec.NghiViecTuNgay = ngayNghi;
            }
        }

        /// <summary>
        /// Delete thoi viec
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteThoiViec<T>(Session session, QuyetDinhCaNhan quyetDinh) where T : BaseObject
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhThoiViec=?",
                    quyetDinh);
            T obj = session.FindObject<T>(filter);
            if (obj != null)
            {
                session.Delete(obj);
                session.Save(obj);
            }
        }
    }
}
