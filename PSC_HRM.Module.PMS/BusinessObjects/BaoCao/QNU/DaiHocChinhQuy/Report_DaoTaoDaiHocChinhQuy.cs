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
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Đào tạo đại học chính quy")]
    public class Report_DaoTaoDaiHocChinhQuy : StoreProcedureReport
    {
        private KyTinhPMS _KyTinhPMS;
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

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
        [ImmediatePostData]
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
                if (!IsLoading)
                    if (NamHoc != null)
                        updateKyPMS();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính thù lao")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn kỳ tính")]
        [DataSourceProperty("KyTinhPMSList")]
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
        [Browsable(false)]
        [ModelDefault("Caption", "Kỳ PMS List")]
        public XPCollection<KyTinhPMS> KyTinhPMSList
        {
            get;
            set;
        }
        void updateKyPMS()
        {
            if (NamHoc != null)
            {
                KyTinhPMSList = new XPCollection<KyTinhPMS>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
            }
            else
                KyTinhPMSList = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            OnChanged("KyTinhPMSList");
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                UpdateNhanVienList();
            }
        }
        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("NhanVienList", DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        public Report_DaoTaoDaiHocChinhQuy(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            UpdateNhanVienList(); 
        }
        [Browsable(false)]
        public XPCollection<NhanVien> NhanVienList { get; set; }

        public void UpdateNhanVienList()
        {
            NhanVienList = new XPCollection<NhanVien>(Session);

            if (BoPhan != null)
                NhanVienList.Criteria = CriteriaOperator.Parse("BoPhan = ?", BoPhan.Oid);
            OnChanged("NhanVienList");
        }
        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_DaoTaoDaiHocChinhQuy", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KyTinhPMS", KyTinhPMS.Oid);
                da.SelectCommand.Parameters.AddWithValue("@ThongTinTruong", ThongTinTruong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                da.Fill(DataSource);
            }
        }
    }

}
