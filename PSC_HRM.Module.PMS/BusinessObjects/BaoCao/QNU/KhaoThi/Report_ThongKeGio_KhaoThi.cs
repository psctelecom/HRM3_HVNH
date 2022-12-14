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
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;

namespace PSC_HRM.Module.PMS.BaoCao.KhaoThi
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê chấm giữa kỳ, ra đề, coi thi")]
    public class Report_ThongKeGio_KhaoThi : StoreProcedureReport
    {
        private QuanLyKhaoThi _QuanLyKhaoThi;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Quản lý khảo thí")]
        [RuleRequiredField("", DefaultContexts.Save, "Quản lý khảo thí không rỗng")]
        public QuanLyKhaoThi QuanLyKhaoThi
        {
            get
            {
                return _QuanLyKhaoThi;
            }
            set
            {
                SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        public Report_ThongKeGio_KhaoThi(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeGio_QuanLyKhaoThi", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLyKhaoThi", QuanLyKhaoThi.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
