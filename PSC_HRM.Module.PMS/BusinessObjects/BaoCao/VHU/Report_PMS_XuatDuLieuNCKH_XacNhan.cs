using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Xuất dữ liệu NCKH đã xác nhận")]
    public class Report_PMS_XuatDuLieuNCKH_XacNhan : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private string _BoPhan;

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [Size(-1)]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        public Report_PMS_XuatDuLieuNCKH_XacNhan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            BoPhan = HamDungChung.GetPhanQuyenBoPhan();
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_NghienCuuKhoaHoc_LayDuLieuTheoPhanQuyen_Report", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan);
                da.Fill(DataSource);
            }
        }
        
    }
}
