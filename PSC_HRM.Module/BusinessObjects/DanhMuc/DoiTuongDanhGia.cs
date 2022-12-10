using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.TapDieuKien;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenDoiTuongDanhGia")]
    [ModelDefault("Caption", "Đối tượng đánh giá")]
    [RuleCombinationOfPropertiesIsUnique("DoiTuongDanhGia.Unique", DefaultContexts.Save, "MaQuanLy;TenDoiTuongDanhGia")]
    [Appearance("QuyetDinh.Khac", TargetItems = "DieuKienApDung", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=6")]
    
    public class DoiTuongDanhGia : TruongBaseObject
    {
        // Fields...
        private DoiTuongDanhGiaEnum _PhanLoai = DoiTuongDanhGiaEnum.GiangVien;
        private string _DieuKienApDung;
        private string _MaQuanLy;
        private string _TenDoiTuongDanhGia;
        private bool _DanhGia;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên đối tượng đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDoiTuongDanhGia
        {
            get
            {
                return _TenDoiTuongDanhGia;
            }
            set
            {
                SetPropertyValue("TenDoiTuongDanhGia", ref _TenDoiTuongDanhGia, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public DoiTuongDanhGiaEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá")]
        public bool DanhGia
        {
            get
            {
                return _DanhGia;
            }
            set
            {
                SetPropertyValue("DanhGia", ref _DanhGia, value);
            }
        }

        [Size(-1)]
        [CriteriaOptions("ObjectType")]
        [ModelDefault("Caption", "Điều kiện áp dụng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoai=6")]
        public string DieuKienApDung
        {
            get
            {
                return _DieuKienApDung;
            }
            set
            {
                SetPropertyValue("DieuKienApDung", ref _DieuKienApDung, value);
            }
        }

        private Type ObjectType
        {
            get
            {
                return typeof(DieuKienTongHop);
            }
        }

        public DoiTuongDanhGia(Session session) : base(session) { }
    }

}
