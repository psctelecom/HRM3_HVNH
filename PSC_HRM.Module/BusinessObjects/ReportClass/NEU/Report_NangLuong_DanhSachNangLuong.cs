using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách nâng lương")]
    [Appearance("Report_NangLuong_DanhSachNangLuong", TargetItems = "QuyetDinhNangLuong", Enabled = false, Criteria = "TatCaQuyetDinh")]
    public class Report_NangLuong_DanhSachNangLuong : StoreProcedureReport
    {
        // Fields...
        private int _Nam;
        private QuyetDinh.QuyetDinhNangLuong _QuyetDinhNangLuong;
        private bool _TatCaQuyetDinh = true;     

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả quyết định")]
        public bool TatCaQuyetDinh
        {
            get
            {
                return _TatCaQuyetDinh;
            }
            set
            {
                SetPropertyValue("TatCaQuyetDinh", ref _TatCaQuyetDinh, value);
            }
        }        

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaQuyetDinh")]
        [ModelDefault("Caption", "Quyết định nâng lương")]
        public QuyetDinh.QuyetDinhNangLuong QuyetDinhNangLuong
        {
            get
            {
                return _QuyetDinhNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangLuong", ref _QuyetDinhNangLuong, value);
                Nam = QuyetDinhNangLuong.NgayHieuLuc.Year;
            }
        }
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
              
            }
        }

        public Report_NangLuong_DanhSachNangLuong(Session session) : base(session) { }


        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@Nam", Nam);
            parameter[1] = new SqlParameter("@QuyetDinhNangLuong", QuyetDinhNangLuong != null ? QuyetDinhNangLuong.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_NangLuong_DanhSachNangLuong", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();    
           
        }
    }

}
