using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.TamUng;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhKhauTruTamUng : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangKhauTruTamUng bangKhauTruTamUng = obj as BangKhauTruTamUng;
            if (bangKhauTruTamUng != null)
            {
                using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý", "Vui lòng chờ..."))
                {
                    //xoa du lieu cu
                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_TamUng_XoaChiTietKhauTruTamUngTheoBangKhauTruTamUng", CommandType.StoredProcedure, new SqlParameter("@BangKhauTruTamUng", bangKhauTruTamUng.Oid));

                    //tinh khau tru tam ung
                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_TamUng_TinhKhauTruTamUng", CommandType.StoredProcedure, new SqlParameter("@BangKhauTruTamUng", bangKhauTruTamUng.Oid));
                }
            }
        }
    }
}
