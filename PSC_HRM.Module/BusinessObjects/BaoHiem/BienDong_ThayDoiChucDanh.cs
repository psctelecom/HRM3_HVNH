using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Điều chỉnh chức danh")]
    public class BienDong_ThayDoiChucDanh : BienDong
    {
        // Fields...
        private string _ChucDanhMoi;
        private string _ChucDanhCu;

        [ModelDefault("Caption", "Chức danh cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ChucDanhCu
        {
            get
            {
                return _ChucDanhCu;
            }
            set
            {
                SetPropertyValue("ChucDanhCu", ref _ChucDanhCu, value);
            }
        }

        [ModelDefault("Caption", "Chức danh mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ChucDanhMoi
        {
            get
            {
                return _ChucDanhMoi;
            }
            set
            {
                SetPropertyValue("ChucDanhMoi", ref _ChucDanhMoi, value);
            }
        }

        public BienDong_ThayDoiChucDanh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiBienDong = "Thay đổi chức danh";
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            ChucDanhCu = String.Format("{0} {1}", ThongTinNhanVien.CongViecHienNay != null ? ThongTinNhanVien.CongViecHienNay.TenCongViec : ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null ? ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : "", BoPhan.TenBoPhan);
        }
    }

}
