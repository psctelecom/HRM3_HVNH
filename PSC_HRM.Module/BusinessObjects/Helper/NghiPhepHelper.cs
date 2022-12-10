using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NghiPhep
{
    public static class NghiPhepHelper
    {
        public static int TinhSoNgayPhep(ThongTinNhanVien nhanVien, int nam)
        {
            if (nhanVien.NgayVaoCoQuan != DateTime.MinValue
                && TinhSoNam(nhanVien.NgayVaoCoQuan.Year, nam) > 0)
            {
                return 12 + TinhSoNam(nhanVien.NgayVaoCoQuan.Year, nam) / 5;
            }
            return 0;
        }

        public static decimal SoNgayNghiPhepConLai(Session session, ThongTinNhanVien nhanVien)
        {
            int nam = HamDungChung.GetServerTime().Year;
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyNghiPhep.Nam=? and ThongTinNhanVien=?", nam, nhanVien.Oid);
            using (XPCollection<ThongTinNghiPhep> npList = new XPCollection<ThongTinNghiPhep>(session, filter))
            {
                npList.TopReturnedObjects = 1;
                if (npList.Count == 1)
                {
                    return npList[0].SoNgayPhepConLai;
                }
                else
                    return TinhSoNgayPhep(nhanVien, nam);
            }
        }

        private static int TinhSoNam(int namBatDau, int namKetThuc)
        {
            if (namBatDau <= namKetThuc)
                return (namKetThuc - namBatDau) + 1;
            return (namBatDau - namKetThuc) + 1;
        }
    }
}
