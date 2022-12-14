using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.TapDieuKien;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.PMS.DieuKien;


namespace PSC_HRM.Module.PMS.NghiepVu
{
    [DefaultClassOptions]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Công thức tính thù lao giảng dạy")]
    [ImageName("BO_Expression")]    
    public class CongThucTinhThuLaoGiangDay : TruongBaseObject, IThongTinTruong
    {
        private bool _NgungSuDung;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private ThongTinTruong _ThongTinTruong;

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
                return typeof(DieuKien_ThuLaoGiangDay);
            }
        }

        [Size(-1)]
        [CriteriaOptions("ObjectType")]
        [ModelDefault("Caption", "Điều kiện áp dụng")]
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
        [ModelDefault("Caption", "Danh sách công thức")]
        [Association("CongThucTinhThuLaoGiangDay-ListChiTietCongThuc")]
        public XPCollection<ChiTietCongThucTinhThuLaoGiangDay> ListChiTietCongThuc
        {
            get
            {
                return GetCollection<ChiTietCongThucTinhThuLaoGiangDay>("ListChiTietCongThuc");
            }
        }
        public CongThucTinhThuLaoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
    }

}
