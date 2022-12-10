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
    [DefaultProperty("TenLoaiChamCongKhac")]
    [ModelDefault("Caption", "Chức danh")]
    [ImageName("BO_Position")]
    public class LoaiChamCongKhac : BaseObject
    {

        private string _MaQuanLy;
        private string _TenLoaiChamCongKhac;

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

        [ModelDefault("Caption", "Tên loại chấm công")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiChamCongKhac
        {
            get
            {
                return _TenLoaiChamCongKhac;
            }
            set
            {
                SetPropertyValue("TenLoaiChamCongKhac", ref _TenLoaiChamCongKhac, value);
            }
        }

        public LoaiChamCongKhac(Session session) : base(session) { }

    }

}
