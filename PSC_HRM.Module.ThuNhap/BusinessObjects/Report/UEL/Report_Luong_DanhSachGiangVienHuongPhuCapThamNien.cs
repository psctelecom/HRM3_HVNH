using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách giảng viên hưởng phụ cấp thâm niên")]
    public class Report_Luong_DanhSachGiangVienHuongPhuCapThamNien : StoreProcedureReport
    {
        // Fields...
        private ChungTu.ChungTu _ChungTu;

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        public Report_Luong_DanhSachGiangVienHuongPhuCapThamNien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_Luong_DanhSachGiangVienHuongPhuCapThamNien", 
                (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ChungTu", ChungTu.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
