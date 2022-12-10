using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DinhBien;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenCongViec")]
    [ModelDefault("Caption", "Công việc")]
    public class CongViec : BaseObject
    {
        private string _MaQuanLy;
        private string _TenCongViec;
        private LoaiNhanSu _LoaiNhanSu;
        private string _DieuKienDinhBien;
        
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

        [ModelDefault("Caption", "Tên công việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue()]
        public string TenCongViec
        {
            get
            {
                return _TenCongViec;
            }
            set
            {
                SetPropertyValue("TenCongViec", ref _TenCongViec, value);
            }
        }

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Điều kiện định biên")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ComboBoxEditor_DieuKienDinhBien")]
        [Size(-1)]
        public string DieuKienDinhBien
        {
            get
            {
                return _DieuKienDinhBien;
            }
            set
            {
                SetPropertyValue("DieuKienDinhBien", ref _DieuKienDinhBien, value);
            }
        }
          
        public CongViec(Session session) : base(session) { }
    }

}
