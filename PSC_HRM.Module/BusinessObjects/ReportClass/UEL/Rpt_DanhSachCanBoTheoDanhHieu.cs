using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Data.SqlClient;
using PSC_HRM.Module.KhenThuong;
using System.Text;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [ModelDefault("Caption", "Báo cáo danh sách cán bộ theo danh hiệu")]
    [Appearance("Rpt_DanhSachCanBoTheoDanhHieu.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Rpt_DanhSachCanBoTheoDanhHieu : StoreProcedureReport, IBoPhan
    {
        private int _NamNhanDanhHieu = DateTime.Today.Year;
        private DanhHieuKhenThuong _DanhHieu;
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;

        public Rpt_DanhSachCanBoTheoDanhHieu(Session session) : base(session) { }

        [ModelDefault("Caption", "Năm nhận danh hiệu")]
        [RuleRange("", DefaultContexts.Save, 1950, 2050, "Năm nhận danh hiệu phải từ 1950 đến 2050")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamNhanDanhHieu
        {
            get
            {
                return _NamNhanDanhHieu;
            }
            set
            {
                SetPropertyValue("NamNhanDanhHieu", ref _NamNhanDanhHieu, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieu
        {
            get
            {
                return _DanhHieu;
            }
            set
            {
                SetPropertyValue("DanhHieu", ref _DanhHieu, value);
            }
        }

        [ModelDefault("Caption", "Tất cả Đơn vị")]
        [ImmediatePostData()]
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TatCaBoPhan", ref _TatCaBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaBoPhan")]
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


        public override void FillDataSource()
        {
            List<string> lstBP;
            if (TatCaBoPhan)
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen();
            else
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_DanhSachNhanDanhHieuTheoNam", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Nam", NamNhanDanhHieu);
                da.SelectCommand.Parameters.AddWithValue("@DanhHieu", DanhHieu.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", sb.ToString());
                da.Fill(DataSource);
            }
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
    }

}
