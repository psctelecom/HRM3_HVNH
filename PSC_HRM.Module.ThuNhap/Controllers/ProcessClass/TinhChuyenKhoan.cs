using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhChuyenKhoan : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
               Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_ChungTu_TinhChuyenKhoan", CommandType.StoredProcedure, new SqlParameter("@ChungTu", obj.Oid));
        }
    }
}
