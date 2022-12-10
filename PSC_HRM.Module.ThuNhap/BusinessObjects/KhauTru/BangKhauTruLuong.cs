using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.KhauTru
{
    [DefaultClassOptions]
    [ImageName("BO_KhauTru")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng khấu trừ lương")]
    [Appearance("BangKhauTruLuong.KhoaSo", TargetItems = "ThuBangTienMat;KyTinhLuong;NgayLap;LoaiKhauTruLuong;ListChiTietKhauTruLuong", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangKhauTruLuong.Unique", DefaultContexts.Save, "KyTinhLuong;LoaiKhauTruLuong;NgayLap;ThongTinTruong")]
    public class BangKhauTruLuong : BaseObject, IThongTinTruong
    {
        private bool _ThuBangTienMat;
        private ChungTu.ChungTu _ChungTu;
        private ThongTinTruong _ThongTinTruong;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private LoaiKhauTruLuong _LoaiKhauTruLuong;
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
                if (!IsLoading && value != DateTime.MinValue)
                {
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayLap.Month, NgayLap.Year));
                }
            }
        }

        [ModelDefault("Caption", "Loại khấu trừ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiKhauTruLuong LoaiKhauTruLuong
        {
            get
            {
                return _LoaiKhauTruLuong;
            }
            set
            {
                SetPropertyValue("LoaiKhauTruLuong", ref _LoaiKhauTruLuong, value);
            }
        }

        [ModelDefault("Caption", "Thu bằng tiền mặt")]
        public bool ThuBangTienMat
        {
            get
            {
                return _ThuBangTienMat;
            }
            set
            {
                SetPropertyValue("ThuBangTienMat", ref _ThuBangTienMat, value);
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
        [ModelDefault("Caption", "Chi tiết khấu trừ lương")]
        [Association("BangKhauTruLuong-ListChiTietKhauTruLuong")]
        public XPCollection<ChiTietKhauTruLuong> ListChiTietKhauTruLuong
        {
            get
            {
                return GetCollection<ChiTietKhauTruLuong>("ListChiTietKhauTruLuong");
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
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

        public BangKhauTruLuong(Session session)
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
            UpdateKyTinhLuongList(); 
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
