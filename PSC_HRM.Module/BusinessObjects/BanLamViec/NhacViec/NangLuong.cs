using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class NangLuong : INhacViec<ThongTinNangLuong>
    {
        public List<ThongTinNangLuong> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinNangLuong> result = new List<ThongTinNangLuong>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_NangLuong", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinNangLuong obj;
                    Guid oid;
                    decimal dTemp;
                    DateTime dtTemp;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinNangLuong>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("NgachLuong") && Guid.TryParse(item["NgachLuong"].ToString(), out oid))
                            obj.NgachLuong = obs.GetObjectByKey<NgachLuong>(oid);
                        if (!item.IsNull("BacLuong") && Guid.TryParse(item["BacLuong"].ToString(), out oid))
                            obj.BacLuongCu = obs.GetObjectByKey<BacLuong>(oid);
                        if (!item.IsNull("HeSoLuong") && decimal.TryParse(item["HeSoLuong"].ToString(), out dTemp))
                            obj.HeSoLuongCu = dTemp;
                        if (!item.IsNull("MocNangLuong") && DateTime.TryParse(item["MocNangLuong"].ToString(), out dtTemp))
                            obj.Ngay = dtTemp;
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
