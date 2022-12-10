using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DoanDang;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Hồ sơ Đảng viên")]
    public class Report_DangVien_HoSoDangVien : StoreProcedureReport
    {
        // Fields...
        private DangVien _DangVien;
        private ToChucDang _ToChucDang;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tổ chức Đảng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ToChucDang ToChucDang
        {
            get
            {
                return _ToChucDang;
            }
            set
            {
                SetPropertyValue("ToChucDang", ref _ToChucDang, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("ToChucDang.DangVienList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DangVien DangVien
        {
            get
            {
                return _DangVien;
            }
            set
            {
                SetPropertyValue("DangVien", ref _DangVien, value);
            }
        }

        public Report_DangVien_HoSoDangVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_DangVien_HoSoDangVien", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@DangVien", DangVien.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
