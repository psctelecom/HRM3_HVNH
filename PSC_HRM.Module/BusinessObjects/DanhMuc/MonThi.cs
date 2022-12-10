using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenMonThi")]
    [ModelDefault("Caption", "Môn thi")]
    [RuleCombinationOfPropertiesIsUnique("MonThi.Unique", DefaultContexts.Save, "MaQuanLy;TenMonThi")]
    public class MonThi : BaseObject
    {
        // Fields...
        private int _DiemSan;
        private int _HeSo;
        private int _ThangDiem;
        private string _MaQuanLy;
        private string _TenMonThi;

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

        [ModelDefault("Caption", "Tên môn thi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenMonThi
        {
            get
            {
                return _TenMonThi;
            }
            set
            {
                SetPropertyValue("TenMonThi", ref _TenMonThi, value);
            }
        }

        [ModelDefault("Caption", "Thang điểm")]
        public int ThangDiem
        {
            get
            {
                return _ThangDiem;
            }
            set
            {
                SetPropertyValue("ThangDiem", ref _ThangDiem, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        public int HeSo
        {
            get
            {
                return _HeSo;
            }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [ModelDefault("Caption", "Điểm sàn")]
        public int DiemSan
        {
            get
            {
                return _DiemSan;
            }
            set
            {
                SetPropertyValue("DiemSan", ref _DiemSan, value);
            }
        }

        public MonThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThangDiem = 100;
            HeSo = 1;
        }
    }

}
