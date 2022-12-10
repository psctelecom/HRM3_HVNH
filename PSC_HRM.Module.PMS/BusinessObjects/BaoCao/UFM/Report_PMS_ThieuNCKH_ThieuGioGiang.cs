using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Giảng viên chưa hoàn thành định mức Giờ giảng _ NCKH")]
    public class Report_PMS_ThieuNCKH_ThieuGioGiang : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private BoPhanView _BoPhan;
        private BoPhan _BoPhanFull;
        private NamHoc _NamHoc;


        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
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
        [ModelDefault("Caption", "Bộ phận")]
        //[RuleRequiredField("", DefaultContexts.Save, "Chưa chọn Bộ phận")]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public BoPhanView BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("NhanVien", ref _BoPhan, value);
                if (!IsLoading)
                    if (BoPhan != null)
                    {
                        BoPhanFull = Session.FindObject<BoPhan>(CriteriaOperator.Parse("Oid =?", BoPhan.OidBoPhan.ToString()));                      
                    }

            }
        }
        [ModelDefault("Caption", "Bộ phận")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public BoPhan BoPhanFull
        {
            get { return _BoPhanFull; }
            set { SetPropertyValue("BoPhanFull", ref _BoPhanFull, value); }
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
        [Browsable(false)]
        public XPCollection<BoPhanView> listbp { get; set; }
        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            listbp = HamDungChung.getBoPhan(Session);
            OnChanged("listbp");
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_ThieuGioNCKHVaGioGiang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhanFull != null ? BoPhanFull.Oid : Guid.Empty);
                
                da.Fill(DataSource);
            }
        }
        public Report_PMS_ThieuNCKH_ThieuGioGiang(Session session) : base(session) { }
    }
}
