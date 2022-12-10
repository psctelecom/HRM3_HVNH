using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Quản lý kiêm giảng")]
    [Appearance("Hide_KiemGiang", TargetItems = "Caption", Visibility = ViewItemVisibility.Hide)]
    [DefaultProperty("Caption")]
    public class KiemGiang : BaseObject
    {
        private NhanVien _NhanVien;
        private string _GhiChu;

        [ModelDefault("Caption", "Giảng viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [ImmediatePostData]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);       
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} {1}", NhanVien != null ? NhanVien.HoTen : "", GhiChu);
            }
        }

        public KiemGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }   
    }
}
