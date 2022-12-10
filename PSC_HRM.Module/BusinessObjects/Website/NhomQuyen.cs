using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;


namespace PSC_HRM.Module.Website
{
    [DefaultProperty("TenNhomQuyen")]
    [ModelDefault("Caption", "Nhóm quyền")]
    public class NhomQuyen : BaseObject
    {
        // Fields...
        private bool _XemBangLuong;
        private bool _XemQuaTrinh;
        private bool _XemHoSo;
        private string _TenNhomQuyen;

        [ModelDefault("Caption", "Tên nhóm quyền")]
        public string TenNhomQuyen
        {
            get
            {
                return _TenNhomQuyen;
            }
            set
            {
                SetPropertyValue("TenNhomQuyen", ref _TenNhomQuyen, value);
            }
        }


        public bool XemHoSo
        {
            get
            {
                return _XemHoSo;
            }
            set
            {
                SetPropertyValue("XemHoSo", ref _XemHoSo, value);
            }
        }


        public bool XemQuaTrinh
        {
            get
            {
                return _XemQuaTrinh;
            }
            set
            {
                SetPropertyValue("XemQuaTrinh", ref _XemQuaTrinh, value);
            }
        }


        public bool XemBangLuong
        {
            get
            {
                return _XemBangLuong;
            }
            set
            {
                SetPropertyValue("XemBangLuong", ref _XemBangLuong, value);
            }
        }



        public NhomQuyen(Session session) : base(session) { }
    }
}
