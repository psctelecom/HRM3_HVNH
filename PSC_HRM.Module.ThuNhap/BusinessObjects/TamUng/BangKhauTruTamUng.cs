using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ThuNhap.TamUng
{
    [DefaultClassOptions]
    [ImageName("BO_KhauTru")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng khấu trừ tạm ứng")]
    [Appearance("BangKhauTruTamUng.Khoa", TargetItems = "NgayLap;KyTinhLuong;ListChiTietKhauTruTamUng", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangKhauTruTamUng.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]
    public class BangKhauTruTamUng : BaseObject, IThongTinTruong
    {
        // Fields...
        private ChungTu.ChungTu _ChungTu;
        private ThongTinTruong _ThongTinTruong;
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;
        private bool _HienLenWeb;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Hiện lên web")]
        public bool HienLenWeb
        {
            get
            {
                return _HienLenWeb;
            }
            set
            {
                SetPropertyValue("HienLenWeb", ref _HienLenWeb, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin trường")]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangKhauTruTamUng-ListChiTietKhauTruTamUng")]
        public XPCollection<ChiTietKhauTruTamUng> ListChiTietKhauTruTamUng
        {
            get
            {
                return GetCollection<ChiTietKhauTruTamUng>("ListChiTietKhauTruTamUng");
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

        public BangKhauTruTamUng(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NgayLap = current;
            if (ThongTinTruong != null)
                KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("ThongTinTruong=? and TuNgay<=? and DenNgay>=? and !KhoaSo", ThongTinTruong.Oid, NgayLap, NgayLap));
        }
    }

}
