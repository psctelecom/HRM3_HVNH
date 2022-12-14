using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Thuong;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ được thưởng theo loại khen thưởng")]
    [ImageName("BO_Report")]
    public class Report_Thuong_DanhSachCanBoDuocThuongTheoLoaiKhenThuong : StoreProcedureReport
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

        public Report_Thuong_DanhSachCanBoDuocThuongTheoLoaiKhenThuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@KyTinhLuong", KyTinhLuong.Oid);
            param[1] = new SqlParameter("@LoaiKhenThuongPhucLoi", LoaiKhenThuongPhucLoi.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_Thuong_DanhSachCanBoDuocThuongTheoLoaiKhenThuong", 
                System.Data.CommandType.StoredProcedure, 
                param);
            return cmd;
        }
    }

}
