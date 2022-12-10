using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenLoaiHoatDong")]
    [ModelDefault("Caption", "Loại hoạt động")]

    public class LoaiHoatDong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHoatDong;
        private string _CongThucQD;
        private NgonNguEnum? _GioGiangDay;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_NgoaiGiangDay";
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hoạt động")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiHoatDong
        {
            get { return _TenLoaiHoatDong; }
            set { SetPropertyValue("TenLoaiHoatDong", ref _TenLoaiHoatDong, value); }
        }
        [ModelDefault("Caption", "Công thức quy đổi")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string CongThucQD
        {
            get { return _CongThucQD; }
            set { SetPropertyValue("CongThucQD", ref _CongThucQD, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguEnum? GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue(" GioGiangDay", ref _GioGiangDay, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue(" BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue(" HeDaoTao", ref _HeDaoTao, value); }
        }
        public LoaiHoatDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}