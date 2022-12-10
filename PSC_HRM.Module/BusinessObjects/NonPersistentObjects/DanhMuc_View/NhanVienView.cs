using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects.DanhMuc_View
{
    [NonPersistent]
    [DefaultProperty("HoTen")]
    public class NhanVienView : BaseObject
    {
        private Guid _OidNhanVien;
        private string _TenBoPhan;
        private string _HoTen;
        private string _TinhTrang;
        private string _CMND;
        [Browsable(false)]
        public Guid OidNhanVien
        {
            get
            {
                return _OidNhanVien;
            }
            set
            {
                SetPropertyValue("OidNhanVien", ref _OidNhanVien, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        public string TenBoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("TenBoPhan", ref _TenBoPhan, value);
            }
        }
        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }
        [ModelDefault("Caption", "Tình trạng làm việc")]
        public string TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }
        
        
        [ModelDefault("Caption", "CMND")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }
        public NhanVienView(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}