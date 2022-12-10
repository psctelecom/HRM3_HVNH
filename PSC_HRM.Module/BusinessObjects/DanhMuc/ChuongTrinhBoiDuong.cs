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
    [DefaultProperty("TenChuongTrinhBoiDuong")]
    [ModelDefault("Caption", "Chương trình bồi dưỡng")]
    public class ChuongTrinhBoiDuong : BaseObject
    {
        // Fields...
        private string _DiaDiem;
        private string _DonViToChuc;
        private string _TenChuongTrinhBoiDuong;

        [Size(500)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên chương trình bồi dưỡng")]
        public string TenChuongTrinhBoiDuong
        {
            get
            {
                return _TenChuongTrinhBoiDuong;
            }
            set
            {
                SetPropertyValue("TenChuongTrinhBoiDuong", ref _TenChuongTrinhBoiDuong, value);
            }
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đơn vị tổ chức")]
        public string DonViToChuc
        {
            get
            {
                return _DonViToChuc;
            }
            set
            {
                SetPropertyValue("DonViToChuc", ref _DonViToChuc, value);
            }
        }

        [Size(500)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Địa điểm")]
        public string DiaDiem
        {
            get
            {
                return _DiaDiem;
            }
            set
            {
                SetPropertyValue("DiaDiem", ref _DiaDiem, value);
            }
        }

        public ChuongTrinhBoiDuong(Session session) : base(session) { }
    }

}
