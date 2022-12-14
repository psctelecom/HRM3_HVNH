using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Thù lao nhân viên")]
    [ImageName("BO_Report")]
    public class Report_ThuLao_DanhSachBangThuLaoNhanVien : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;
         [ModelDefault("Caption","Kỳ tính lương")]
        [RuleRequiredField("",DefaultContexts.Save,"Chưa chọn kỳ tính lương")]
        [ImmediatePostData]
         public KyTinhLuong  KyTinhLuong
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
         public Report_ThuLao_DanhSachBangThuLaoNhanVien(Session session) : base(session) { }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThuLao_BangThuLaoNhanVien", System.Data.CommandType.StoredProcedure, param);
            return cmd;
        }
    }

}
