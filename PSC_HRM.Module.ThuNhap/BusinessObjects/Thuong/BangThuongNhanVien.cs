using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Thuong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Bảng thưởng - phúc lợi cán bộ")]
    [Appearance("BangThuongNhanVien.KhoaSo", TargetItems = "LoaiKhenThuongPhucLoi;KyTinhLuong;NgayLap;ListChiTietThuongNhanVien", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangThuongNhanVien.Unique", DefaultContexts.Save, 
        "KyTinhLuong;LoaiKhenThuongPhucLoi;NgayLap;ThongTinTruong")]
    public class BangThuongNhanVien : BaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTu;
        private LoaiKhenThuongPhucLoi _LoaiKhenThuongPhucLoi;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private ThongTinTruong _ThongTinTruong;
        private bool _HienLenWeb;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
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

        [ModelDefault("Caption", "Loại Khen thưởng - Phúc lợi")]
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
        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangThuongNhanVien-ListChiTietThuongNhanVien")]
        public XPCollection<ChiTietThuongNhanVien> ListChiTietThuongNhanVien
        {
            get
            {
                return GetCollection<ChiTietThuongNhanVien>("ListChiTietThuongNhanVien");
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


        public BangThuongNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);

            if (ThongTinTruong != null)
                KyTinhLuongList.Criteria = CriteriaOperator.Parse("ThongTinTruong=? and !KhoaSo", ThongTinTruong);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateKyTinhLuongList();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NgayLap = HamDungChung.GetServerTime();
            if (ThongTinTruong != null)
                KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("ThongTinTruong = ? and TuNgay<=? and DenNgay>=? and !KhoaSo", ThongTinTruong, NgayLap, NgayLap));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateKyTinhLuongList();
        }
    }
}
