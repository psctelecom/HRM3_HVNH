using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ công tác học tập nghiên cứu")]
    public class Report_ThongKe_DanhSachCanBoCongTacHocTapNghienCuu : StoreProcedureReport
    {
        public Report_ThongKe_DanhSachCanBoCongTacHocTapNghienCuu(Session session) : base(session) { }
        private DateTime _TuNgay;
        private DateTime _DenNgay; 
        private TrinhDoChuyenMon _TrinhDoChuyenMon;

        [ModelDefault("Caption", "Từ ngày")]
        //[ModelDefault("EditMask", "MM/yyyy")]
        //[ModelDefault("DisplayFormat", "MM/yyyy")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
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

        [ModelDefault("Caption", "Trình độ")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] para = new SqlParameter[3];
            if (TuNgay == DateTime.MinValue)
                para[0] = new SqlParameter("@TuNgay", DBNull.Value);
            else
                para[0] = new SqlParameter("@TuNgay", TuNgay);

            if (DenNgay == DateTime.MinValue)
                para[1] = new SqlParameter("@DenNgay", DBNull.Value);
            else
                para[1] = new SqlParameter("@DenNgay", DenNgay);

            if (TrinhDoChuyenMon == null)
                para[2] = new SqlParameter("@TrinhDoChuyenMon", DBNull.Value);
            else
                para[2] = new SqlParameter("@TrinhDoChuyenMon", TrinhDoChuyenMon.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_DanhSachCanBoCongTacHocTapNghienCuu", System.Data.CommandType.StoredProcedure, para);
            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
