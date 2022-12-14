using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultProperty("BoPhan")]
    [ImageName("BO_DangKyThiDua")]
    [ModelDefault("Caption", "Chi tiết tập thể đăng ký thi đua")]
    public class ChiTietTapTheDangKyThiDua : BaseObject, IBoPhan
    {
        private ChiTietDangKyThiDua _ChiTietDangKyThiDua;
        private DateTime _NgayDangKy;
        private BoPhan _BoPhan;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Chi tiết đăng ký thi đua")]
        [Association("ChiTietDangKyThiDua-ListChiTietTapTheDangKyThiDua")]
        public ChiTietDangKyThiDua ChiTietDangKyThiDua
        {
            get
            {
                return _ChiTietDangKyThiDua;
            }
            set
            {
                SetPropertyValue("ChiTietDangKyThiDua", ref _ChiTietDangKyThiDua, value);
            }
        }

        [ModelDefault("Caption", "Tập thể")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Ngày đăng ký")]
        public DateTime NgayDangKy
        {
            get
            {
                return _NgayDangKy;
            }
            set
            {
                SetPropertyValue("NgayDangKy", ref _NgayDangKy, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietTapTheDangKyThiDua(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayDangKy = HamDungChung.GetServerTime();
        }
    }

}
