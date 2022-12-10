using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.NangLuong
{
    public class NangLuongHelper
    {
        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhNangLuong quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhNangLuong
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }
    }
}
