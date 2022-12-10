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
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.DanhMuc
{

    [Appearance("Khoa_UFM", TargetItems = "*", Enabled = false, Criteria = "MaTruong = 'UFM'")]
    [Appearance("Hide_UFM", TargetItems = "KhongTinhTien;PhiDiChuyen"
                                         , Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UFM'")]

    [ModelDefault("Caption", "Cơ sở giảng dạy")]
    [DefaultProperty("TenCoSo")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "MaQuanLy", "Mã cơ sở đã tồn tại.")]
    public class CoSoGiangDay : BaseObject
    {
        private string _MaTruong;
        private string _MaQuanLy;
        private string _TenCoSo;
        private string _DiaChi;
        private bool _KhongTinhTien;
        private decimal _PhiDiChuyen;
        [ModelDefault("Caption", "MaTruong")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public string MaTruong
        {
            get { return _MaTruong; }
            set { SetPropertyValue("MaTruong", ref _MaTruong, value); }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Cơ sở")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
        public string TenCoSo
        {
            get { return _TenCoSo; }
            set { SetPropertyValue("TenCoSo", ref _TenCoSo, value); }
        }
        [ModelDefault("Caption", "Địa chỉ")]
        public string DiaChi
        {
            get { return _DiaChi; }
            set { SetPropertyValue("DiaChi", ref _DiaChi, value); }
        }
        [ModelDefault("Caption", "Không tính tiền")]
        [VisibleInListView(false)]
        public bool KhongTinhTien
        {
            get { return _KhongTinhTien; }
            set { SetPropertyValue("KhongTinTien", ref _KhongTinhTien, value); }
        }

        [ModelDefault("Caption", "Phí di chuyển")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [VisibleInListView(false)]
        public decimal PhiDiChuyen
        {
            get { return _PhiDiChuyen; }
            set { SetPropertyValue("PhiDiChuyen", ref _PhiDiChuyen, value); }
        }
        public CoSoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
        }
    }
}
