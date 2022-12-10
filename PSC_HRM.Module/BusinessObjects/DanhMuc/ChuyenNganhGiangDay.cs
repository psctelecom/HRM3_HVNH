using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DinhBien;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNganhGD")]
    [ModelDefault("Caption", "Chuyên ngành giảng dạy")]
    public class ChuyenNganhGiangDay : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNganhGD;
        private KhoiNganh _KhoiNganh;
        //private LoaiNhanSu _LoaiNhanSu;
        //private string _DieuKienDinhBien;

        [ModelDefault("Caption", "Khối ngành")]
        public KhoiNganh KhoiNganh
        {
            get
            {
                return _KhoiNganh;
            }
            set
            {
                SetPropertyValue("KhoiNganh", ref _KhoiNganh, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chuyên ngành giảng dạy")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue()]
        public string TenNganhGD
        {
            get
            {
                return _TenNganhGD;
            }
            set
            {
                SetPropertyValue("TenNganhGD", ref _TenNganhGD, value);
            }
        }

        //[ModelDefault("Caption", "Loại nhân sự")]
        //public LoaiNhanSu LoaiNhanSu
        //{
        //    get
        //    {
        //        return _LoaiNhanSu;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
        //    }
        //}

        //[ModelDefault("Caption", "Điều kiện định biên")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ComboBoxEditor_DieuKienDinhBien")]
        //[Size(-1)]
        //public string DieuKienDinhBien
        //{
        //    get
        //    {
        //        return _DieuKienDinhBien;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DieuKienDinhBien", ref _DieuKienDinhBien, value);
        //    }
        //}
          
        public ChuyenNganhGiangDay(Session session) : base(session) { }
    }

}
