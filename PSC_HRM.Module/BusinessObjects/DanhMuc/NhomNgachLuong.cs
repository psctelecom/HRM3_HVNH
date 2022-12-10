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
    [DefaultProperty("TenNhomNgachLuong")]
    [ModelDefault("Caption", "Nhóm ngạch lương")]
    public class NhomNgachLuong : BaseObject
    {
        private string _SoThuTu;
        private string _BangLuong;
        private string _MaQuanLy;
        private string _TenNhomNgachLuong;

        [ModelDefault("Caption", "Số thứ tự")]
        public string SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [ModelDefault("Caption", "Bảng lương")]
        public string BangLuong
        {
            get
            {
                return _BangLuong;
            }
            set
            {
                SetPropertyValue("BangLuong", ref _BangLuong, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên nhóm ngạch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomNgachLuong
        {
            get
            {
                return _TenNhomNgachLuong;
            }
            set
            {
                SetPropertyValue("TenNhomNgachLuong", ref _TenNhomNgachLuong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ngạch lương")]
        [Association("NhomNgachLuong-ListNgachLuong")]
        public XPCollection<NgachLuong> ListNgachLuong
        {
            get
            {
                return GetCollection<NgachLuong>("ListNgachLuong");
            }
        }

        public NhomNgachLuong(Session session) : base(session) { }
    }

}
