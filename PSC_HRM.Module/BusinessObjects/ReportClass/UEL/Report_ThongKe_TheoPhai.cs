using System;

using DevExpress.Xpo;

using System.Data.SqlClient;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using System.Data;
using System.Text;
using System.Collections.Generic;
using PSC_HRM.Module.Report;//

//PSC_HRM.Module.Report --> old
namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thống kê:Thống kê theo phái")]
    public class Report_ThongKe_TheoPhai : StoreProcedureReport
    {
        // Fields...-->NEU
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TatCaBoPhan", ref _TatCaBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaBoPhan")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }//<--NEU

        public Report_ThongKe_TheoPhai(Session session) : base(session) { }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd=null;
            if (TruongConfig.MaTruong.Equals("UEL"))
            {
                cmd = DataProvider.GetCommand("spd_Report_ThongKe_TheoPhai", System.Data.CommandType.StoredProcedure);
                
            }
            else
            {
                List<string> lstBP;
                if (TatCaBoPhan)
                    lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);
                else
                    lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

                StringBuilder sb = new StringBuilder();
                foreach (string item in lstBP)
                {
                    sb.Append(String.Format("{0},", item));
                }

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BoPhan", sb.ToString());

                cmd = DataProvider.GetCommand("spd_Report_ThongKe_TheoPhai",CommandType.StoredProcedure,param);
            }
            return cmd;
        }
    }

}
