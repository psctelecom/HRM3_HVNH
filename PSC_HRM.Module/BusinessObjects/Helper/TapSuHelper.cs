using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.XuLyQuyTrinh.TapSu;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.TapSu
{
    public static class TapSuHelper
    {
        /// <summary>
        /// Search tap su
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="qlTapSu"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static T SearchTapSu<T>(Session session, QuanLyTapSu qlTapSu, ThongTinNhanVien nhanVien) where T : BaseObject
        {
            T t = session.FindObject<T>(CriteriaOperator.Parse("QuanLyTapSu=? and ThongTinNhanVien=?", qlTapSu, nhanVien));
            return t;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="duyetDK"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhBoNhiemNgach quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhBoNhiemNgach
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="duyetDK"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhChoPhepNghiHoc quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietChoPhepNghiHoc
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="duyetDK"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhHuongDanTapSu quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhHuongDanTapSu
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }
    }
}
