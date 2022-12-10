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


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Loại giảng viên")]
    [DefaultProperty("TenLoaiGiangVien")]
    public class LoaiGiangVienPMS : BaseObject
    {
        private int _MaQuanLy;
        private string _TenLoaiGiangVien;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public int MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên loại giảng viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiGiangVien
        {
            get { return _TenLoaiGiangVien; }
            set { SetPropertyValue("TenLoaiGiangVien", ref _TenLoaiGiangVien, value); }
        }
        public LoaiGiangVienPMS(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
