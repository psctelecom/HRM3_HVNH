using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalEditorState;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DungChung;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [Appearance("Report_Param_NhanVienTheoHinhThucHopDong", "HinhThucHopDong", Enabled = false, "TatCaHinhThucHopDong", DevExpress.ExpressApp.ViewType.DetailView)]
    [ModelDefault("Caption","Báo cáo :Danh sách CBCNV theo hình thức hợp đồng")]
    public class Report_Param_DanhSachNhanVienTheoHinhThucHopDong : StoreProcedureReport
    {
        public Report_Param_DanhSachNhanVienTheoHinhThucHopDong(Session session) : base(session) { }

        private bool _TatCaHinhThucHopDong =true;
        private HinhThucHopDong _HinhThucHopDong;
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả hình thức hợp đồng")]
        public bool TatCaHinhThucHopDong
        {
            get
            {
                return _TatCaHinhThucHopDong;
            }
            set
            {
                SetPropertyValue("TatCaHinhThucHopDong", ref _TatCaHinhThucHopDong, value);
            }
        }

        [ModelDefault("Caption", "Hình thức hợp đồng")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="!TatCaHinhThucHopDong")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                return _HinhThucHopDong;
            }
            set
            {
                SetPropertyValue("HinhThucHopDong", ref _HinhThucHopDong, value);
            }
        }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd;
            SqlParameter param;
            if (TatCaHinhThucHopDong)
                param = new SqlParameter("@HinhThucHopDong", DBNull.Value);
            else
                param = new SqlParameter("@HinhThucHopDong", HinhThucHopDong.Oid);

            cmd = DataProvider.GetCommand("spd_Report_Param_NhanVienTheoHinhThucHopDong", System.Data.CommandType.StoredProcedure, param);
            return cmd;
        }
    }

}
