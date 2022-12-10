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


namespace PSC_HRM.Module.ThuNhap.BoSungLuong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")] 
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bổ sung lương nhân viên")]
    [Appearance("BoSungLuongNhanVien.KhoaSo", TargetItems = "NgayLap;KyTinhLuong", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo)")]
    [RuleCombinationOfPropertiesIsUnique("BoSungLuongNhanVien.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong")]
    public class BoSungLuongNhanVien : TruongBaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTuLuongKy1;
        private ChungTu.ChungTu _ChungTuPhuCapThamNien;
        private ChungTu.ChungTu _ChungTuPhuCapTrachNhiem;
        private ChungTu.ChungTu _ChungTuNangLuongKy1;
        private ChungTu.ChungTu _ChungTuLuongKy2;
        private ChungTu.ChungTu _ChungTuNangLuongKy2;
        private ChungTu.ChungTu _ChungTuTienSi;
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
        [ModelDefault("Caption", "Chi bổ sung lương kỳ 1")]
        [Association("BoSungLuongNhanVien-ListChiBoSungLuongKy1")]
        public XPCollection<ChiBoSungLuongKy1> ListChiBoSungLuongKy1
        {
            get
            {
                return GetCollection<ChiBoSungLuongKy1>("ListChiBoSungLuongKy1");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung lương phụ cấp ưu đãi")]
        [Association("BoSungLuongNhanVien-ListChiBoSungPhuCapUuDai")]
        public XPCollection<ChiBoSungPhuCapUuDai> ListChiBoSungPhuCapUuDai
        {
            get
            {
                return GetCollection<ChiBoSungPhuCapUuDai>("ListChiBoSungPhuCapUuDai");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung lương phụ cấp trách nhiệm")]
        [Association("BoSungLuongNhanVien-ListChiBoSungPhuCapTrachNhiem")]
        public XPCollection<ChiBoSungPhuCapTrachNhiem> ListChiBoSungPhuCapTrachNhiem
        {
            get
            {
                return GetCollection<ChiBoSungPhuCapTrachNhiem>("ListChiBoSungPhuCapTrachNhiem");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung lương phụ cấp thâm niên")]
        [Association("BoSungLuongNhanVien-ListChiBoSungPhuCapThamNien")]
        public XPCollection<ChiBoSungPhuCapThamNien> ListChiBoSungPhuCapThamNien
        {
            get
            {
                return GetCollection<ChiBoSungPhuCapThamNien>("ListChiBoSungPhuCapThamNien");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung lương kỳ 2")]
        [Association("BoSungLuongNhanVien-ListChiBoSungLuongKy2")]
        public XPCollection<ChiBoSungLuongKy2> ListChiBoSungLuongKy2
        {
            get
            {
                return GetCollection<ChiBoSungLuongKy2>("ListChiBoSungLuongKy2");
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung lương phụ cấp tiến sĩ")]
        [Association("BoSungLuongNhanVien-ListChiBoSungLuongPhuCapTienSi")]
        public XPCollection<ChiBoSungPhuCapTienSi> ListChiBoSungLuongPhuCapTienSi
        {
            get
            {
                return GetCollection<ChiBoSungPhuCapTienSi>("ListChiBoSungLuongPhuCapTienSi");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung nâng lương kỳ 1")]
        [Association("BoSungLuongNhanVien-ListChiBoSungNangLuongKy1")]
        public XPCollection<ChiBoSungNangLuongKy1> ListChiBoSungNangLuongKy1
        {
            get
            {
                return GetCollection<ChiBoSungNangLuongKy1>("ListChiBoSungNangLuongKy1");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi bổ sung nâng lương kỳ 2")]
        [Association("BoSungLuongNhanVien-ListChiBoSungNangLuongKy2")]
        public XPCollection<ChiBoSungNangLuongKy2> ListChiBoSungNangLuongKy2
        {
            get
            {
                return GetCollection<ChiBoSungNangLuongKy2>("ListChiBoSungNangLuongKy2");
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuLuongKy1
        {
            get
            {
                return _ChungTuLuongKy1;
            }
            set
            {
                SetPropertyValue("ChungTuLuongKy1", ref _ChungTuLuongKy1, value);
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuPhuCapThamNien
        {
            get
            {
                return _ChungTuPhuCapThamNien;
            }
            set
            {
                SetPropertyValue("ChungTuPhuCapThamNien", ref _ChungTuPhuCapThamNien, value);
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuPhuCapTrachNhiem
        {
            get
            {
                return _ChungTuPhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("ChungTuPhuCapTrachNhiem", ref _ChungTuPhuCapTrachNhiem, value);
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuNangLuongKy1
        {
            get
            {
                return _ChungTuNangLuongKy1;
            }
            set
            {
                SetPropertyValue("ChungTuNangLuongKy1", ref _ChungTuNangLuongKy1, value);
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuLuongKy2
        {
            get
            {
                return _ChungTuLuongKy2;
            }
            set
            {
                SetPropertyValue("ChungTuLuongKy2", ref _ChungTuLuongKy2, value);
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuNangLuongKy2
        {
            get
            {
                return _ChungTuNangLuongKy2;
            }
            set
            {
                SetPropertyValue("ChungTuNangLuongKy2", ref _ChungTuNangLuongKy2, value);
            }
        }

        //chỉ sử dụng để an toàn dữ liệu
        [Browsable(false)]
        public ChungTu.ChungTu ChungTuTienSi
        {
            get
            {
                return _ChungTuTienSi;
            }
            set
            {
                SetPropertyValue("ChungTuTienSi", ref _ChungTuTienSi, value);
            }
        }

        public BoSungLuongNhanVien(Session session)
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
            //
            UpdateKyTinhLuongList();
           
            NgayLap = HamDungChung.GetServerTime();
            //
            if (ThongTinTruong != null) 
                KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("ThongTinTruong = ? and TuNgay<=? and DenNgay>=? and !KhoaSo", ThongTinTruong, NgayLap, NgayLap));
        }
    }

}
