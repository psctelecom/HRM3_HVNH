using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenHoatDong")]
    [ModelDefault("Caption", "Nhóm hoạt động")]

    public class NhomHoatDong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHoatDong;

        [ModelDefault("Caption", "Mã quản lý")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hoạt động")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh Sách chi tiết")]
        [Association("NhomHoatDong-ListDanhSachChiTietHDKhac")]
        public XPCollection<DanhSachChiTietHDKhac> ListDanhSachChiTietHDKhac
        {
            get
            {
                return GetCollection<DanhSachChiTietHDKhac>("ListDanhSachChiTietHDKhac");
            }
        }
        public NhomHoatDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}