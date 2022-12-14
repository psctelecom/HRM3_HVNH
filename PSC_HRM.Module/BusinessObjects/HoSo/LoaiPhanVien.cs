using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.HoSo
{
    [ModelDefault("Caption", "Loại phân viện")]
    [DefaultProperty("TenLoaiPhanVien")]
    public class LoaiPhanVien : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiPhanVien;



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

        [ModelDefault("Caption", "Loại phân viện")]
        public string TenLoaiPhanVien
        {
            get
            {
                return _TenLoaiPhanVien;
            }
            set
            {
                SetPropertyValue("TenLoaiPhanVien", ref _TenLoaiPhanVien, value);
            }
        }
        
        public LoaiPhanVien(Session session) : base(session) { }
    }

}
