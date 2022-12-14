using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.HoSo
{
    [DefaultClassOptions]
    [ImageName("BO_LocDuLieu")]
    [DefaultProperty("TenDieuKien")]
    [ModelDefault("Caption", "Tìm kiếm thỉnh giảng")]
    public class TimKiemThinhGiang : BaseObject
    {
        private string _TenDieuKien;
        private string _DieuKienTimKiem;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public Type GetObjectType
        {
            get
            {
                return typeof(GiangVienThinhGiang);
            }
        }

        [ModelDefault("Caption", "Tên điều kiện")]
        public string TenDieuKien
        {
            get
            {
                return _TenDieuKien;
            }
            set
            {
                SetPropertyValue("TenDieuKien", ref _TenDieuKien, value);
            }
        }

        [ModelDefault("Caption", "Điều kiện tìm kiếm")]
        [CriteriaOptions("GetObjectType")]
        [Size(SizeAttribute.Unlimited)]
        [ImmediatePostData]
        public string DieuKienTimKiem
        {
            get
            {
                return _DieuKienTimKiem;
            }
            set
            {
                SetPropertyValue("DieuKienTimKiem", ref _DieuKienTimKiem, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Danh sách thỉnh giảng")]
        public XPCollection<ChiTietTrichDanhSachThinhGiang> ListChiTietTrichDanhSachThinhGiang { get; set; }

        public TimKiemThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListChiTietTrichDanhSachThinhGiang = new XPCollection<ChiTietTrichDanhSachThinhGiang>(Session, false);
        }
    }

}
