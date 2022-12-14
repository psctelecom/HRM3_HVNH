using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Báo cáo danh sách bảng truy lương nhân viên")]
    [ImageName("BO_Report")]
    public class Report_TruyLuong_DanhSachBangTruyLuongNhanVien : StoreProcedureReport
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
         public Report_TruyLuong_DanhSachBangTruyLuongNhanVien(Session session) : base(session) { }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TruyLuong_DanhSachBangTruyLuongNhanVien", System.Data.CommandType.StoredProcedure, new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid));
            return cmd;
        }
    }

}
