using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;
using System.Text;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Báo cáo thanh toán tiền lương và các khoản phụ cấp")]
    public class Report_Luong_ThanhToanTienLuongVaCacKhoanPhuCap : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;

        [ModelDefault("Caption", "Kỳ tính lương")]
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

        public Report_Luong_ThanhToanTienLuongVaCacKhoanPhuCap(Session session) : base(session) { }
         public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_Luong_ThanhToanTienLuongVaCacKhoanPhuCap", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KyTinhLuong", KyTinhLuong.Oid);
                da.Fill(DataSource);
                
            }
        }

         public override SqlCommand CreateCommand()
         {
             return null;
         }
    }

}
