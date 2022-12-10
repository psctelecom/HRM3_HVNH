using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public static class Utils
    {
        /// <summary>
        /// Phan quyen don vi
        /// </summary>
        /// <param name="session"></param>
        /// <param name="truong"></param>
        public static void PhanQuyenDonVi(Session session, ThongTinTruong truong)
        {
            string dieuKienBoPhan = HamDungChung.GetPhanQuyenBoPhan();
            SqlCommand cmd = new SqlCommand("spd_TaiChinh_PhanQuyen");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ThongTinTruong", truong.Oid);
            cmd.Parameters.AddWithValue("@DieuKien", dieuKienBoPhan);
            DataProvider.ExecuteNonQuery((SqlConnection)session.Connection, cmd);
        }

        /// <summary>
        /// XuLyDuLieu
        /// </summary>
        /// <param name="session"></param>
        /// <param name="type"></param>
        /// <param name="query"></param>
        /// <param name="param"></param>
        public static void XuLyDuLieu(Session session, string query, CommandType type, params SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = type;
            if (param != null)
                cmd.Parameters.AddRange(param);
            DataProvider.ExecuteNonQuery((SqlConnection)session.Connection, cmd);
        }
    }
}
