using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [DefaultClassOptions]
    [NonPersistent()]
    //[VisibleInReports(true)]
    [ModelDefault("Caption", "Báo cáo danh sách tuyển dụng theo năm")]
    [Appearance("RptDanhSachTuyenDungNam.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class RptDanhSachTuyenDungNam : StoreProcedureReport, IBoPhan
    {
        public RptDanhSachTuyenDungNam(Session session) : base(session) { }
        private int _NamTuyenDung = DateTime.Today.Year;
        [ModelDefault("Caption", "Năm tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTuyenDung
        {
            get
            {
                return _NamTuyenDung;
            }
            set
            {
                SetPropertyValue("NamTuyenDung", ref _NamTuyenDung, value);
            }
        }

        private bool _TatCaBoPhan = true;
        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()] //Load dữ liệu ngay lập tức
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

        private DateTime _TinhDenNgay = DateTime.Today;
        [ModelDefault("Caption", "Tính đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TinhDenNgay
        {
            get
            {
                return _TinhDenNgay;
            }
            set
            {
                SetPropertyValue("TinhDenNgay", ref _TinhDenNgay, value);
            }
        }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_TuyenDung_DanhSachTuyenDungTheoNam");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Nam", NamTuyenDung);
            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);

            return cm;
        }
    }

}
