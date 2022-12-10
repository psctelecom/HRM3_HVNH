using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách ứng tuyển ngạch chuyên viên")]
    public class Report_TuyenDung_DanhSachChuyenVien : StoreProcedureReport
    {
        private ChiTietTuyenDung _ChiTietTuyenDung;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private VongTuyenDung _VongTuyenDung;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đợt tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListChiTietTuyenDung")]
        public ChiTietTuyenDung ChiTietTuyenDung
        {
            get
            {
                return _ChiTietTuyenDung;
            }
            set
            {
                SetPropertyValue("ChiTietTuyenDung", ref _ChiTietTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vòng tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("ChiTietTuyenDung.ListVongTuyenDung")]
        public VongTuyenDung VongTuyenDung
        {
            get
            {
                return _VongTuyenDung;
            }
            set
            {
                SetPropertyValue("VongTuyenDung", ref _VongTuyenDung, value);
            }
        }

        public Report_TuyenDung_DanhSachChuyenVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@NamHoc", QuanLyTuyenDung.NamHoc.Oid);
            param[1] = new SqlParameter("@Dot", QuanLyTuyenDung.DotTuyenDung);
            param[2] = new SqlParameter("@VongTuyenDung", VongTuyenDung.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TuyenDung_DanhSachChuyenVien",
                System.Data.CommandType.StoredProcedure, param);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }
    }

}
