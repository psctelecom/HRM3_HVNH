using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Thuong
{
    [ImageName("BO_Expression")]
    [ModelDefault("IsCloneable", "True")]
    [DefaultProperty("CongThucTinhSoTienNhan")]
    [ModelDefault("Caption", "Công thức tính thưởng")]
    [Appearance("CongThucKhenThuongPhucLoi.TinhThueTNCN", TargetItems = "CongThucTinhTNCT", Enabled = false,
        Criteria = "TinhTNCT")]
    public class CongThucKhenThuongPhucLoi : TruongBaseObject, IThongTinTruong
    {
        private ThongTinTruong _ThongTinTruong;
        private LoaiKhenThuongPhucLoi _LoaiKhenThuongPhucLoi;
        private string _MaQuanLy;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private string _CongThucTinhSoTienNhan;
        private bool _TinhTNCT = true;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;

        [ModelDefault("Caption", "Loại thưởng, phụ cấp")]
        [Association("LoaiKhenThuongPhucLoi-CongThucKhenThuongPhucLoi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiKhenThuongPhucLoi LoaiKhenThuongPhucLoi
        {
            get
            {
                return _LoaiKhenThuongPhucLoi;
            }
            set
            {
                SetPropertyValue("LoaiKhenThuongPhucLoi", ref _LoaiKhenThuongPhucLoi, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
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

        private Type ObjectType
        {
            get
            {
                return typeof(TapDieuKien.DieuKienTongHop);
            }
        }

        [ModelDefault("Caption", "Điều kiện áp dụng cho các Cán bộ")]
        [Size(-1)]
        [CriteriaOptions("ObjectType")]
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
                return "PSC_HRM.Module.ThuNhap.Thuong.ChiTietThuongNhanVien";
            }
        }

        [Size(-1)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Công thức tính số tiền nhận")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        public string CongThucTinhSoTienNhan
        {
            get
            {
                return _CongThucTinhSoTienNhan;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienNhan", ref _CongThucTinhSoTienNhan, value);
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

        [ImmediatePostData()]
        [ModelDefault("Caption", "Tính TNCT theo công thức mặc định")]
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
        [ModelDefault("Caption", "Công thức tính TNCT")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TinhTNCT")]
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

        public CongThucKhenThuongPhucLoi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            CongThucTinhBangChu = "";
        }
    }

}
