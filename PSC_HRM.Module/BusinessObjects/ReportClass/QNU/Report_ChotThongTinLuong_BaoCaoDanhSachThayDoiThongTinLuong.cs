using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách thay đổi thông tin lương so với tháng trước")]
    public class Report_ChotThongTinLuong_BaoCaoDanhSachThayDoiThongTinLuong : StoreProcedureReport
    {
        

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


        public Report_ChotThongTinLuong_BaoCaoDanhSachThayDoiThongTinLuong (Session session) : base(session) { }
        //public override SqlCommand CreateCommand()
        //{
        //    return null;
        //}

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_ChotThongTinLuong_BaoCaoDanhSachThayDoiThongTinLuong");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Thang", Thang);

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Thang = HamDungChung.GetServerTime().Date;
           
        }
    }
}
