using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Báo cáo danh sách CBCNV nộp phí công đoàn")]
    [ImageName("BO_Report")]
    [Appearance("Report_KhauTru_DanhSachCanBoCongNhanVienNopPhiCongDoan.DonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]

    public class Report_KhauTru_DoanPhi : StoreProcedureReport, IBoPhan
    {
        private KyTinhLuong _KyTinhLuong;
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn kỳ tính lương")]
        [ImmediatePostData]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả bộ phận")]
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TaCaBoPhan", ref _TatCaBoPhan, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaBoPhan")]
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

        public Report_KhauTru_DoanPhi(Session session) : 
            base(session) 
        { }

        public override void FillDataSource()
        {
            List<string> lsBoPhan;
            if (TatCaBoPhan)
                lsBoPhan = HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session);
            else
                lsBoPhan = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);
            StringBuilder sb = new StringBuilder();
            foreach (string lsBP in lsBoPhan)
            {
                sb.Append(String.Format("{0},", lsBP));
            }
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_KhauTru_DanhSachCBCNVNopPhiCongDoan", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KyTinhLuong", KyTinhLuong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", sb.ToString());
                da.Fill(DataSource);

            }
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
    }

}
