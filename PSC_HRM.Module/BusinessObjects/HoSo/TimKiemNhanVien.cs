using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.TapDieuKien;

namespace PSC_HRM.Module.HoSo
{
   [ImageName("BO_LocDuLieu")]
    [ModelDefault("Caption", "Tìm kiếm cán bộ")]
    public class TimKiemNhanVien : BaseObject
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
                return typeof(DieuKienTongHop);
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietTrichDanhSachNhanVien> ListChiTietTrichDanhSachNhanVien { get; set; }

        public TimKiemNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListChiTietTrichDanhSachNhanVien = new XPCollection<ChiTietTrichDanhSachNhanVien>(Session, false);
        }
    }

}
