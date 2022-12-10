using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.Luong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")] 
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng lương nhân viên")]
    [Appearance("BangLuongNhanVien.KhoaSo", TargetItems = "NgayLap;KyTinhLuong;LoaiLuong;ListLuongNhanVien", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangLuongNhanVien.Unique", DefaultContexts.Save, "KyTinhLuong;LoaiLuong;ThongTinTruong")]
    [Appearance("Hide.NEU", TargetItems = "LoaiLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    
    public class BangLuongNhanVien : TruongBaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTu;
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;
        private ThongTinTruong _ThongTinTruong;
        private LoaiLuongEnum _LoaiLuong;
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
                if (!IsLoading && value != null)
                {
                    if (MaTruong.Equals("QNU") && value.BangChotThongTinTinhLuong != null)
                        LoaiLuong = value.BangChotThongTinTinhLuong.LoaiLuong;
                }
                    
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong = 'QNU'")]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangLuongNhanVien-ListLuongNhanVien")]
        public XPCollection<LuongNhanVien> ListLuongNhanVien
        {
            get
            {
                return GetCollection<LuongNhanVien>("ListLuongNhanVien");
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

        public BangLuongNhanVien(Session session)
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
            LoaiLuong = 0;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }

}
