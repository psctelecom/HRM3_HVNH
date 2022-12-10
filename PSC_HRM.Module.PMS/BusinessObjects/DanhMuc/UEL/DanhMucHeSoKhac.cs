using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [DefaultProperty("TenDanhMuc")]
    [ModelDefault("Caption", "Danh mục hệ số khác")]
    public class DanhMucHeSoKhac : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDanhMuc;
        [ModelDefault("Caption", "Mã quản lý")]

        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        [ModelDefault("Caption", "Tên danh mục")]
        public string TenDanhMuc
        {
            get { return _TenDanhMuc; }
            set
            {
                SetPropertyValue("TenDanhMuc", ref _TenDanhMuc, value);
            }
        }
        public DanhMucHeSoKhac(Session session) : base(session) { }

    }
}
