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
    [DefaultProperty("TenChucDanhHoiDongKyLuat")]
    [ModelDefault("Caption", "Chức danh hội đồng kỷ luật")]
    public class ChucDanhHoiDongKyLuat : BaseObject
    {
        // Fields...
        private string _TenChucDanhHoiDongKyLuat;
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

        [ModelDefault("Caption", "Tên chức danh hội đồng kỷ luật")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucDanhHoiDongKyLuat
        {
            get
            {
                return _TenChucDanhHoiDongKyLuat;
            }
            set
            {
                SetPropertyValue("TenChucDanhHoiDongKyLuat", ref _TenChucDanhHoiDongKyLuat, value);
            }
        }

        public ChucDanhHoiDongKyLuat(Session session) : base(session) { }
    }

}
