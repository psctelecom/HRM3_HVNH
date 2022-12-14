using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;

using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.ThuNhap.Thuong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng chi tiết khen thưởng phúc lợi")]
    public class Report_Thuong_BangChiTietKhenThuong : StoreProcedureReport
    {
        private LoaiKhenThuongPhucLoi _LoaiKhenThuongPhucLoi;
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

        [ModelDefault("Caption", "Loại Khen thưởng - Phúc lợi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiKhenThuongPhucLoi LoaiKhenThuongPhucLoi
        {
            get
            {
                return _LoaiKhenThuongPhucLoi;
            }
            set
            {
                SetPropertyValue("LoaiKhenThuongPhucLoi", ref _LoaiKhenThuongPhucLoi, value);
            }
        }

        public Report_Thuong_BangChiTietKhenThuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);
            param[1] = new SqlParameter("@LoaiThuong", LoaiKhenThuongPhucLoi.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_Thuong_BangChiTietKhenThuong", 
                System.Data.CommandType.StoredProcedure, 
                param);
            return cmd;
        }
    }

}
