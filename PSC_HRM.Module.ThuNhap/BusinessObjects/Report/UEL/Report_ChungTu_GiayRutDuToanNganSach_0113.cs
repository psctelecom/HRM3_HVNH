using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Giấy rút dự toán ngân sách (0113)")]
    public class Report_ChungTu_GiayRutDuToanNganSach_0113 : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn kỳ tính lương")]
         public KyTinhLuong  KyTinhLuong
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

        public Report_ChungTu_GiayRutDuToanNganSach_0113(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ChungTu_GiayRutDuToanNganSach_0113", 
                System.Data.CommandType.StoredProcedure,
                param);
            return cmd;
        }
    }

}
