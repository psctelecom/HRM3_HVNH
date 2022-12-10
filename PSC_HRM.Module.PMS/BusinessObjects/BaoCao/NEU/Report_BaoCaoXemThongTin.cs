using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng xem thông tin")]
    public class Report_BaoCaoXemThongTin : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;


        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        public Report_BaoCaoXemThongTin(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_XemThongTin", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ThongTinTruong", ThongTinTruong == null ? Guid.Empty : ThongTinTruong.Oid);
                da.Fill(DataSource);
            }
        }
    }
}
