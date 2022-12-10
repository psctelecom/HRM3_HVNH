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
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.TruyLuong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng truy lĩnh mới")]
    [Appearance("BangTruyLuong.KhoaSo", TargetItems = "DenNgayMLCS;TuNgayMLCS;ChungTu;ThongTinTruong;KyTinhLuong;NgayLap;TuNgay;DenNgay;", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangTruyLuongNew.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]//LoaiLuong;

    //[Appearance("Hide_BUH", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    //[Appearance("Hide_IUH", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    //[Appearance("Hide_UTE", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    //[Appearance("Hide_LUH", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    //[Appearance("Hide_DLU", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    //[Appearance("Hide_HBU", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]

    public class BangTruyLuongNew : TruongBaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTu;
        private ThongTinTruong _ThongTinTruong;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private bool _HienLenWeb;
        private DateTime _TuNgayMLCS;
        private DateTime _DenNgayMLCS;
        //private LoaiLuongEnum _LoaiLuong;

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

        [ModelDefault("Caption", "Truy từ tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartMonth);
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Truy đến tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.EndMonth);
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Truy MLCS từ tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime TuNgayMLCS
        {
            get
            {
                return _TuNgayMLCS;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartMonth);
                SetPropertyValue("TuNgayMLCS", ref _TuNgayMLCS, value);
            }
        }

        [ModelDefault("Caption", "Truy MLCS đến tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime DenNgayMLCS
        {
            get
            {
                return _DenNgayMLCS;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.EndMonth);
                SetPropertyValue("DenNgayMLCS", ref _DenNgayMLCS, value);
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
        [Association("BangTruyLuongNew-ListTruyLuongNhanVienNew")]
        public XPCollection<TruyLuongNhanVienNew> ListTruyLuongNhanVien
        {
            get
            {
                return GetCollection<TruyLuongNhanVienNew>("ListTruyLuongNhanVien");
            }
        }

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Loại lương")]
        //public LoaiLuongEnum LoaiLuong
        //{
        //    get
        //    {
        //        return _LoaiLuong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LoaiLuong", ref _LoaiLuong, value);
        //    }
        //}

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

        public BangTruyLuongNew(Session session) : base(session) { }

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
            //LoaiLuong = 0;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }

}

