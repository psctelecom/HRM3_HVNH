using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toán thù lao giảng dạy")]
    public class Report_PMS_ThanhToanThuLaoGiangDay_Tong : StoreProcedureReport
    {
        private string _SQL;
        [ModelDefault("Caption", "Dữ liệu in")]
        [Size(-1)]
        public string SQL
        {
            get { return _SQL; }
            set { SetPropertyValue("SQL", ref _SQL, value); }
        }

        public Report_PMS_ThanhToanThuLaoGiangDay_Tong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_ThanhToanThuLaoGiangDay_ChayStore");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sql", SQL.ToString());
            return cmd;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();         
        }
        public override void FillDataSource()
        {          
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThanhToanThuLaoGiangDay_ChayStore", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Sql1", "");
                da.SelectCommand.Parameters.AddWithValue("@Sql", SQL);
                da.Fill(DataSource);
            }
        }
        
    }
}
