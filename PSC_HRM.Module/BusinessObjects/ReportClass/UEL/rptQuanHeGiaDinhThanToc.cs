using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo Quan hệ gia đình, thân tộc")]
    public class rptQuanHeGiaDinhThanToc : StoreProcedureReport, IBoPhan
    {
        public rptQuanHeGiaDinhThanToc(Session session) : base(session) { }

        private BoPhan _BoPhan;
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        private ThongTinNhanVien _ThongTinNhanVien;
        [ModelDefault("Caption", "Thông tin cán bộ")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
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
        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_QuanHeGiaDinhThanToc");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            if (ThongTinNhanVien != null)
                cm.Parameters.AddWithValue("@Oid_NhanVien", ThongTinNhanVien.Oid);
            else
                cm.Parameters.AddWithValue("@Oid_NhanVien", DBNull.Value);

            if (BoPhan != null)
                cm.Parameters.AddWithValue("@Oid_BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@Oid_BoPhan", DBNull.Value);

            return cm;
        }
    }

}
