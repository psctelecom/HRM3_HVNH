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
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.BaoCao.SauDaiHoc
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê tổng hợp giờ giảng sau đại học")]
    public class Report_ThongKeTongHopGioGiang_SDH : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BoPhan _BoPhan;

        private NhanVien _NhanVienFull;

        #region Thong tin chính
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
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save, "Chọn năm học")]
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
        #endregion

        #region Đơn vị
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                    if (BoPhan != null)
                        updateNV();
            }
        }
        #endregion
        void updateNV()
        {
            listnv = new XPCollection<NhanVien>(Session, CriteriaOperator.Parse("BoPhan =?", BoPhan.Oid));
            OnChanged("listnv");
        }
        #region Cán bộ
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("listnv", DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVienFull
        {
            get { return _NhanVienFull; }
            set { SetPropertyValue("NhanVienFull", ref _NhanVienFull, value); }
        }
        #endregion

        public Report_ThongKeTongHopGioGiang_SDH(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listnv = new XPCollection<NhanVien>(Session, false);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
        [Browsable(false)]
        public XPCollection<NhanVien> listnv { get; set; }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeTongHopGioGiang_SDH", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVienFull != null ? NhanVienFull.Oid : Guid.Empty);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                da.Fill(DataSource);
            }
        }
    }
}
