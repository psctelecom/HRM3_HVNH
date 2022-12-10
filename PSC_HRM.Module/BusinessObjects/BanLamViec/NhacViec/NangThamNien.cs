using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNien;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class NangThamNien : INhacViec<ThongTinNangThamNien>
    {
        public List<ThongTinNangThamNien> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinNangThamNien> result = new List<ThongTinNangThamNien>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_ThamNienGiangVien", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinNangThamNien obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinNangThamNien>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("ThamNien"))
                            obj.ThamNienCu = int.Parse(item["ThamNien"].ToString());
                        if (!item.IsNull("NgayHuongThamNien"))
                            obj.Ngay = DateTime.Parse(item["NgayHuongThamNien"].ToString());
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
