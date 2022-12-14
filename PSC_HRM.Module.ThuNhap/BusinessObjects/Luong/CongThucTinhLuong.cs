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


namespace PSC_HRM.Module.ThuNhap.Luong
{
    [DefaultClassOptions]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Công thức tính lương")]
    [ImageName("BO_Expression")]

    //[Appearance("Hide_BUH", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    //[Appearance("Hide_IUH", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    [Appearance("Hide_UTE", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    [Appearance("Hide_LUH", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    [Appearance("Hide_DLU", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    [Appearance("Hide_HBU", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]
    
    public class CongThucTinhLuong : TruongBaseObject, IThongTinTruong
    {
        private bool _NgungSuDung;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private ThongTinTruong _ThongTinTruong;
        private LoaiLuongEnum _LoaiLuong;

        public CongThucTinhLuong(Session session) : base(session) { }

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

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại lương")]
        //[Appearance("LoaiLuong_Hide", TargetItems = "LoaiLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='LUH' or MaTruong ='UTE' or MaTruong ='HBU' or MaTruong ='DLU' ")]
        public LoaiLuongEnum LoaiLuong
        {
            get
            {
                return _LoaiLuong;
            }
            set
            {
                SetPropertyValue("LoaiLuong", ref _LoaiLuong, value);
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
        [ModelDefault("Caption", "Danh sách công thức tính lương")]
        [Association("CongThucTinhLuong-ListChiTietCongThucTinhLuong")]
        public XPCollection<ChiTietCongThucTinhLuong> ListChiTietCongThucTinhLuong
        {
            get
            {
                return GetCollection<ChiTietCongThucTinhLuong>("ListChiTietCongThucTinhLuong");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            LoaiLuong = 0;
        }
    }

}
