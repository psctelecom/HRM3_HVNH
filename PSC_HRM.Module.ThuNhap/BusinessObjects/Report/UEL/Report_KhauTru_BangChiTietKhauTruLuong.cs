using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.ChungTu;
using PSC_HRM.Module.ThuNhap.KhauTru;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng chi tiết khấu trừ lương")]
    public class Report_KhauTru_BangChiTietKhauTruLuong : StoreProcedureReport
    {
        private LoaiKhauTruLuong _LoaiKhauTruLuong;
        private KyTinhLuong _KyTinhLuong;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
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

        [ModelDefault("Caption", "Loại khấu trừ lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiKhauTruLuong LoaiKhauTruLuong
        {
            get
            {
                return _LoaiKhauTruLuong;
            }
            set
            {
                SetPropertyValue("LoaiKhauTruLuong", ref _LoaiKhauTruLuong, value);
            }
        }

        public Report_KhauTru_BangChiTietKhauTruLuong(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);
            parameter[1] = new SqlParameter("@LoaiKhauTru", LoaiKhauTruLuong.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_KhauTru_BangChiTietKhauTruLuong", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
