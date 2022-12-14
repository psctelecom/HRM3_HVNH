using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Tuổi nghỉ hưu")]
    [RuleCombinationOfPropertiesIsUnique("TuoiNghiHuu.Identifier", DefaultContexts.Save, "MaQuanLy;GioiTinh;Tuoi", "Tuổi nghỉ hưu đã tồn tại trong hệ thống.")]
    public class TuoiNghiHuu : BaseObject
    {
        private string _MaQuanLy;
        private GioiTinhEnum _GioiTinh;
        private int _Tuoi;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Giới tính")]
        [RuleRequiredField(DefaultContexts.Save)]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Tuổi")]
        [RuleRange("", DefaultContexts.Save, 20, 120)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Tuoi
        {
            get
            {
                return _Tuoi;
            }
            set
            {
                SetPropertyValue("Tuoi", ref _Tuoi, value);
            }
        }

        public TuoiNghiHuu(Session session) : base(session) { }
    }

}
