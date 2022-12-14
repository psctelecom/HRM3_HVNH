using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Text;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Kết quả định biên chức danh công việc")]
    [Appearance("Report_DanhSachHetHanHopDong", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Report_DinhBien_KetQuaDinhBienChucDanhTheoPhongBan : StoreProcedureReport, IBoPhan
    {
        // Fields...
        private BoPhan _BoPhan;
        private bool _TatCaBoPhan = true;

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
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria="!TatCaBoPhan")]
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

        public Report_DinhBien_KetQuaDinhBienChucDanhTheoPhongBan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            List<string> lstBP;
            if (TatCaBoPhan)
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session);
            else
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            SqlCommand cmd = new SqlCommand("spd_Report_DinhBien_KetQuaDinhBienChucDanhTheoPhongBan", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //
            cmd.Parameters.AddWithValue("@PhongBan", sb.ToString());

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
        }
    }

}
