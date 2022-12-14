using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: 01/ĐK-TNCN")]
    public class Report_01DK_TNCN : StoreProcedureReport
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _NhanVien;

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("BoPhan.ListThongTinNhanVien")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn cán bộ")]
        [ImmediatePostData]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        public Report_01DK_TNCN(Session session) : base(session) { }
    
        public override System.Data.SqlClient.SqlCommand  CreateCommand()
        {
 	        throw new NotImplementedException();
        }
    }  
}
