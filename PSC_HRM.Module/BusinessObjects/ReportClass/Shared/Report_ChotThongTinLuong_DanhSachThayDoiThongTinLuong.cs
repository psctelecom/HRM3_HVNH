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
    [ModelDefault("Caption", "Báo cáo: Danh sách thay đổi thông tin lương")]
    public class Report_ChotThongTinLuong_DanhSachThayDoiThongTinLuong : StoreProcedureReport
    {
        // Fields...
        private DateTime _ThangTruoc;
        private DateTime _ThangHienTai;

        [ModelDefault("Caption", "Tháng trước")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime ThangTruoc
        {
            get
            {
                return _ThangTruoc;
            }
            set
            {
                SetPropertyValue("ThangTruoc", ref _ThangTruoc, value);
            }
        }

        [ModelDefault("Caption", "Tháng hiện tại")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime ThangHienTai
        {
            get
            {
                return _ThangHienTai;
            }
            set
            {
                SetPropertyValue("ThangHienTai", ref _ThangHienTai, value);
            }
        }

        public Report_ChotThongTinLuong_DanhSachThayDoiThongTinLuong(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ThangTruoc = HamDungChung.GetServerTime().Date.AddMonths(-1);
            ThangHienTai = HamDungChung.GetServerTime().Date;
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_ChotThongTinLuong_DanhSachThayDoiThongTinLuong");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ThangTruoc", ThangTruoc);
            cmd.Parameters.AddWithValue("@ThangHienTai", ThangHienTai);
            //
            return cmd;
        }
    }

}
