using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp các khoản thu nhập")]
    public class Report_Luong_BangTongHopCacKhoanThuNhap : StoreProcedureReport
    {
        // Fields...
        private BangTongHopLuongEnum _PhanLoai = BangTongHopLuongEnum.Huong100;
        private ChungTu.ChungTu _ChungTu;

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public BangTongHopLuongEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        public Report_Luong_BangTongHopCacKhoanThuNhap(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChungTu", ChungTu.Oid);
            param[1] = new SqlParameter("@PhanLoai", (int)PhanLoai);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_Luong_BangTongHopCacKhoanThuNhap", 
                System.Data.CommandType.StoredProcedure, param);
            return cmd;
        }
    }

}
