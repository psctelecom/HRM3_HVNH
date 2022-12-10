using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.ThuNhapKhac
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng thu nhập khác")]
    [Appearance("BangThuNhapKhac.Khoa", TargetItems = "LoaiThuNhapKhac;KyTinhLuong;NgayLap;ListChiTietThuNhapKhac", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangThuNhapKhac.Unique", DefaultContexts.Save, "KyTinhLuong;LoaiThuNhapKhac;ThongTinTruong")]
    public class BangThuNhapKhac : BaseObject, IThongTinTruong
    {
        private LoaiThuNhapKhac _LoaiThuNhapKhac;
        private ChungTu.ChungTu _ChungTu;
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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Loại thu nhập khác")]
        public LoaiThuNhapKhac LoaiThuNhapKhac
        {
            get
            {
                return _LoaiThuNhapKhac;
            }
            set
            {
                SetPropertyValue("LoaiThuNhapKhac", ref _LoaiThuNhapKhac, value);
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
                if(!IsLoading && value !=DateTime.MinValue)
                {
                  KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayLap.Month, NgayLap.Year));
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
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangThuNhapKhac-ListChiTietThuNhapKhac")]
        public XPCollection<ChiTietThuNhapKhac> ListChiTietThuNhapKhac
        {
            get
            {
                return GetCollection<ChiTietThuNhapKhac>("ListChiTietThuNhapKhac");
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


        public BangThuNhapKhac(Session session) : base(session) { }

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
            UpdateKyTinhLuongList();
            //
            NgayLap = HamDungChung.GetServerTime();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }

}
