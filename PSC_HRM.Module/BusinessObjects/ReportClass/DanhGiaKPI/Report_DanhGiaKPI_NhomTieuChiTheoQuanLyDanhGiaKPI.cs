using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using PSC_HRM.Module.DanhGiaKPI;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Nhóm tiêu chí theo Quản lý đánh giá KPI")]
    [Appearance("Report_DanhGiaKPI_NhomTieuChiTheoQuanLyDanhGiaKPI", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Report_DanhGiaKPI_NhomTieuChiTheoQuanLyDanhGiaKPI : StoreProcedureReport
    {
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;
        private QuanLyDanhGiaKPI _QuanLyDanhGiaKPI;

        public Report_DanhGiaKPI_NhomTieuChiTheoQuanLyDanhGiaKPI(Session session) : base(session) { }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả bộ phận")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BoPhanList")]
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
        }

        [ModelDefault("Caption", "Quản lý đánh giá KPI")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuanLyDanhGiaKPI QuanLyDanhGiaKPI
        {
            get
            {
                return _QuanLyDanhGiaKPI;
            }
            set
            {
                SetPropertyValue("QuanLyDanhGiaKPI", ref _QuanLyDanhGiaKPI, value);
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void CriteriaBoPhanList()
        {
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);
            GroupOperator go = new GroupOperator();
            go.Operands.Add(new InOperator("Oid", HamDungChung.GetCriteriaBoPhan()));

            BoPhanList.Criteria = go;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CriteriaBoPhanList();
        }
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
                sb.Append(String.Format("{0};", item));
            }

            SqlCommand cm = new SqlCommand("[dbo].[spd_Report_DanhGiaKPI_NhomTieuChiTheoQuanLyDanhGiaKPI]");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@BoPhanList", sb.ToString());
            cm.Parameters.AddWithValue("@QuanLyDanhGiaKPI", QuanLyDanhGiaKPI.Oid);

            return cm;
        }
    }

}
