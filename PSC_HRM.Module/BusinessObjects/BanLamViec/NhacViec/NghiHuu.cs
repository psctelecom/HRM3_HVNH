using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class NghiHuu : INhacViec<ThongTinNghiHuu>
    {
        public List<ThongTinNghiHuu> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinNghiHuu> result = new List<ThongTinNghiHuu>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_NghiHuu", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinNghiHuu obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinNghiHuu>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
