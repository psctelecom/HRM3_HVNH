using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Giáo vụ phí")]
    public class Report_GiaoVuPhi : StoreProcedureReport
    {
        private HocKy _HocKy;
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn năm học")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn học kỳ")]
         public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
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

        public Report_GiaoVuPhi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_GiaoVuPhi", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@ThongTinTruong", ThongTinTruong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                da.Fill(DataSource);
            }
        }
    }

}
