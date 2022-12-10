using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class HetHanNghiKhongHuongLuong : INhacViec<ThongTinNghiKhongHuongLuong>
    {
        public List<ThongTinNghiKhongHuongLuong> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinNghiKhongHuongLuong> result = new List<ThongTinNghiKhongHuongLuong>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_HetHanNghiKhongLuong", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinNghiKhongHuongLuong obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinNghiKhongHuongLuong>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("Oid"))
                            obj.QuyetDinhNghiKhongHuongLuong = obs.GetObjectByKey<QuyetDinhNghiKhongHuongLuong>(new Guid(item["Oid"].ToString()));
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
