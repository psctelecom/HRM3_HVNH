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
    [DefaultProperty("TenChucDanhHoiDongKhenThuong")]
    [ModelDefault("Caption", "Chức danh hội đồng khen thưởng")]
    public class ChucDanhHoiDongKhenThuong : BaseObject
    {
        // Fields...
        private string _TenChucDanhHoiDongKhenThuong;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chức danh hội đồng khen thưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucDanhHoiDongKhenThuong
        {
            get
            {
                return _TenChucDanhHoiDongKhenThuong;
            }
            set
            {
                SetPropertyValue("TenChucDanhHoiDongKhenThuong", ref _TenChucDanhHoiDongKhenThuong, value);
            }
        }

        public ChucDanhHoiDongKhenThuong(Session session) : base(session) { }
    }

}
