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
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Ngạch lương")]
    public class NgachLuong : BaseObject
    {
        private BacLuong _TotKhung;
        private NhomNgachLuong _NhomNgachLuong;
        private string _MaQuanLy;
        private string _TenNgachLuong;
        private int _ThoiGianNangBac;

        [ModelDefault("Caption", "Nhóm ngạch lương")]
        [Association("NhomNgachLuong-ListNgachLuong")]
        public NhomNgachLuong NhomNgachLuong
        {
            get
            {
                return _NhomNgachLuong;
            }
            set
            {
                SetPropertyValue("NhomNgachLuong", ref _NhomNgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("",DefaultContexts.Save)]
        [RuleUniqueValue("",DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgachLuong
        {
            get
            {
                return _TenNgachLuong;
            }
            set
            {
                SetPropertyValue("TenNgachLuong", ref _TenNgachLuong, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Tên ngạch lương")]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("{MaQuanLy} - {TenNgachLuong}", this);
            }
        }

        [ModelDefault("Caption", "Thời gian nâng bậc (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianNangBac
        {
            get
            {
                return _ThoiGianNangBac;
            }
            set
            {
                SetPropertyValue("ThoiGianNangBac", ref _ThoiGianNangBac, value);
            }
        }

        [ModelDefault("Caption", "Tột khung")]
        [DataSourceProperty("ListBacLuong")]
        public BacLuong TotKhung
        {
            get
            {
                return _TotKhung;
            }
            set
            {
                SetPropertyValue("TotKhung", ref _TotKhung, value);
            }
        }

        [Aggregated]
        [Association("NgachLuong-ListBacLuong")]
        [ModelDefault("Caption", "Danh sách bậc lương")]
        public XPCollection<BacLuong> ListBacLuong
        {
            get
            {
                return GetCollection<BacLuong>("ListBacLuong");
            }
        }

        public NgachLuong(Session session) : base(session) { }
    }
}
