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
    [ModelDefault("Caption", "Báo cáo: Kết quả đánh giá KPI bộ phận")]
    public class Report_DanhGiaKPI_KetQuaDanhGiaBoPhan : StoreProcedureReport
    {
        private BoPhan _BoPhan;
        private QuanLyDanhGiaKPI _QuanLyDanhGiaKPI;
        private VongDanhGia _VongDanhGia;

        public Report_DanhGiaKPI_KetQuaDanhGiaBoPhan(Session session) : base(session) { }

        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BoPhanList")]
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

        [ModelDefault("Caption", "Vòng đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public VongDanhGia VongDanhGia
        {
            get
            {
                return _VongDanhGia;
            }
            set
            {
                SetPropertyValue("VongDanhGia", ref _VongDanhGia, value);
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
            SqlCommand cm = new SqlCommand("[dbo].[spd_Web1_DanhGiaKPI_LayKetQuaDanhGia_ByBoPhan]");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            cm.Parameters.AddWithValue("@QuanLyDanhGiaKPI", QuanLyDanhGiaKPI.Oid);
            cm.Parameters.AddWithValue("@VongDanhGia", VongDanhGia.Oid);

            return cm;
        }
    }

}
