using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using PSC_HRM.Module.BaoMat;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tính truy lĩnh cho viên chức và người lao động")]
    [Appearance("BangLuong.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_TruyLuong_BangTruyLinhVienChuc : StoreProcedureReport
    {
        // Fields...
        private BangTruyLuongNew _BangTruyLuong;
        private bool _TatCaDonVi = true;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Bảng truy lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BangTruyLuongNew BangTruyLuong
        {
            get
            {
                return _BangTruyLuong;
            }
            set
            {
                SetPropertyValue("BangTruyLuong", ref _BangTruyLuong, value);
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

        public Report_TruyLuong_BangTruyLinhVienChuc(Session session) : base(session) { }

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

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_TruyLuong_BangTruyLinhVienChuc", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangTruyLuong", BangTruyLuong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", sb.ToString());
                da.Fill(DataSource);
            }
        }
    }

}
