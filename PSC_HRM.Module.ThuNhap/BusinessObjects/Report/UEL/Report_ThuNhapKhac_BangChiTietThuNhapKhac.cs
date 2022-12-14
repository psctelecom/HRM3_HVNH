using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap.Thuong;
using PSC_HRM.Module.ThuNhap.KhauTru;
using PSC_HRM.Module.ThuNhap.ThuNhapKhac;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng chi tiết thu nhập khác")]
    public class Report_ThuNhapKhac_BangChiTietThuNhapKhac : StoreProcedureReport
    {
        private LoaiThuNhapKhac _LoaiThuNhapKhac;
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

        [ModelDefault("Caption", "Loại thu nhập khác")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiThuNhapKhac LoaiThuNhapKhac
        {
            get
            {
                return _LoaiThuNhapKhac;
            }
            set
            {
                SetPropertyValue("LoaiThuNhapKhac", ref _LoaiThuNhapKhac,value);
            }
        }

        public Report_ThuNhapKhac_BangChiTietThuNhapKhac(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);
            param[1] = new SqlParameter("@LoaiThuNhapKhac", LoaiThuNhapKhac.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThuNhapKhac_BangChiTietThuNhapKhac", 
                System.Data.CommandType.StoredProcedure, 
                param);
            return cmd;
        }
    }

}
