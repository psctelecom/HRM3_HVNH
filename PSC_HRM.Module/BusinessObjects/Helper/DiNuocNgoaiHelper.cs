using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.DiNuocNgoai;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.DiNuocNgoai
{
    public static class DiNuocNgoaiHelper
    {
        /// <summary>
        /// nước ngoài
        /// </summary>
        /// <param name="quocGia"></param>
        /// <returns></returns>
        public static bool IsNgoaiNuoc(QuocGia quocGia)
        {
            if (quocGia != null
                && HamDungChung.CauHinhChung.QuocGia != null
                && quocGia.Oid != HamDungChung.CauHinhChung.QuocGia.Oid)
                return true;
            return false;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExits(QuyetDinhDiNuocNgoai quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhDiNuocNgoai
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }
    }
}
