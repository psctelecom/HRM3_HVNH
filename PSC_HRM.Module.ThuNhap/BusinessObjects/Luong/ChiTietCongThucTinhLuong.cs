using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.ThuNhap.Luong
{
    [ImageName("BO_Expression")]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Chi tiết công thức tính lương")]
    [Appearance("ChiTietCongThucTinhLuong", TargetItems = "CongThucTinhTNCT", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTNCT")]
    public class ChiTietCongThucTinhLuong : TruongBaseObject
    {
        private bool _NgungSuDung;
        private CongThucTinhLuong _CongThucTinhLuong;
        private CongTruEnum _CongTru;
        private string _MaChiTiet;
        private string _DienGiai;
        private string _CongThucTinhSoTien;
        private bool _TinhTNCT;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính lương")]
        [Association("CongThucTinhLuong-ListChiTietCongThucTinhLuong")]
        public CongThucTinhLuong CongThucTinhLuong
        {
            get
            {
                return _CongThucTinhLuong;
            }
            set
            {
                SetPropertyValue("CongThucTinhLuong", ref _CongThucTinhLuong, value);
            }
        }

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

        [ImmediatePostData]
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

        [ModelDefault("Caption", "Cộng/Trừ")]
        public CongTruEnum CongTru
        {
            get
            {
                return _CongTru;
            }
            set
            {
                SetPropertyValue("CongTru", ref _CongTru, value);
            }
        }

        public ChiTietCongThucTinhLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TinhTNCT = true;
            CongTru = CongTruEnum.Cong;
            CongThucTinhBangChu = "";
        }
    }

}
