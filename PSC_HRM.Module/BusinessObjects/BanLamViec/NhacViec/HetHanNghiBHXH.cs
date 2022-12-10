using DevExpress.ExpressApp;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class HetHanNghiBHXH : INhacViec<ThongTinNghiBHXH>
    {
        public List<ThongTinNghiBHXH> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinNghiBHXH> result = new List<ThongTinNghiBHXH>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_HetHanNghiBHXH", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinNghiBHXH obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinNghiBHXH>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("LyDo"))
                            obj.LyDoNghi = (LyDoNghiEnum)item["LyDo"];
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
