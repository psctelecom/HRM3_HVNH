using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê thi đua khen thưởng (cá nhân)")]
    [ImageName("BO_Report")]
    public class Report_ThongKeThiDuaKhenThuongCaNhan : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _CanBo;

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

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn cán bộ")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        public ThongTinNhanVien CanBo
        {
            get
            {
                return _CanBo;
            }
            set
            {
                SetPropertyValue("CanBo", ref _CanBo, value);
            }
        }

        public Report_ThongKeThiDuaKhenThuongCaNhan(Session session) : base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeThiDuaKhenThuongCaNhan", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CanBo", CanBo.Oid);

            da.Fill(DataSource);
        }
    }

}
