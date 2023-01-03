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
    [ModelDefault("Caption", "Báo cáo: Danh sách kết quả cán bộ được cử đi bồi dưỡng ngắn hạn")]
    [Appearance("Report_NhanSu_DanhSachKetQuaCanBoDuocCuDiBoiDuongNganHan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Report_NhanSu_DanhSachKetQuaCanBoDuocCuDiBoiDuongNganHan : StoreProcedureReport
    {
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        public Report_NhanSu_DanhSachKetQuaCanBoDuocCuDiBoiDuongNganHan(Session session) : base(session) { }

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


        [ModelDefault("Caption","Từ ngày")]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set { _TuNgay = value; }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set { _DenNgay = value; }
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

            SqlCommand cm = new SqlCommand("[dbo].[spd_Report_NhanSu_DanhSachCanBoDuocCuDiBoiDuongNganHan]");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@BoPhanList", sb.ToString());
            cm.Parameters.AddWithValue("@TuNgay", TuNgay);
            cm.Parameters.AddWithValue("@DenNgay", DenNgay);

            return cm;
        }
    }

}
