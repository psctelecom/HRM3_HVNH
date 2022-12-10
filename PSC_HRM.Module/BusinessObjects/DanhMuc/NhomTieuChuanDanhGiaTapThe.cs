using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNhomTieuChuanDanhGiaTapThe")]
    [ModelDefault("Caption", "Nhóm tiêu chuẩn đánh giá tập thể")]
    public class NhomTieuChuanDanhGiaTapThe : BaseObject
    {
        private string _TenNhomTieuChuanDanhGiaTapThe;
        private string _DieuKienApDung;
        private bool _LuuTru;

        [ModelDefault("Caption", "Tên nhóm tiêu chuẩn đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomTieuChuanDanhGiaTapThe
        {
            get
            {
                return _TenNhomTieuChuanDanhGiaTapThe;
            }
            set
            {
                SetPropertyValue("TenNhomTieuChuanDanhGiaTapThe", ref _TenNhomTieuChuanDanhGiaTapThe, value);
            }
        }

        [Size(-1)]
        [CriteriaOptions("ObjectType")]
        [ModelDefault("Caption", "Điều kiện áp dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ExtendedCriteriaPropertyEditor")]
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

        // lữu trữ mỗi lần đánh giá
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        public bool LuuTru
        {
            get
            {
                return _LuuTru;
            }
            set
            {
                SetPropertyValue("LuuTru", ref _LuuTru, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tiêu chí đánh giá")]
        [Association("NhomTieuChuanDanhGiaTapThe-ListTieuChuanDanhGiaTapThe")]
        public XPCollection<TieuChuanDanhGiaTapThe> ListTieuChuanDanhGiaTapThe
        {
            get
            {
                return GetCollection<TieuChuanDanhGiaTapThe>("ListTieuChuanDanhGiaTapThe");
            }
        }

        private Type ObjectType
        {
            get
            {
                return typeof(BoPhan);
            }
        }

        public NhomTieuChuanDanhGiaTapThe(Session session) : base(session) { }
    }

}
