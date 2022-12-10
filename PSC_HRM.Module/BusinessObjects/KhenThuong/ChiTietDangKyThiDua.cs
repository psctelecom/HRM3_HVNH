using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultProperty("DanhHieu")]
    [ImageName("BO_DangKyThiDua")]
    [ModelDefault("Caption", "Chi tiết đăng ký thi đua")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDangKyThiDua", DefaultContexts.Save, "QuanLyKhenThuong;DanhHieuKhenThuong")]
    public class ChiTietDangKyThiDua : BaseObject
    {
        // Fields...
        private DateTime _NgayLap;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private QuanLyKhenThuong _QuanLyKhenThuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý khen thưởng")]
        [Association("QuanLyKhenThuong-ListChiTietDangKyThiDua")]
        public QuanLyKhenThuong QuanLyKhenThuong
        {
            get
            {
                return _QuanLyKhenThuong;
            }
            set
            {
                SetPropertyValue("QuanLyKhenThuong", ref _QuanLyKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cá nhân")]
        [Association("ChiTietDangKyThiDua-ListChiTietCaNhanDangKyThiDua")]
        public XPCollection<ChiTietCaNhanDangKyThiDua> ListChiTietCaNhanDangKyThiDua
        {
            get
            {
                return GetCollection<ChiTietCaNhanDangKyThiDua>("ListChiTietCaNhanDangKyThiDua");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tập thể")]
        [Association("ChiTietDangKyThiDua-ListChiTietTapTheDangKyThiDua")]
        public XPCollection<ChiTietTapTheDangKyThiDua> ListChiTietTapTheDangKyThiDua
        {
            get
            {
                return GetCollection<ChiTietTapTheDangKyThiDua>("ListChiTietTapTheDangKyThiDua");
            }
        }

        public ChiTietDangKyThiDua(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
        }
    }

}
