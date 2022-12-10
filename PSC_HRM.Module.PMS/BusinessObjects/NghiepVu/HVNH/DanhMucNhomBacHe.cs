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
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Nhóm bậc hệ")]
    public class DanhMucNhomBacHe : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhom;

        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên nhóm")]
        public string TenNhom
        {
            get { return _TenNhom; }
            set
            {
                SetPropertyValue("TenNhom", ref _TenNhom, value);
            }
        }

        [Aggregated]
        [Association("DanhMucNhomBacHe-ListChiTietNhomBac_HeDaoTao")]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietNhomBac_HeDaoTao> ListChiTietNhomBac_HeDaoTao
        {
            get
            {
                return GetCollection<ChiTietNhomBac_HeDaoTao>("ListChiTietNhomBac_HeDaoTao");
            }
        }
        public DanhMucNhomBacHe(Session session) : base(session) { }

    }
}
