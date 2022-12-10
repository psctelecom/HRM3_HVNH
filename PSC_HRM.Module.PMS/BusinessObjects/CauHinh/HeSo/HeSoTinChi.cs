using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [Appearance("Hide_HUFLIT", TargetItems = "LoaiMonHoc;BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat != 'HUFLIT'")]
    [ModelDefault("Caption", "Hệ số tín chỉ")]
    [DefaultProperty("Caption")]
    public class HeSoTinChi : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoTinChi")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        //private NamHoc _NamHoc;
        private decimal _SoTinChi;
        private decimal _HeSo_TinChi;
        private LoaiMonHoc _LoaiMonHoc;
        private BoPhan _BoPhan;

        //[ModelDefault("Caption", "Năm học")]
        //[DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        //public NamHoc NamHoc
        //{
        //    get { return _NamHoc; }
        //    set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        //}
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [RuleRange("HSTC_SoTinChi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_TinChi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_TinChi
        {
            get { return _HeSo_TinChi; }
            set { SetPropertyValue("HeSo_TinChi", ref _HeSo_TinChi, value); }
        }

        [ModelDefault("Caption", "Loại môn học")]
        public LoaiMonHoc LoaiMonHoc
        {
            get { return _LoaiMonHoc; }
            set { SetPropertyValue("LoaiMonHoc", ref _LoaiMonHoc, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        public HeSoTinChi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            HeSo_TinChi = 1;
        }
    }
}