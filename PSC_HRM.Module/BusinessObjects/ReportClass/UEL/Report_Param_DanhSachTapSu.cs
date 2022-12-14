using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.ConditionalEditorState;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [Appearance("Report_Param_DanhSachTapSu.DenNam", "DenNam", Enabled = false, "Loai=0")]
    public class Report_Param_DanhSachTapSu : ReportParametersObjectBase
    {
        public enum LoaiEnum
        { 
            [DevExpress.Xpo.DisplayName("Năm")]
            Nam = 0,
            [DevExpress.Xpo.DisplayName("Khoảng thời gian")]
            KhoangThoiGian = 1
        }

        private LoaiEnum _Loai;
        private int _TuNam;
        private int _DenNam;


        [ModelDefault("Caption", "Hình thức")]
        public LoaiEnum Loai
        {
            get { return _Loai; }
            set { SetPropertyValue("Loai", ref _Loai, value); }
        }

        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập Từ năm")]
        public int TuNam
        {
            get { return _TuNam; }
            set { SetPropertyValue("TuNam", ref _TuNam, value); }
        }

        [ModelDefault("Caption", "Đến năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập Đến năm", TargetCriteria="Loai=2")]
        public int DenNam
        {
            get { return _DenNam; }
            set { SetPropertyValue("DenNam", ref _DenNam, value); }
        }

        public Report_Param_DanhSachTapSu(Session session) : 
            base(session) 
        { }

        public override CriteriaOperator GetCriteria()
        {
            if (Loai == LoaiEnum.Nam)
                return (new OperandProperty("NgayKy") >= new DateTime(TuNam, 1, 1) &
                    new OperandProperty("NgayKy") < new DateTime(TuNam + 1, 1, 1) & 
                    CriteriaOperator.Parse("HinhThucHopDong.TenHinhThucHopDong LIKE ?", "hợp đồng lần đầu"));
            else
                return (new OperandProperty("NgayKy") >= new DateTime(TuNam, 1, 1) &
                    new OperandProperty("NgayKy") < new DateTime(DenNam + 1, 1, 1) &
                    CriteriaOperator.Parse("HinhThucHopDong.TenHinhThucHopDong LIKE ?", "hợp đồng lần đầu"));
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNam = DateTime.Today.Year;
            DenNam = TuNam;
        }
    }
}
