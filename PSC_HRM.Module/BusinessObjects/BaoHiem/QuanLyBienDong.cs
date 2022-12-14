using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý biến động")]
    [DefaultProperty("Caption")]
    [Appearance("QuanLyBienDong.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;ThoiGian;Dot")]
    public class QuanLyBienDong : BaoMatBaseObject
    {
        // Fields...
        private bool _KhoaSo;
        private decimal _SoPhaiDongBHTNKyTruoc;
        private decimal _SoPhaiDongBHYTKyTruoc;
        private decimal _SoPhaiDongBHXHKyTruoc;
        private decimal _QuyLuongBHTNKyTruoc;
        private decimal _QuyLuongBHYTKyTruoc;
        private decimal _QuyLuongBHXHKyTruoc;
        private int _SoLaoDongBHTNKyTruoc;
        private int _SoLaoDongBHYTKyTruoc;
        private int _SoLaoDongBHXHKyTruoc;
        private int _Dot = 1;
        private DateTime _ThoiGian;

        [ModelDefault("Caption", "Thời gian")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
                CreateCaption();
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
                CreateCaption();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [ModelDefault("Caption", "Số lao động BHXH kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoLaoDongBHXHKyTruoc
        {
            get
            {
                return _SoLaoDongBHXHKyTruoc;
            }
            set
            {
                SetPropertyValue("SoLaoDongBHXHKyTruoc", ref _SoLaoDongBHXHKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Số lao động BHYT kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoLaoDongBHYTKyTruoc
        {
            get
            {
                return _SoLaoDongBHYTKyTruoc;
            }
            set
            {
                SetPropertyValue("SoLaoDongBHYTKyTruoc", ref _SoLaoDongBHYTKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Số lao động BHTN kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoLaoDongBHTNKyTruoc
        {
            get
            {
                return _SoLaoDongBHTNKyTruoc;
            }
            set
            {
                SetPropertyValue("SoLaoDongBHTNKyTruoc", ref _SoLaoDongBHTNKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Quỹ lương BHXH kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QuyLuongBHXHKyTruoc
        {
            get
            {
                return _QuyLuongBHXHKyTruoc;
            }
            set
            {
                SetPropertyValue("QuyLuongBHXHKyTruoc", ref _QuyLuongBHXHKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Quỹ lương BHYT kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QuyLuongBHYTKyTruoc
        {
            get
            {
                return _QuyLuongBHYTKyTruoc;
            }
            set
            {
                SetPropertyValue("QuyLuongBHYTKyTruoc", ref _QuyLuongBHYTKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Quỹ lương BHTN kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal QuyLuongBHTNKyTruoc
        {
            get
            {
                return _QuyLuongBHTNKyTruoc;
            }
            set
            {
                SetPropertyValue("QuyLuongBHTNKyTruoc", ref _QuyLuongBHTNKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Số phải đóng BHXH kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoPhaiDongBHXHKyTruoc
        {
            get
            {
                return _SoPhaiDongBHXHKyTruoc;
            }
            set
            {
                SetPropertyValue("SoPhaiDongBHXHKyTruoc", ref _SoPhaiDongBHXHKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Số phải đóng BHYT kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoPhaiDongBHYTKyTruoc
        {
            get
            {
                return _SoPhaiDongBHYTKyTruoc;
            }
            set
            {
                SetPropertyValue("SoPhaiDongBHYTKyTruoc", ref _SoPhaiDongBHYTKyTruoc, value);
            }
        }

        [ModelDefault("Caption", "Số phải đóng BHTN kỳ trước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoPhaiDongBHTNKyTruoc
        {
            get
            {
                return _SoPhaiDongBHTNKyTruoc;
            }
            set
            {
                SetPropertyValue("SoPhaiDongBHTNKyTruoc", ref _SoPhaiDongBHTNKyTruoc, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string Caption { get; private set; }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách biến động")]
        [Association("QuanLyBienDong-ListBienDong")]
        public XPCollection<BienDong> ListBienDong
        {
            get
            {
                return GetCollection<BienDong>("ListBienDong");
            }
        }

        public QuanLyBienDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThoiGian == DateTime.MinValue)
                ThoiGian = HamDungChung.GetServerTime();
        }

        private void CreateCaption()
        {
            if (ThoiGian != DateTime.MinValue && Dot > 0)
                Caption = String.Format("Tháng {0} (Đợt {1})", ThoiGian.ToString("MM/yyyy"), Dot);
        }
    }

}
