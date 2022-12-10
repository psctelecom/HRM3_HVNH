using System;
using System.Collections.Generic;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using System.Data;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BanLamViec
{
    public class TheoDoiBoNhiem : INhacViec<ThongTinBoNhiem>
    {
        public List<ThongTinBoNhiem> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinBoNhiem> result = new List<ThongTinBoNhiem>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_BoNhiem", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinBoNhiem obj;
                    Guid oid;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinBoNhiem>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("QuyetDinh") && Guid.TryParse(item["QuyetDinh"].ToString(), out oid))
                            obj.QuyetDinh = obs.GetObjectByKey<QuyetDinhBoNhiem>(oid);

                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }

}
