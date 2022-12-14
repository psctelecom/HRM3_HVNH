using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ThuNhap.ChungTu;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: So sánh chứng từ")]
    public class Report_ChungTu_SoSanhChungTu : StoreProcedureReport
    {
        private ChuyenKhoanLuongNhanVien _ChungTuCu;
        private ChuyenKhoanLuongNhanVien _ChungTuMoi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Chứng từ cũ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn chứng từ")]
        public ChuyenKhoanLuongNhanVien ChungTuCu
        {
            get
            {
                return _ChungTuCu;
            }
            set
            {
                SetPropertyValue("ChungTuCu", ref _ChungTuCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chứng từ mới")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn chứng từ")]
        public ChuyenKhoanLuongNhanVien ChungTuMoi
        {
            get
            {
                return _ChungTuMoi;
            }
            set
            {
                SetPropertyValue("ChungTuMoi", ref _ChungTuMoi, value);
            }
        }

        public Report_ChungTu_SoSanhChungTu(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChungTuCu", ChungTuCu.Oid);
            param[1] = new SqlParameter("@ChungTu", ChungTuMoi.Oid);


            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ChungTu_SoSanhChungTu",
                System.Data.CommandType.StoredProcedure,
                param);
            return cmd;
        }
    }

}
