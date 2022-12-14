using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using System.ComponentModel;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Chi tiết thu nhập cá nhân")]
    public class Report_Luong_ChiTietThuNhapCaNhan : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _NhanVien;
        private ChungTu.ChungTu _ChungTu;

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        public Report_Luong_ChiTietThuNhapCaNhan(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_Luong_ChiTietThuNhapCaNhan", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ChungTu", ChungTu.Oid);
                da.SelectCommand.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);
                
                da.Fill(DataSource);
            }
        }
    }

}
