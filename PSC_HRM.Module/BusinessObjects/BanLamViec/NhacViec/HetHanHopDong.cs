using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.HopDong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class HetHanHopDong : INhacViec<ThongTinHopDong>
    {
        public List<ThongTinHopDong> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinHopDong> result = new List<ThongTinHopDong>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_HetHanHopDong", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinHopDong obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinHopDong>();
                        if (!item.IsNull("NhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["NhanVien"].ToString()));
                        if (!item.IsNull("Oid"))
                            obj.HopDong = obs.GetObjectByKey<HopDong_NhanVien>(new Guid(item["Oid"].ToString()));
                        if (!item.IsNull("TuNgay"))
                            obj.TuNgay = DateTime.Parse(item["TuNgay"].ToString());
                        if (!item.IsNull("DenNgay"))
                            obj.Ngay = DateTime.Parse(item["DenNgay"].ToString());
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
