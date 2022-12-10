using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thục hiện chế độ phụ cấp vượt khung cho CBVC")]
    public class Report_DanhSach_ThucHienCheDoPhuCapThamNienVuotKhungDoiVoiCBVC : StoreProcedureReport
    {
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Thời điểm thống kê")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Display", "dd/MM/yyyy")]
        [ModelDefault("EditMark", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }
        public Report_DanhSach_ThucHienCheDoPhuCapThamNienVuotKhungDoiVoiCBVC(Session session) : base(session) { }

        public override void FillDataSource()
        {
            DateTime TuNgay = HamDungChung.SetTime(DenNgay, 4);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNgay", HamDungChung.SetTime(TuNgay, 0));
            param[1] = new SqlParameter("@DenNgay", HamDungChung.SetTime(DenNgay, 1));
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DanhSach_ThucHienCheDoPhuCapThamNienVuotKhungDoiVoiCBVC", System.Data.CommandType.StoredProcedure, param);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }
        public override SqlCommand CreateCommand()
        {
            return null;
        }
    }

}
