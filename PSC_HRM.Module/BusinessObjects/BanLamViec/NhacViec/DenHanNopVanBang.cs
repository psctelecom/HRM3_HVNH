using DevExpress.ExpressApp;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSC_HRM.Module.BanLamViec
{
    public class DenHanNopVanBang : INhacViec<ThongTinDaoTao>
    {
        public List<ThongTinDaoTao> GetData(IObjectSpace obs, params System.Data.SqlClient.SqlParameter[] param)
        {
            List<ThongTinDaoTao> result = new List<ThongTinDaoTao>();
            using (DataTable dt = DataProvider.GetDataTable("spd_Notification_DenHanNopVanBang", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ThongTinDaoTao obj;
                    foreach (DataRow item in dt.Rows)
                    {
                        obj = obs.CreateObject<ThongTinDaoTao>();
                        if (!item.IsNull("ThongTinNhanVien"))
                            obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                        if (!item.IsNull("QuyetDinhDaoTao"))
                            obj.QuyetDinhDaoTao = obs.GetObjectByKey<QuyetDinhDaoTao>(new Guid(item["QuyetDinhDaoTao"].ToString()));
                        if (!item.IsNull("HanNopVanBang"))
                            obj.Ngay = DateTime.Parse(item["HanNopVanBang"].ToString());
                        if (!item.IsNull("TrinhDoChuyenMon"))
                            obj.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(new Guid(item["TrinhDoChuyenMon"].ToString()));
                        if (!item.IsNull("ChuyenMonDaoTao"))
                            obj.ChuyenMonDaoTao = obs.GetObjectByKey<ChuyenMonDaoTao>(new Guid(item["ChuyenMonDaoTao"].ToString()));
                        if (!item.IsNull("TruongDaoTao"))
                            obj.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(new Guid(item["TruongDaoTao"].ToString()));
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
