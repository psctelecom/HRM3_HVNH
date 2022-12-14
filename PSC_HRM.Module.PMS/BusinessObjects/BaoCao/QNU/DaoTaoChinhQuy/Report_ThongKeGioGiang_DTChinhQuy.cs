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
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.BaoCao.DaoTaoChinhQuy
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê giờ giảng đại học chính quy")]
    public class Report_ThongKeGioGiang_DTChinhQuy : StoreProcedureReport
    {
        private KhoiLuongGiangDay _KhoiLuongGiangDay;
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        [RuleRequiredField("", DefaultContexts.Save, "Khối lượng giảng dạy không rỗng")]
        public KhoiLuongGiangDay KhoiLuongGiangDay
        {
            get
            {
                return _KhoiLuongGiangDay;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay", ref _KhoiLuongGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("NhanVienList", DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
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

        public Report_ThongKeGioGiang_DTChinhQuy(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
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
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeGioGiang_DaiHocChinhQuy", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KhoiLuongGiangDay", KhoiLuongGiangDay.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.Fill(DataSource);
            }
        }
    }
}
