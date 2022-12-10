using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Biến động bảo hiểm")]
   
    public class Report_BienDongBaoHiem : StoreProcedureReport
    {

      
     
        public Report_BienDongBaoHiem(Session session) : base(session) { }
  
        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter cmd = new SqlDataAdapter("spd_Report_BaoHiem_BienDongBaoHiem", (SqlConnection)Session.Connection);
            cmd.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Fill(DataSource);
        }
      

    }

        
    }

