using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenLoaiNhanSu")]
    [ModelDefault("Caption", "Loại nhân sự")]
    [RuleCombinationOfPropertiesIsUnique("LoaiNhanSu.Identifier", DefaultContexts.Save, "MaQuanLy;TenLoaiNhanSu", "Loại nhân sự đã tồn tại trong hệ thống.")]
    public class LoaiNhanSu : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiNhanSu;

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

        [ModelDefault("Caption", "Tên loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiNhanSu
        {
            get
            {
                return _TenLoaiNhanSu;
            }
            set
            {
                SetPropertyValue("TenLoaiNhanSu", ref _TenLoaiNhanSu, value);
            }
        }

        private decimal _CapDo;
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }
        public LoaiNhanSu(Session session) : base(session) { }
    }

}
