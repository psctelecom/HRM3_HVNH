using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNguonKinhPhi")]
    [ModelDefault("Caption", "Nguồn kinh phí")]
    public class NguonKinhPhi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNguonKinhPhi;

        public NguonKinhPhi(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên nguồn kinh phí")]
        [Size(8000)]
        public string TenNguonKinhPhi
        {
        	get
        	{
        		return _TenNguonKinhPhi;
        	}
        	set
        	{
        	  SetPropertyValue("TenNguonKinhPhi", ref _TenNguonKinhPhi, value);
        	}
        }
    }
}
