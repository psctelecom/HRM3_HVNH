using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách bồi dưỡng theo quyết định")]
    public class Report_BoiDuong_DanhSachBoiDuongTheoQuyetDinh : StoreProcedureReport
    {
        // Fields...        
        private QuyetDinhBoiDuong _QuyetDinhBoiDuong;

        [ModelDefault("Caption", "Quyết định bồi dưỡng")]
        public QuyetDinhBoiDuong QuyetDinhBoiDuong
        {
            get
            {
                return _QuyetDinhBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoiDuong", ref _QuyetDinhBoiDuong, value);               
            }
        }

        public Report_BoiDuong_DanhSachBoiDuongTheoQuyetDinh(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];           
            parameter[0] = new SqlParameter("@QuyetDinhBoiDuong", QuyetDinhBoiDuong != null ? QuyetDinhBoiDuong.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DanhSachCanBoDiBoiDuong", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
     
           
        }
    }

}
