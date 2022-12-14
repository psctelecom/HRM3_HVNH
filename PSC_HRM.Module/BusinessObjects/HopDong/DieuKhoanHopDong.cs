using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_HopDong")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Điều khoản hợp đồng")]
    public class DieuKhoanHopDong : BaseObject
    {
        public event EventHandler OnDieuKhoanChanged;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private int _VuotKhung;
        private bool _Huong85PhanTramMucLuong;
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                {
                    BacLuong = null;
                    HeSoLuong = 0;

                    if (OnDieuKhoanChanged != null)
                        OnDieuKhoanChanged(this, null);
                }

            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuong = value.HeSoLuong;

                    if (OnDieuKhoanChanged != null)
                        OnDieuKhoanChanged(this, null);
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);

                if (OnDieuKhoanChanged != null)
                    OnDieuKhoanChanged(this, null);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Vượt khung")]
        public int VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);
            }
        }

        [ModelDefault("Caption", "Hưởng 85% mức lương")]
        public bool Huong85PhanTramMucLuong
        {
            get
            {
                return _Huong85PhanTramMucLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramMucLuong", ref _Huong85PhanTramMucLuong, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Ngạch {NgachLuong} bậc {BacLuong} hệ số {HeSoLuong:N2}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
            }
        }

        public DieuKhoanHopDong(Session session) : base(session) { }
    }

}
