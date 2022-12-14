using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp thu nhập tăng thêm")]
    public class Report_ThuNhapTangThem_BangTongHopThuNhapTangThem : StoreProcedureReport
    {
        private BangQuyetToanThuNhapTangThem _BangQuyetToanThuNhapTangThem;


        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BangQuyetToanThuNhapTangThem BangQuyetToanThuNhapTangThem
        {
            get
            {
                return _BangQuyetToanThuNhapTangThem;
            }
            set
            {
                SetPropertyValue("BangQuyetToanThuNhapTangThem", ref _BangQuyetToanThuNhapTangThem, value);
            }
        }

        public Report_ThuNhapTangThem_BangTongHopThuNhapTangThem(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThuNhapTangThem_BangTongHopThuNhapTangThem", 
                System.Data.CommandType.StoredProcedure,
                new SqlParameter("@BangQuyetToanThuNhapTangThem", BangQuyetToanThuNhapTangThem.Oid));
            return cmd;
        }
    }

}
