using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Kết quả nâng bậc lương đối với CBVC")]
    public class Report_DanhSach_NangBacLuongDoiVoiCBVC : StoreProcedureReport
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
        public Report_DanhSach_NangBacLuongDoiVoiCBVC(Session session) : base(session) { }

        public override void FillDataSource()
        {
            DateTime TuNgay = HamDungChung.SetTime(DenNgay, 4);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNgay", HamDungChung.SetTime(TuNgay, 0));
            param[1] = new SqlParameter("@DenNgay", HamDungChung.SetTime(DenNgay, 1));
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DanhSach_NangBacLuongDoiVoiCBVC", System.Data.CommandType.StoredProcedure, param);
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
