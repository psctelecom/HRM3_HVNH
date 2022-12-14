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
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Chi tiết lương ngoài giờ")]
    [ImageName("BO_Report")]
    public class Report_NgoaiGio_ChiTietLuongNgoaiGio : StoreProcedureReport
    { 
          
        private KyTinhLuong _KyTinhLuong;
         
         [ModelDefault("Caption","Kỳ tính lương")]
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
        public Report_NgoaiGio_ChiTietLuongNgoaiGio(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_NgoaiGio_ChiTietLuongNgoaiGio", System.Data.CommandType.StoredProcedure, param);
            return cmd;
        }
    }

}
