using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.Data;
using DevExpress.Utils;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap;


namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhKhauTruThueTNCN : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý", "Vui lòng chờ..."))
            {
                Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_ThueTNCN_TinhKhauTruThueTNCN", CommandType.StoredProcedure, new SqlParameter("@ToKhai", obj.Oid));
            }
        }
    }
}
