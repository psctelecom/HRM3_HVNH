using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhTruyLuong : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BangTruyLuong", obj.Oid);

            SqlCommand cmd = new SqlCommand("spd_TaiChinh_TruyLuong_TinhTruyLuong");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param);
            DataProvider.ExecuteNonQuery((SqlConnection)((XPObjectSpace)obs).Session.Connection, cmd);
        }
    }
}
