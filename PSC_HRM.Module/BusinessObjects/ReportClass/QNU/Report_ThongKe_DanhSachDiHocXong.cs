using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ và viên chức đi học xong")]
    [Appearance("Report_ThongKe_DSDangDiHoc.TatCaTrinhDo", TargetItems = "TrinhDo", Enabled = false, Criteria = "TatCaTrinhDo")]
    public class Report_ThongKe_DanhSachDiHocXong : StoreProcedureReport
    {
        // Fields...
        private DateTime _DenNgay;
        private TrinhDoChuyenMon _TrinhDo;
        private bool _TatCaTrinhDo = true;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả trình độ")]
        public bool TatCaTrinhDo
        {
            get
            {
                return _TatCaTrinhDo;
            }
            set
            {
                SetPropertyValue("TatCaTrinhDo", ref _TatCaTrinhDo, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaTrinhDo")]
        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDo
        {
            get
            {
                return _TrinhDo;
            }
            set
            {
                SetPropertyValue("TrinhDo", ref _TrinhDo, value);
            }
        }

        public Report_ThongKe_DanhSachDiHocXong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            DenNgay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@DenNgay", DenNgay);
            parameter[1] = new SqlParameter("@TrinhDo", TrinhDo != null ? TrinhDo.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_DSDiHocXong", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }

    }
}
