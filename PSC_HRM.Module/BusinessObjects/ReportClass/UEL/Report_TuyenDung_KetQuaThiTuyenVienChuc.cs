using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Kết quả thi tuyển viên chức")]
    public class Report_TuyenDung_KetQuaThiTuyenVienChuc : StoreProcedureReport
    {
        private QuyetDinhCongNhanKetQuaThiTuyenDung _QuyetDinh;

        [ImmediatePostData]
        [ModelDefault("Caption", "QĐ công nhận kết quả tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuyetDinhCongNhanKetQuaThiTuyenDung QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        public Report_TuyenDung_KetQuaThiTuyenVienChuc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TuyenDung_KetQuaThiTuyenVienChuc",
                System.Data.CommandType.StoredProcedure, new SqlParameter("@QuanLyTuyenDung", QuyetDinh.QuanLyTuyenDung.Oid));

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }
    }

}
