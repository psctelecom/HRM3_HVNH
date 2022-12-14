using System;
using DevExpress.ExpressApp;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public class TinhQuyetToanThuNhapTangThem : ITaiChinh
    {
        public void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            BangQuyetToanThuNhapTangThem bangQuyetToan = obj as BangQuyetToanThuNhapTangThem;
            if (bangQuyetToan != null)
            {

                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@BangQuyetToanThuNhapTangThem", bangQuyetToan.Oid);
                    param[1] = new SqlParameter("@Nam", bangQuyetToan.Nam);
                    param[2] = new SqlParameter("@HeSoTangThem", bangQuyetToan.HeSoTangThem);
                    param[3] = new SqlParameter("@ThongTinTruong", bangQuyetToan.ThongTinTruong.Oid);

                    Utils.XuLyDuLieu(((XPObjectSpace)obs).Session, "spd_TaiChinh_ThuNhapTangThem_TinhQuyetToanThuNhapTangThem", CommandType.StoredProcedure, param);
            }
        }
    }
}
