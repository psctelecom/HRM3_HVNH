using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.TuyenDung;
using System.Data;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Quyết định công nhận kết quả thi tuyển dụng")]
    public class Report_TuyenDung_QuyetDinhCongNhanKetQuaThiTuyenDung : StoreProcedureReport
    {
        // Fields...
        private QuyetDinhCongNhanKetQuaThiTuyenDung _QuyetDinhCongNhanKetQuaThiTuyenDung;

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhCongNhanKetQuaThiTuyenDung QuyetDinhCongNhanKetQuaThiTuyenDung
        {
            get
            {
                return _QuyetDinhCongNhanKetQuaThiTuyenDung;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanKetQuaThiTuyenDung", ref _QuyetDinhCongNhanKetQuaThiTuyenDung, value);
            }
        }

        public Report_TuyenDung_QuyetDinhCongNhanKetQuaThiTuyenDung(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_TuyenDung_QuyetDinhCongNhanKetQuaThiTuyenDung", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuyetDinhCongNhanKetQuaThiTuyenDung", QuyetDinhCongNhanKetQuaThiTuyenDung.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
