using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

//PSC_HRM.Module.Report --> old
namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê số lượng cán bộ, giảng viên, nhân viên")]
    public class Report_ThongKe_SoLuongCanBoGiangVienNhanVien : StoreProcedureReport
    {
        // Fields...
        private DateTime _NgayGui;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Ngày gửi")]
        public DateTime NgayGui
        {
            get
            {
                return _NgayGui;
            }
            set
            {
                SetPropertyValue("NgayGui", ref _NgayGui, value);
            }
        }

        public Report_ThongKe_SoLuongCanBoGiangVienNhanVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd=null;
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                cmd = new SqlCommand("spd_Report_ThongKe_SoLuongCanBoGiangVienNhanVien");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ngay", NamHoc.NgayBatDau);
            }
            else if (TruongConfig.MaTruong.Equals("UEL"))
            {
                cmd = new SqlCommand("spd_Report_ThongKe_5_1_DHCBGV");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
            }
            return cmd;
        }

        //public override void FillDataSource()
        //{
        //    if (TruongConfig.MaTruong.Equals("UEL"))
        //    {
        //        using (SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_5_1_DHCBGV", System.Data.CommandType.StoredProcedure, new SqlParameter("@NamHoc", NamHoc.Oid)))
        //        {
        //            cmd.Connection = (SqlConnection)Session.Connection;
        //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //            {
        //                da.Fill(DataSource);
        //            }
        //        }
        //    }
        //}

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayGui = HamDungChung.GetServerTime();
        }
    }

}
