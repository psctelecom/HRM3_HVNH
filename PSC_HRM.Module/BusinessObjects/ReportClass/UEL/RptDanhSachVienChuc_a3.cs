using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo danh sách viên chức Mẫu a3")]
    [Appearance("RptDanhSachVienChuc_a3.TatCaBoPhan", TargetItems =  "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class RptDanhSachVienChuc_a3 : StoreProcedureReport, IBoPhan
    {
        public RptDanhSachVienChuc_a3(Session session) : base(session) { }

        private bool _TatCaBoPhan = true;
        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()] //Load dữ liệu ngay lập tức
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

        private BoPhan _BoPhan;
        [ModelDefault("Caption", "Đơn vị")]
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
        }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_DanhSachVienChuc_Mau3a");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);

            return cm;
        }
    }

}
