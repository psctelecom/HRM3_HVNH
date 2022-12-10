using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ nghỉ hưu từ tháng đến tháng")]
    public class Report_ThongKe_DanhSachCanBoNghiHuuTuThangDenThang : StoreProcedureReport
    {
        private DateTime _TuThang;
        private DateTime _DenThang;

        [ModelDefault("Caption", "Từ tháng")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        public Report_ThongKe_DanhSachCanBoNghiHuuTuThangDenThang(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKe_DanhSachCanBoNghiHuuTuThangDenThang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@TuThang", TuThang.SetTime(SetTimeEnum.StartMonth));
                da.SelectCommand.Parameters.AddWithValue("@DenThang", DenThang.SetTime(SetTimeEnum.EndMonth).AddDays(1));
                da.SelectCommand.CommandTimeout = 180;
                da.Fill(DataSource);
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
