using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.KhenThuong;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách thi đua khen thưởng theo năm, theo danh hiệu")]
    public class Report_DanhSachKhenThuongTheoNam_DanhHieu : StoreProcedureReport
    {
        public enum TuyChonEnum
        {
            [DevExpress.Xpo.DisplayName("Cán bộ")]
            CanBo = 0,
            [DevExpress.Xpo.DisplayName("Đơn vị")]
            DonVi = 1
        }

        private TuyChonEnum _TuyChon;
        private int _Nam;
        private DanhHieuKhenThuong _DanhHieu;

        [ModelDefault("Caption", "Tuỳ chọn")]
        public TuyChonEnum TuyChon
        {
            get
            {
                return _TuyChon;
            }
            set
            {
                SetPropertyValue("TuyChon", ref _TuyChon, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập năm")]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập danh hiệu", TargetCriteria = "TuyChon=0")]
        public DanhHieuKhenThuong DanhHieu
        {
            get
            {
                return _DanhHieu;
            }
            set
            {
                SetPropertyValue("DanhHieu", ref _DanhHieu, value);
            }
        }

        public Report_DanhSachKhenThuongTheoNam_DanhHieu(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = DateTime.Today.Year;
        }

        public override void FillDataSource()
        {
            base.FillDataSource();

            SqlDataAdapter da;

            if (TuyChon == TuyChonEnum.CanBo)
                da = new SqlDataAdapter("spd_Report_DanhSachKhenThuongTheoNam_DanhHieu", (SqlConnection)Session.Connection);
            else
                da = new SqlDataAdapter("spd_Report_DanhSachKhenThuongTheoNam_DanhHieu_DonVi", (SqlConnection)Session.Connection);

            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@Nam", Nam);
            if (DanhHieu == null)
                da.SelectCommand.Parameters.AddWithValue("@DanhHieu", DBNull.Value);
            else
                da.SelectCommand.Parameters.AddWithValue("@DanhHieu", DanhHieu.Oid);

            da.Fill(DataSource);
        }
    }

}
