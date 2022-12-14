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

namespace PSC_HRM.Module.PMS.BaoCao.SauDaiHoc
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê giờ giảng sau đại học")]
    public class Report_ThongKeGioGiang_SDH : StoreProcedureReport
    {
        private QuanLySauDaiHoc _QuanLySauDaiHoc;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Giờ giảng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn giờ giảng")]
        public QuanLySauDaiHoc QuanLySauDaiHoc
        {
            get
            {
                return _QuanLySauDaiHoc;
            }
            set
            {
                SetPropertyValue("QuanLySauDaiHoc", ref _QuanLySauDaiHoc, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        public Report_ThongKeGioGiang_SDH(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeGioGiang_SDH", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLySauDaiHoc", QuanLySauDaiHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
