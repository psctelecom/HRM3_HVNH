using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.ThuNhap.NgoaiGio
{
    [DefaultClassOptions]
    [ImageName("BO_Expression")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Công thức tính tiền ngoài giờ")]
    [Appearance("CongThucTinhNgoaiGio.CongThucTinhThue", TargetItems = "CongThucTinhTNCT", Enabled = false,
        Criteria = "TinhTNCT")]
    public class CongThucTinhNgoaiGio : TruongBaseObject, IThongTinTruong
    {
        private ThongTinTruong _ThongTinTruong;
        private string _MaChiTiet;
        private string _DienGiai;
        private string _CongThuc;
        private bool _TinhTNCT = true;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;

        [ModelDefault("Caption", "Mã chi tiết")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập mã chi tiết")]
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
        [RuleRequiredField("", DefaultContexts.Save, "Diễn giải")]
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

        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.ThuNhap.NgoaiGio.ChiTietLuongNgoaiGio";
            }
        }

        [ModelDefault("Caption", "Công thức")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập công thức")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaEditor")]
        [Size(SizeAttribute.Unlimited)]
        public string CongThuc
        {
            get
            {
                return _CongThuc;
            }
            set
            {
                SetPropertyValue("CongThuc", ref _CongThuc, value);
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
        [ImmediatePostData]
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

        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("Caption", "Công thức tính TNCT khác")]
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

        public CongThucTinhNgoaiGio(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            CongThucTinhBangChu = "";
        }
    }

}
