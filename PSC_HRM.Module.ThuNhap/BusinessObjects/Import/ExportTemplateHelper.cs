using PSC_HRM.Module;
using System;
using System.Collections.Generic;
using System.Data;

namespace PSC_HRM.Module.ThuNhap.Import
{
    public static class ExportTemplateHelper
    {
        /// <summary>
        /// Cái này chưa phân quyền đơn vị
        /// </summary>
        /// <returns></returns>
        public static List<ExportItem> LoadData()
        {
            List<ExportItem> result = new List<ExportItem>();
            using (DataTable dt = DataProvider.GetDataTable("spd_System_DanhSachNhanVien", CommandType.StoredProcedure))
            {
                ExportItem ei;
                foreach (DataRow item in dt.Rows)
                {
                    ei = new ExportItem()
                    {
                        SoHieuCongChuc = item["MaQuanLy"].ToString(),
                        Ho = item["Ho"].ToString(),
                        Ten = item["Ten"].ToString(),
                        BoPhan = item["TenBoPhan"].ToString()
                    };

                    result.Add(ei);
                }
            }
            return result;
        }
    }
}
