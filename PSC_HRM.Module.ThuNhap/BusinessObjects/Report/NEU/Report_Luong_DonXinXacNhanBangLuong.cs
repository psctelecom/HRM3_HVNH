using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Đơn xin xác nhận bảng lương")]
    public class Report_Luong_DonXinXacNhanBangLuong : StoreProcedureReport
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _TuThang;
        private DateTime _DenThang;
        private string _LyDo;
        private bool _TiengAnh;

        [ModelDefault("Caption", "Thông tin nhân viên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]  
        //[Size(250)] 
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [ModelDefault("Caption", "Tiếng Anh")]
        public bool TiengAnh
        {
            get
            {
                return _TiengAnh;
            }
            set
            {
                SetPropertyValue("TiengAnh", ref _TiengAnh, value);
            }
        }

        public Report_Luong_DonXinXacNhanBangLuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_Luong_DonXinXacNhanBangLuong", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@TuThang", TuThang);
                da.SelectCommand.Parameters.AddWithValue("@DenThang", DenThang);
                da.SelectCommand.Parameters.AddWithValue("@LyDo", LyDo);
                da.SelectCommand.Parameters.AddWithValue("@TiengAnh", TiengAnh);
                da.Fill(DataSource);
            }
        }
    }

}
