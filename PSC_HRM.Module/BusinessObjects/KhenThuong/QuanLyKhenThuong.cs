using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.KhenThuong
{
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_QuanLyKhenThuong")]
    [ModelDefault("Caption", "Quản lý khen thưởng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc;LoaiKhenThuong")]
    //[Appearance("QuanLyKhenThuong", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    [Appearance("Unhide_BUH", TargetItems = "LoaiKhenThuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE' or MaTruong = 'LUH' or MaTruong = 'HBU' or MaTruong = 'IUH' or MaTruong = 'DLU'")]
    [Appearance("Hide_BUH", TargetItems = "ListKhenThuongTruong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]

    public class QuanLyKhenThuong : BaoMatBaseObject
    {
        private NamHoc _NamHoc;
        private LoaiKhenThuong _LoaiKhenThuong;

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamKhenThuong", ref _NamHoc, value);
            }
        }

        [RuleRequiredField("",DefaultContexts.Save, TargetCriteria = "MaTruong == 'BUH'")]
        [ModelDefault("Caption", "Loại khen thưởng")]
        public LoaiKhenThuong LoaiKhenThuong
        {
            get
            {
                return _LoaiKhenThuong;
            }
            set
            {
                SetPropertyValue("LoaiKhenThuong", ref _LoaiKhenThuong, value);
            }
        }

        


        [Aggregated]
        [ModelDefault("Caption", "Đăng ký thi đua")]
        [Association("QuanLyKhenThuong-ListChiTietDangKyThiDua")]
        public XPCollection<ChiTietDangKyThiDua> ListChiTietDangKyThiDua
        {
            get
            {
                return GetCollection<ChiTietDangKyThiDua>("ListChiTietDangKyThiDua");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hội đồng khen thưởng")]
        [Association("QuanLyKhenThuong-ListHoiDongKhenThuong")]
        public XPCollection<HoiDongKhenThuong> ListHoiDongKhenThuong
        {
            get
            {
                return GetCollection<HoiDongKhenThuong>("ListHoiDongKhenThuong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Đề nghị khen thưởng")]
        [Association("QuanLyKhenThuong-ListChiTietDeNghiKhenThuong")]
        public XPCollection<ChiTietDeNghiKhenThuong> ListChiTietDeNghiKhenThuong
        {
            get
            {
                return GetCollection<ChiTietDeNghiKhenThuong>("ListChiTietDeNghiKhenThuong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Khen thưởng của Trường")]
        [Association("QuanLyKhenThuong-ListKhenThuongTruong")]
        public XPCollection<KhenThuongTruong> ListKhenThuongTruong
        {
            get
            {
                return GetCollection<KhenThuongTruong>("ListKhenThuongTruong");
            }
        }

        [NonPersistent]
        protected string MaTruong { get; set; }

        public QuanLyKhenThuong(Session session) : base(session) { }
        
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            MaTruong = TruongConfig.MaTruong;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            MaTruong = TruongConfig.MaTruong;
        }
    }

}
