using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Report Danh sách nghỉ hưu theo năm")]
    [ImageName("BO_Report")]
    public class Report_DanhSachNghiHuuTheoNam : StoreProcedureReport
    {
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        public Report_DanhSachNghiHuuTheoNam(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            DataTable.Columns.Add("Ho", typeof(string));
            DataTable.Columns.Add("Ten", typeof(string));
            DataTable.Columns.Add("DonVi", typeof(string));
            DataTable.Columns.Add("SoQD", typeof(string));
            DataTable.Columns.Add("NgayKy", typeof(DateTime));
            DataTable.Columns.Add("NgayHieuLuc", typeof(DateTime));

            XPCollection<QuyetDinhThoiViec> tvList = new XPCollection<QuyetDinhThoiViec>(Session);
            tvList.Criteria = CriteriaOperator.Parse("LoaiThoiViec.TenLoaiQuyetDinh LIKE ?", "Nghỉ hưu");

            if (tvList != null && tvList.Count > 0)
            {
                foreach (QuyetDinhThoiViec item in tvList)
                {
                    if (item.NgayHieuLuc.Year == Nam)
                        DataTable.Rows.Add(item.ThongTinNhanVien.Ho, item.ThongTinNhanVien.Ten,
                            item.ThongTinNhanVien.BoPhan.TenBoPhan, item.SoQuyetDinh, item.NgayQuyetDinh, item.NgayHieuLuc);
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = DateTime.Today.Year;
        }
    }

}
