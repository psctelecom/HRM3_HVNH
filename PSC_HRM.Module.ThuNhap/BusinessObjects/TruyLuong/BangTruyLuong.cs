using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using System.Collections.Generic;

using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.TruyLuong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng truy lĩnh")]
    [Appearance("BangTruyLuong.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangTruyLuong.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]
    public class BangTruyLuong : BaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTu;
        private ThongTinTruong _ThongTinTruong;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private bool _HienLenWeb;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("KyTinhLuongList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
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

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
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
        [ModelDefault("Caption", "Danh sách truy lương")]
        [Association("BangTruyLuong-ListTruyLuongNhanVien")]
        public XPCollection<TruyLuongNhanVien> ListTruyLuongNhanVien
        {
            get
            {
                return GetCollection<TruyLuongNhanVien>("ListTruyLuongNhanVien");
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

        public BangTruyLuong(Session session) : base(session) { }

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
            //
            UpdateKyTinhLuongList();
        }
    }

}

