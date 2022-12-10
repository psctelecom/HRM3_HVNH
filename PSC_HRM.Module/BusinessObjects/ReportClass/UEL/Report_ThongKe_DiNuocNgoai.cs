using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Thống kê đi nước ngoài")]
    public class Report_ThongKe_DiNuocNgoai : StoreProcedureReport
    {
        // Fields...
        private QuocGia _QuocGia;
        private int _DenNam;
        private int _TuNam;

        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("Caption", "Từ năm")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("Caption", "Đến năm")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                if (value == null ||
                    (value != null
                    && value.Oid != Guid.Empty))
                    SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        public Report_ThongKe_DiNuocNgoai(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_ThongKe_DiNuocNgoai", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TuNam", TuNam);
            cmd.Parameters.AddWithValue("@DenNam", DenNam);
            if (QuocGia == null)
                cmd.Parameters.AddWithValue("@QuocGia", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@QuocGia", QuocGia.Oid);

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            TuNam = current.Year - 1;
            DenNam = current.Year;
        }
    }

}
