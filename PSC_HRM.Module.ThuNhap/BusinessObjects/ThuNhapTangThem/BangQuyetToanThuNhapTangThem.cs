using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.ThuNhapTangThem
{
    [DefaultClassOptions]
    [DefaultProperty("Nam")]
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Bảng quyết toán thu nhập tăng thêm")]
    [Appearance("BangQuyetToanThuNhapTangThem.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    public class BangQuyetToanThuNhapTangThem : BaseObject
    {
        // Fields...
        private ChungTu.ChungTu _ChungTu;
        private ThongTinTruong _ThongTinTruong;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private decimal _HeSoTangThem;
        private int _Nam;

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

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số tăng thêm")]
        public decimal HeSoTangThem
        {
            get
            {
                return _HeSoTangThem;
            }
            set
            {
                SetPropertyValue("HeSoTangThem", ref _HeSoTangThem, value);
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

        //chỉ dùng để lưu vết
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangQuyetToanThuNhapTangThem-ListChiTietQuyetToanThuNhapTangThem")]
        public XPCollection<ChiTietQuyetToanThuNhapTangThem> ListChiTietQuyetToanThuNhapTangThem
        {
            get
            {
                return GetCollection<ChiTietQuyetToanThuNhapTangThem>("ListChiTietQuyetToanThuNhapTangThem");
            }
        }

        public BangQuyetToanThuNhapTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            NgayLap = current;
            Nam = current.Year;
            HeSoTangThem = 2.0m;
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            if (ThongTinTruong != null)
                KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("ThongTinTruong = ? and TuNgay<=? and DenNgay>=? and !KhoaSo", ThongTinTruong, NgayLap, NgayLap));
        }
    }

}
