using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap.Thuong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp khen thưởng phúc lợi")]
    public class Report_Thuong_BangTongHopKhenThuong : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        public Report_Thuong_BangTongHopKhenThuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_Thuong_BangTongHopKhenThuong", 
                System.Data.CommandType.StoredProcedure, 
                param);
            return cmd;
        }
    }

}
