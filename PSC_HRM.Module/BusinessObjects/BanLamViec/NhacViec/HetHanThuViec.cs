using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.ThuViec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class HetHanThuViec : INhacViec<ThongTinHetHanThuViec>
    {
        public List<ThongTinHetHanThuViec> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinHetHanThuViec> result = new List<ThongTinHetHanThuViec>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_ThuViec", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinHetHanThuViec obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinHetHanThuViec>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("HopDongHienTai"))
                            obj.HopDongHienTai = obs.GetObjectByKey<HopDong_LaoDong>(new Guid(item["HopDongHienTai"].ToString()));
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
