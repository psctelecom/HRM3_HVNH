using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.XuLyQuyTrinh.ThuViec;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuViec
{
    public static class ThuViecHelper
    {
        /// <summary>
        /// Get quan ly thu viec
        /// </summary>
        /// <param name="session"></param>
        /// <param name="namHoc"></param>
        /// <returns></returns>
        public static QuanLyThuViec GetQuanLyThuViec(Session session, DateTime ngay)
        {
            Guid oid = new ThucHienQuyTrinhThuViec().DaBatDau(session);
            QuanLyThuViec qlThuViec = null;
            if (oid != Guid.Empty)
                qlThuViec = session.GetObjectByKey<QuanLyThuViec>(oid);

            if (qlThuViec == null)
            {
                ThongTinTruong truong = HamDungChung.ThongTinTruong(session);
                NamHoc namHoc = HamDungChung.SearchNamHoc(session, ngay);
                int dot;
                object obj = session.Evaluate<QuanLyTuyenDung>(CriteriaOperator.Parse("Max(DotTuyenDung)"), CriteriaOperator.Parse("NamHoc=?", namHoc));
                if (obj != null)
                    dot = (int)obj;
                else
                    dot = 1;

                qlThuViec = session.FindObject<QuanLyThuViec>(CriteriaOperator.Parse("ThongTinTruong=? and NamHoc=? and Dot=?", truong, namHoc.Oid, dot));
                if (qlThuViec == null)
                {
                    qlThuViec = new QuanLyThuViec(session);
                    qlThuViec.NamHoc = namHoc;
                }
            }
            return qlThuViec;
        }

        /// <summary>
        /// Delete thu viec
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="namHoc"></param>
        /// <param name="nhanVien"></param>
        public static void DeleteThuViec<T>(Session session, ThongTinNhanVien nhanVien, DateTime ngay) where T : BaseObject
        {
            QuanLyThuViec qlThuViec = GetQuanLyThuViec(session, ngay);
            T t = SearchThuViec<T>(session, qlThuViec, nhanVien);
            if (t != null)
            {
                session.Delete(t);
                session.Save(t);
            }
        }

        /// <summary>
        /// Search thu viec
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="qlThuViec"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static T SearchThuViec<T>(Session session, QuanLyThuViec qlThuViec, ThongTinNhanVien nhanVien) where T : BaseObject
        {
            T t = session.FindObject<T>(CriteriaOperator.Parse("QuanLyThuViec=? and ThongTinNhanVien=?", qlThuViec, nhanVien));
            return t;
        }
    }
}
