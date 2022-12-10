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

    [ModelDefault("Caption", "Phân loại học phần")]
    [DefaultProperty("TenPhanLoai")]
    public class DanhMucLoaiKhongPhanThoiKhoaBieu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenPhanLoai;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên phân loại")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenPhanLoai
        {
            get { return _TenPhanLoai; }
            set { SetPropertyValue("TenPhanLoai", ref _TenPhanLoai, value); }
        }
        public DanhMucLoaiKhongPhanThoiKhoaBieu(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
