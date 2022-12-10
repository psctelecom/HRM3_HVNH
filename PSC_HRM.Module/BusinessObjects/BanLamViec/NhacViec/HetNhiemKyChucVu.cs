using DevExpress.ExpressApp;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class HetNhiemKyChucVu : INhacViec<ThongTinBoNhiem>
    {
        public List<ThongTinBoNhiem> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinBoNhiem> result = new List<ThongTinBoNhiem>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_HetNhiemKy", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinBoNhiem obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinBoNhiem>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("Oid"))
                            obj.QuyetDinh = obs.GetObjectByKey<QuyetDinhCaNhan>(new Guid(item["Oid"].ToString()));
                        if (!item.IsNull("ChucVu"))
                            obj.ChucVu = obs.GetObjectByKey<ChucVu>(new Guid(item["ChucVu"].ToString()));
                        if (!item.IsNull("KiemNhiem"))
                            obj.KiemNhiem = item["KiemNhiem"].ToString() == "0" ? false : true;
                        if (!item.IsNull("TaiBoPhan"))
                            obj.TaiBoPhan = obs.GetObjectByKey<BoPhan>(new Guid(item["TaiBoPhan"].ToString()));
                        if (!item.IsNull("NgayBoNhiem"))
                            obj.NgayBoNhiem = DateTime.Parse(item["NgayBoNhiem"].ToString());
                        if (!item.IsNull("NgayHetNhiemKy"))
                            obj.Ngay = DateTime.Parse(item["NgayHetNhiemKy"].ToString());
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
