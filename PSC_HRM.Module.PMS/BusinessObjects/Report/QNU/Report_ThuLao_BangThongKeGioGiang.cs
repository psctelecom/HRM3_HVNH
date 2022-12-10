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
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thống kê giờ giảng")]
    [Appearance("BangLuong.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_ThuLao_BangThongKeGioGiang : StoreProcedureReport, IBoPhan
    {
        private KyTinhPMS _KyTinhPMS;
        private bool _TatCaDonVi = true;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Kỳ tính PMS")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhPMS KyTinhPMS
        {
            get
            {
                return _KyTinhPMS;
            }
            set
            {
                SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
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

        public Report_ThuLao_BangThongKeGioGiang(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            List<string> listBP = new List<string>();
            if (TatCaDonVi)
                listBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session);
            else
                listBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in listBP)
            {
                sb.Append(String.Format("{0};", item));
            }

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThuLao_BangThanhToanTienVuotGioDinhMuc", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KyTinhPMS", KyTinhPMS.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", sb.ToString());
                da.SelectCommand.CommandTimeout = 180;
                da.Fill(DataSource);
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //KyTinhPMS = Session.FindObject<KyTinhPMS>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=?", DateTime.Now, DateTime.Now));
        }
    }
}
