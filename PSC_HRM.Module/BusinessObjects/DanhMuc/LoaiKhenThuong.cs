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
    [DefaultProperty("TenLoaiKhenThuong")]
    [ModelDefault("Caption", "Loại khen thưởng")]
    [RuleCombinationOfPropertiesIsUnique("LoaiKhenThuong.Identifier", DefaultContexts.Save, "MaQuanLy;TenLoaiKhenThuong", "Loại khen thưởng đã tồn tại trong hệ thống.")]
    public class LoaiKhenThuong : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiKhenThuong;

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

        [ModelDefault("Caption", "Tên loại khen thưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiKhenThuong
        {
            get
            {
                return _TenLoaiKhenThuong;
            }
            set
            {
                SetPropertyValue("TenLoaiKhenThuong", ref _TenLoaiKhenThuong, value);
            }
        }
        
        public LoaiKhenThuong(Session session) : base(session) { }
    }

}
