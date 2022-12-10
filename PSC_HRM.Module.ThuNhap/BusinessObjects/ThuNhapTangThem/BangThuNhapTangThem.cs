using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.Collections.Generic;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.ThuNhapTangThem
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [ModelDefault("Caption", "Bảng thu nhập tăng thêm")]
    [DefaultProperty("KyTinhLuong")]
    [Appearance("BangThuNhapTangThem.KhoaSo", TargetItems = "NgayLap;KyTinhLuong;ListChiTietThuNhapTangThem", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    //[RuleCombinationOfPropertiesIsUnique("BangThuNhapTangThem.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]
    public class BangThuNhapTangThem : BaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTu;
        private decimal _HeSoTangThem;
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;
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
                if (!IsLoading)
                {
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang = ? and Nam = ? and !KhoaSo", NgayLap.Month, NgayLap.Year));
                }
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
        [Association("BangThuNhapTangThem-ListChiTietThuNhapTangThem")]
        public XPCollection<ChiTietThuNhapTangThem> ListChiTietThuNhapTangThem
        {
            get
            {
                return GetCollection<ChiTietThuNhapTangThem>("ListChiTietThuNhapTangThem");
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

        [ModelDefault("Caption", "Hệ số tăng thêm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        public BangThuNhapTangThem(Session session)
            : base(session)
        { }

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
            //
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NgayLap = HamDungChung.GetServerTime();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateKyTinhLuongList();
        }
    }

}
