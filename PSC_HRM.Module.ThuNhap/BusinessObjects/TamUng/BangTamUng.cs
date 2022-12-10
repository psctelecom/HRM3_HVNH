using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.TamUng
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Bảng tạm ứng")]
    [DefaultProperty("NamString")]
    [RuleCombinationOfPropertiesIsUnique("BangTamUng.Unique", DefaultContexts.Save, "Nam;ThongTinTruong")]
    public class BangTamUng : BaseObject, IThongTinTruong
    {
        private bool _KhoaSo;
        private int _Nam;
        private ThongTinTruong _ThongTinTruong;
        private String _NamString;


        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Năm String")]
        public String NamString
        {
            get
            {
                return _NamString;
            }
            set
            {
                SetPropertyValue("NamString", ref _NamString, value);
                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                NamString = value.ToString();
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangTamUng-ListTamUng")]
        public XPCollection<TamUng> ListTamUng
        {
            get
            {
                return GetCollection<TamUng>("ListTamUng");
            }
        }

        public BangTamUng(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            Nam = HamDungChung.GetServerTime().Year; 
        }
    }

}
