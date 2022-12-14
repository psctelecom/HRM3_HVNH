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


namespace PSC_HRM.Module.ThuNhap.KhauTru
{
    [ImageName("BO_Expression")]
    [DefaultProperty("DienGiai")]
    [ModelDefault("Caption", "Công thức khấu trừ lương")]
    public class CongThucTinhKhauTru : TruongBaseObject, IThongTinTruong
    {
        private ThongTinTruong _ThongTinTruong;
        private string _MaQuanLy;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhBangChu;

        [ModelDefault("Caption", "Mã quản lý")]
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
                return typeof(DieuKienTongHop);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Điều kiện áp dụng")]
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
                return "PSC_HRM.Module.ThuNhap.KhauTru.ChiTietKhauTruLuong";
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính số tiền")]
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

        public CongThucTinhKhauTru(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            CongThucTinhBangChu = "";
        }
    }

}
