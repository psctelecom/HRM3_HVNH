using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_List")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng định mức nộp thuế TNCN")]
    [Appearance("BangDinhMucNopThueTNCN.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangDinhMucNopThueTNCN.Unique", DefaultContexts.Save,"KyTinhLuong")]
    public class BangDinhMucNopThueTNCN : BaseObject, IThongTinTruong
    {
        // Fields...
        //private int _Nam;
        private ChungTu.ChungTu _ChungTu;
        private KyTinhLuong _KyTinhLuong;
        private BaoMat.ThongTinTruong _ThongTinTruong;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleUniqueValue(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangDinhMucNopThueTNCN-ListChiTietDinhMucNopThueTNCN")]
        public XPCollection<ChiTietDinhMucNopThueTNCN> ListChiTietDinhMucNopThueTNCN
        {
            get
            {
                return GetCollection<ChiTietDinhMucNopThueTNCN>("ListChiTietDinhMucNopThueTNCN");
            }
        }

        [Browsable(false)]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        //chỉ dùng để truy vết
        [Browsable(false)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        public BangDinhMucNopThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
    }

}
