using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Danh sách tạm ứng theo tháng")]
    [ImageName("BO_Report")]
    public class Report_TamUng_DanhSachTamUngTheoThang : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn kỳ tính lương")]
        [ImmediatePostData]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        public Report_TamUng_DanhSachTamUngTheoThang(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TamUng_DanhSachTamUngTheoThang", System.Data.CommandType.StoredProcedure, new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid));
            return cmd;
        }
    }

}
