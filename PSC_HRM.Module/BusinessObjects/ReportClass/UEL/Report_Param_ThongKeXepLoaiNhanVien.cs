using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public class Report_Param_ThongKeXepLoaiNhanVien : ReportParametersObjectBase, PSC_HRM.Module.Security.IBoPhan
    {
        private BoPhan boPhan;
        private ThongTinNhanVien nhanVien;
        private DateTime tuThang;
        private DateTime denThang;

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return boPhan; }
            set
            {
                SetPropertyValue<BoPhan>("BoPhan", ref boPhan, value);
            }
        }

        [ModelDefault("Caption", "Nhân Viên")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn nhân viên")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        public ThongTinNhanVien NhanVien
        {
            get { return nhanVien; }
            set
            {
                SetPropertyValue<ThongTinNhanVien>("NhanVien", ref nhanVien, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn tháng bắt đầu")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuThang
        {
            get { return tuThang; }
            set
            {
                SetPropertyValue("TuThang", ref tuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn tháng kết thúc")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenThang
        {
            get { return denThang; }
            set
            {
                SetPropertyValue("DenThang", ref denThang, value);
            }
        }

        public Report_Param_ThongKeXepLoaiNhanVien(Session session) : base(session) { }
        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("NhanVien=? AND KyTinhLuong.TuNgay Between(?,?)", NhanVien, TuThang, DenThang);
        }
        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
