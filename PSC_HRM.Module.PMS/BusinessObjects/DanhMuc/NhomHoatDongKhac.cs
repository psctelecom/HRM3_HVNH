using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Nhóm hoạt động khác")]
    [DefaultProperty("TenLoaiKhoiLuong")]
    public class NhomHoatDongKhac : BaseObject
    {
        private string _MaLoaiKhoiLuong;
        private string _TenLoaiKhoiLuong;
        private bool _NghiaVu;

        [ModelDefault("Caption", "Mã loại khối lượng")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public string MaLoaiKhoiLuong
        {
            get { return _MaLoaiKhoiLuong; }
            set { SetPropertyValue("MaLoaiKhoiLuong", ref _MaLoaiKhoiLuong, value); }
        }

        [ModelDefault("Caption", "Tên loại khối lượng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenLoaiKhoiLuong
        {
            get { return _TenLoaiKhoiLuong; }
            set { SetPropertyValue("TenLoaiKhoiLuong", ref _TenLoaiKhoiLuong, value); }
        }

        [ModelDefault("Caption", "Nghĩa vụ")]
        public bool NghiaVu
        {
            get { return _NghiaVu; }
            set { SetPropertyValue("NghiaVu", ref _NghiaVu, value); }
        }
        public NhomHoatDongKhac(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
