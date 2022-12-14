using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.TapDieuKien;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.ThuNhapTangThem
{
    [DefaultClassOptions]
    [ImageName("BO_Expression")]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Công thức tính thu nhập tăng thêm")]
    [Appearance("CongThucTinhThuNhapTangThem.CongThucTinhTNCT", TargetItems = "CongThucTinhTNCT", Enabled = false,
        Criteria = "TinhTNCT")]
    public class CongThucTinhThuNhapTangThem : TruongBaseObject, IThongTinTruong
    {
        private string _MaChiTiet;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private string _CongThucTinhSoTien;
        private bool _TinhTNCT;
        private string _CongThucTinhTNCT;
        private ThongTinTruong _ThongTinTruong;
        private string _CongThucTinhBangChu;
        private bool _NgungSuDung;

        [ModelDefault("Caption", "Mã chi tiết")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Ngừng sử dụng")]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }

        private Type ObjectType
        {
            get
            {
                return typeof(DieuKienTongHop);
            }
        }

        [Size(-1)]
        [CriteriaOptions("ObjectType")]
        [ModelDefault("Caption", "Áp dụng cho tất cả cán bộ thỏa điều kiện")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ExtendedCriteriaPropertyEditor")]
        public string DieuKienNhanVien
        {
            get
            {
                return _DieuKienNhanVien;
            }
            set
            {
                SetPropertyValue("DieuKienNhanVien", ref _DieuKienNhanVien, value);
            }
        }

        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.ThuNhap.Luong.ChonGiaTriLapCongThuc";
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính số tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        [Appearance("CongThucTinhBangChu_LUH", TargetItems = "CongThucTinhBangChu", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'LUH'")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }

        [ModelDefault("Caption", "Tính TNCT theo công thức mặc định")]
        [ImmediatePostData()]
        public bool TinhTNCT
        {
            get
            {
                return _TinhTNCT;
            }
            set
            {
                SetPropertyValue("TinhTNCT", ref _TinhTNCT, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính TNCT khác")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TinhTNCT")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhTNCT
        {
            get
            {
                return _CongThucTinhTNCT;
            }
            set
            {
                SetPropertyValue("CongThucTinhTNCT", ref _CongThucTinhTNCT, value);
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

        public CongThucTinhThuNhapTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TinhTNCT = true;
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            CongThucTinhBangChu = "";
        }
    }

}
