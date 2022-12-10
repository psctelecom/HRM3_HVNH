using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Utils;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap;


namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class LapThanhToan : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_ChungTu_LapThanhToan", CommandType.StoredProcedure, new SqlParameter("@ThanhToan", obj.Oid));
        }
    }
}
