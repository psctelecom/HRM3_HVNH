using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.ChuyenNgach
{
    public class ChuyenNgachHelper
    {
        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhChuyenNgach quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhChuyenNgach
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }
    }
}
