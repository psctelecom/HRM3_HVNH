using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenBacLuong")]
    [ModelDefault("Caption", "Bậc lương")]
    [RuleCombinationOfPropertiesIsUnique("BacLuong.Unique", DefaultContexts.Save, "NgachLuong;MaQuanLy;TenBacLuong;HeSoLuong")]
    [Appearance("ShowP1P3", TargetItems = "P1MucLuongDongBHXH;P3ThuNhapTangThem", Visibility = ViewItemVisibility.Show, Criteria = "MaTruong = 'VLU'")]
    public class BacLuong : BaseObject
    {
        private NgachLuong _NgachLuong;
        private string _MaQuanLy;
        private string _TenBacLuong;
        private decimal _HeSoLuong;
        private bool _BacLuongCu;
        //VLU
        private decimal _P1MucLuongDongBHXH;
        private decimal _P3ThuNhapTangThem;

        //[ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("NgachLuong-ListBacLuong")]
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        //[ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        //[ModelDefault("Caption", "Tên bậc lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên bậc lương")]
        public string TenBacLuong
        {
            get
            {
                return _TenBacLuong;
            }
            set
            {
                SetPropertyValue("TenBacLuong", ref _TenBacLuong, value);
            }
        }
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Mức lương đóng BHXH (P1)")]
        public decimal P1MucLuongDongBHXH
        {
            get
            {
                return _P1MucLuongDongBHXH;
            }
            set
            {
                SetPropertyValue("P1MucLuongDongBHXH", ref _P1MucLuongDongBHXH, value);
            }
        }
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Thu nhập tăng thêm (P3)")]
        public decimal P3ThuNhapTangThem
        {
            get
            {
                return _P3ThuNhapTangThem;
            }
            set
            {
                SetPropertyValue("P3ThuNhapTangThem", ref _P3ThuNhapTangThem, value);
            }
        }

        //[ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hệ số lương")]  
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        //[ModelDefault("Caption", "Bậc lương cũ")]
        [ModelDefault("Caption", "Bậc lương cũ")]
        public bool BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
            }
        }

        public BacLuong(Session session) : base(session) { }
    }

}
