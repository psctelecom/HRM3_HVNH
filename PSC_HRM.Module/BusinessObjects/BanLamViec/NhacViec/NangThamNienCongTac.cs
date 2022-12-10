using PSC_HRM.Module.XuLyQuyTrinh.NangThamNienCongTac;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BanLamViec
{
    public class NangThamNienCongTac : INhacViec<ThamNienCongTac>
    {
        public List<ThamNienCongTac> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThamNienCongTac> result = new List<ThamNienCongTac>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_ThamNienCongTac", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThamNienCongTac obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThamNienCongTac>();
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
