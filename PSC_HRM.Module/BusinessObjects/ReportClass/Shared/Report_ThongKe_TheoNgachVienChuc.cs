using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.BusinessObjects.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Thống kê theo ngạch viên chức")]
    [Appearance("TatCaNhomNgachLuong", TargetItems = "NhomNgachLuong", Enabled = false, Criteria = "TatCaNhomNgachLuong")]
    public class Report_ThongKe_TheoNgachVienChuc : StoreProcedureReport
    {
        private DateTime _Ngay;

        [ModelDefault("Caption", "Tính đến ngày")]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
                
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Lấy ngày hiện tại
            Ngay = HamDungChung.GetServerTime();
        }

        public Report_ThongKe_TheoNgachVienChuc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Ngay", Ngay);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_TheoNgachVienChuc", System.Data.CommandType.StoredProcedure, param);
            return cmd;
        }

    }
}
