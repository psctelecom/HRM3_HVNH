using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PSC_HRM.Module.XuLyQuyTrinh.SinhNhat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BanLamViec
{
    public class SinhNhat : INhacViec<ThongTinSinhNhat>
    {
        public List<ThongTinSinhNhat> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinSinhNhat> result = new List<ThongTinSinhNhat>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_SinhNhat", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinSinhNhat obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinSinhNhat>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("NgaySinh"))
                            obj.Ngay = DateTime.Parse(item["NgaySinh"].ToString());
                        obj.GhiChu = String.Format("Sinh nhật cán bộ {0}", obj.ThongTinNhanVien.HoTen);
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
