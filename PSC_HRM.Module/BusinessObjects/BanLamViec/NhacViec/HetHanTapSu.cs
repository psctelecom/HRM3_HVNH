using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.TapSu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class HetHanTapSu : INhacViec<ThongTinHetHanTapSu>
    {
        public List<ThongTinHetHanTapSu> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinHetHanTapSu> result = new List<ThongTinHetHanTapSu>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_TapSu", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinHetHanTapSu obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinHetHanTapSu>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("QuyetDinhHuongDanTapSu"))
                            obj.QuyetDinhHuongDanTapSu = obs.GetObjectByKey<QuyetDinhHuongDanTapSu>(new Guid(item["QuyetDinhHuongDanTapSu"].ToString()));
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
