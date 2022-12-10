using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChucDanh")]
    [ModelDefault("Caption", "Chức danh hội đồng nâng lương")]
    public class ChucDanhHoiDongNangLuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChucDanh;
        
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

        [ModelDefault("Caption", "Tên chức danh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucDanh
        {
            get
            {
                return _TenChucDanh;
            }
            set
            {
                SetPropertyValue("TenChucDanh", ref _TenChucDanh, value);
            }
        }

        public ChucDanhHoiDongNangLuong(Session session) : base(session) { }
    }

}
