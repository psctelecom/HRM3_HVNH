using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Báo cáo danh sách thay đổi tình trạng cán bộ")]
    public class Report_ChotThongTinLuong_BaoCaoDanhSachThayDoiTinhTrangNhanVien : StoreProcedureReport
    {

        // Fields...
        private DateTime _Thang;

        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }



        public Report_ChotThongTinLuong_BaoCaoDanhSachThayDoiTinhTrangNhanVien(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Thang = HamDungChung.GetServerTime().Date;
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_ChotThongTinLuong_DanhSachThayDoiTinhTrangNhanVien");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Thang", Thang);
           

            return cmd;
        }
    }

}
