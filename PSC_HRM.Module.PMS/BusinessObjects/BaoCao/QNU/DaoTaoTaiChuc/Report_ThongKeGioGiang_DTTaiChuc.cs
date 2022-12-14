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
using PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen;

namespace PSC_HRM.Module.PMS.BaoCao.DaoTaoChinhQuy
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê giờ giảng đào tạo tại chức")]
    public class Report_ThongKeGioGiang_DTTaiChuc : StoreProcedureReport
    {
        private QuanLyBoiDuongThuongXuyen _QuanLyBoiDuongThuongXuyen;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Quản lý bồi dưỡng thường xuyên")]
        [RuleRequiredField("", DefaultContexts.Save, "Quản lý bồi dưỡng thường xuyên không rỗng")]
        public QuanLyBoiDuongThuongXuyen QuanLyBoiDuongThuongXuyen
        {
            get
            {
                return _QuanLyBoiDuongThuongXuyen;
            }
            set
            {
                SetPropertyValue("QuanLyBoiDuongThuongXuyen", ref _QuanLyBoiDuongThuongXuyen, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        public Report_ThongKeGioGiang_DTTaiChuc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeGioGiang_BoiDuongThuongXuyen", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLyBoiDuongThuongXuyen", QuanLyBoiDuongThuongXuyen.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
