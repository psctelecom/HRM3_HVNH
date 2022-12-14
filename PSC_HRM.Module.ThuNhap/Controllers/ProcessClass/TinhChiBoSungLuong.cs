using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap;
using PSC_HRM.Module.ThuNhap.BoSungLuong;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public static class TinhChiBoSungLuong
    {
        public static void LuongKy1(BoSungLuongNhanVien boSungLuong, Session sesion)
        {
            if (boSungLuong != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoSungLuongNhanVien", boSungLuong.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungLuongKy1", CommandType.StoredProcedure, param);
            }
        }

        public static void LuongPhuCapUuDai(BoSungLuongNhanVien boSungLuong, Session sesion)
        {
            if (boSungLuong != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoSungLuongNhanVien", boSungLuong.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungLuongPhuCapUuDai", CommandType.StoredProcedure, param);
            }
        }

        public static void NangLuongKy2(BoSungLuongNhanVien boSungLuong, Session sesion)
        {
            if (boSungLuong != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoSungLuongNhanVien", boSungLuong.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungNangLuongKy2", CommandType.StoredProcedure, param);
            }
        }

        public static void LuongKy2(BoSungLuongNhanVien boSungLuong, Session sesion)
        {
            if (boSungLuong != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoSungLuongNhanVien", boSungLuong.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungLuongKy2", CommandType.StoredProcedure, param);
            }
        }

        public static void PhuCapThamNienNhaGiao(ChiBoSungPhuCapThamNien chiBoSungPhuCapThamNien, Session sesion)
        {
            if (chiBoSungPhuCapThamNien != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ChiBoSungPhuCapThamNien", chiBoSungPhuCapThamNien.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungLuongPhuCapThamNienNhaGiao", CommandType.StoredProcedure, param);
            }
        }

        public static void PhuCapTrachNhiem(BoSungLuongNhanVien boSungLuong, Session sesion)
        {
            if (boSungLuong != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoSungLuongNhanVien", boSungLuong.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungLuongPhuCapTrachNhiem", CommandType.StoredProcedure, param);
            }
        }


        public static void NangLuongKy1(BoSungLuongNhanVien boSungLuong, Session sesion)
        {
            if (boSungLuong != null)
            {
                //
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoSungLuongNhanVien", boSungLuong.Oid);
                //
                Utils.XuLyDuLieu(sesion, "spd_TaiChinh_BoSungLuong_TinhChiBoSungNangLuongKy1", CommandType.StoredProcedure, param);
            }
        }
    }
}
