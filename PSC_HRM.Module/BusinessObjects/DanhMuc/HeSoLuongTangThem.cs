using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Hệ số lương tăng thêm")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "NgachLuong,TuHeSo,DenHeSo,PhanTramVuotKhung,HocHam")]
    public class HeSoLuongTangThem : BaseObject
    {
        private NgachLuong _NgachLuong;
        private decimal _TuHeSo;
        private decimal _DenHeSo;
        private bool _VuotKhung;
        private decimal _PhanTramVuotKhung;
        private NhomNgachLuong _NhomNgachLuong;
        private decimal _HSLTangThem;
        private HocHam _HocHam;
        private decimal _HSTangThemTienSi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Nhóm ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhomNgachLuong NhomNgachLuong
        {
            get
            {
                return _NhomNgachLuong;
            }
            set
            {
                SetPropertyValue("NhomNgachLuong", ref _NhomNgachLuong, value);
                if (!IsLoading && value != null)
                {
                    UpdateNgachLuong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [DataSourceProperty("NgachLuongList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading && value != null)
                {
                    if (NhomNgachLuong == null || value.NhomNgachLuong.Oid != NhomNgachLuong.Oid)
                        NhomNgachLuong = value.NhomNgachLuong;
                }
            }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ModelDefault("Caption", "Từ hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Appearance("TuHeSo", TargetItems = "TuHeSo", Enabled = false, Criteria = "VuotKhung")]
        public decimal TuHeSo
        {
            get
            {
                return _TuHeSo;
            }
            set
            {
                SetPropertyValue("TuHeSo", ref _TuHeSo, value);
            }
        }

        [ModelDefault("Caption", "Đến hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Appearance("DenHeSo", TargetItems = "DenHeSo", Enabled = false, Criteria = "VuotKhung")]
        public decimal DenHeSo
        {
            get
            {
                return _DenHeSo;
            }
            set
            {
                SetPropertyValue("DenHeSo", ref _DenHeSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vượt khung")]
        public bool VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);
            }
        }

        [ModelDefault("Caption", "% vượt khung")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [Appearance("PhanTramVuotKhung", TargetItems = "PhanTramVuotKhung", Enabled = false, Criteria = "!VuotKhung")]
        public decimal PhanTramVuotKhung
        {
            get
            {
                return _PhanTramVuotKhung;
            }
            set
            {
                SetPropertyValue("PhanTramVuotKhung", ref _PhanTramVuotKhung, value);
            }
        }

        [ModelDefault("Caption", "HSL tăng thêm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSLTangThem
        {
            get
            {
                return _HSLTangThem;
            }
            set
            {
                SetPropertyValue("HSLTangThem", ref _HSLTangThem, value);
            }
        }

        [ModelDefault("Caption", "HS tăng thêm Tiến sĩ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSTangThemTienSi
        {
            get
            {
                return _HSTangThemTienSi;
            }
            set
            {
                SetPropertyValue("HSTangThemTienSi", ref _HSTangThemTienSi, value);
            }
        }

        [Browsable(false)]
        public XPCollection<NgachLuong> NgachLuongList { get; set; }

        public HeSoLuongTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            VuotKhung = false;
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNgachLuong();
        }

        public void UpdateNgachLuong()
        {
            if (NgachLuongList == null)
                NgachLuongList = new XPCollection<NgachLuong>(Session);
            NgachLuongList.Criteria = CriteriaOperator.Parse("NhomNgachLuong = ?", NhomNgachLuong.Oid);
        }
    }
}
