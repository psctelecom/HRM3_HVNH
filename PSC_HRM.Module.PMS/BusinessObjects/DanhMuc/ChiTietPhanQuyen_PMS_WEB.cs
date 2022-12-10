
using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.BusinessObjects.DanhMuc
{
    [ModelDefault("Caption", "Chi tiết  phân quyền sử dụng PMS (WEB)")]
    [Appearance("VHU_Hide", TargetItems = "BoPhanImport;BoPhanXacNhan;OidBoPhanImport;OidBoPhanXacNhan", Visibility = ViewItemVisibility.Hide, Criteria = "PhanQuyen_PMS_WEB.ThongTinTruong.TenVietTat = 'VHU'")]
    public class ChiTietPhanQuyen_PMS_WEB : BaseObject
    {
        #region key
        private PhanQuyen_PMS_WEB _PhanQuyen_PMS_WEB;
        [ModelDefault("Caption", "Quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("PhanQuyen_PMS_WEB-ListChiTiet")]

        public PhanQuyen_PMS_WEB PhanQuyen_PMS_WEB
        {
            get
            {
                return _PhanQuyen_PMS_WEB;
            }
            set
            {
                SetPropertyValue("PhanQuyen_PMS_WEB", ref _PhanQuyen_PMS_WEB, value);
            }
        }
        #endregion
        //private string _TimKiem;
        private ThongTinNhanVien _NhanVien;
        private string _BoPhanImport;
        private string _BoPhanXacNhan;
        private string _OidBoPhanImport;
        private string _OidBoPhanXacNhan;
        private bool _XemPhanCongTongHop;


        //[NonPersistent]
        //[ModelDefault("Caption", "Tìm kiếm")]
        //[VisibleInListView(false)]
        //public string TimKiem
        //{
        //    get { return _TimKiem; }
        //    set
        //    {
        //        SetPropertyValue("TimKiem", ref _TimKiem, value);
        //        if (!IsLoading)
        //        {
        //            if (!string.IsNullOrWhiteSpace(TimKiem))
        //            {
        //                NhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy =? or SoHieuCongChuc =? or HoTen =?", TimKiem, TimKiem, TimKiem));
        //            }
        //        }
        //    }
        //}

        [ModelDefault("Caption", "Cán bộ")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinNhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }


        [ModelDefault("Caption", "Đơn vị - Import")]
        [ModelDefault("AllowEdit", "False")]
        [Size(-1)]
        public string BoPhanImport
        {
            get { return _BoPhanImport; }
            set { SetPropertyValue("BoPhanImport", ref _BoPhanImport, value); }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "OID - Import")]
        [Size(-1)]
        public string OidBoPhanImport
        {
            get { return _OidBoPhanImport; }
            set { SetPropertyValue("OidBoPhanImport", ref _OidBoPhanImport, value); }
        }

        [ModelDefault("Caption", "Đơn vị - Xác nhận")]
        [ModelDefault("AllowEdit", "False")]
        [Size(-1)]
        public string BoPhanXacNhan
        {
            get { return _BoPhanXacNhan; }
            set { SetPropertyValue("BoPhanXacNhan", ref _BoPhanXacNhan, value); }
        }


        [ModelDefault("Caption", "Oid - Xác nhận")]
        [Size(-1)]
        [Browsable(false)]
        public string OidBoPhanXacNhan
        {
            get { return _OidBoPhanXacNhan; }
            set { SetPropertyValue("OidBoPhanXacNhan", ref _OidBoPhanXacNhan, value); }
        }

        [ModelDefault("Caption", "XemPhanCongTongHop")]
        public bool XemPhanCongTongHop
        {
            get { return _XemPhanCongTongHop; }
            set { SetPropertyValue("XemPhanCongTongHop", ref _XemPhanCongTongHop, value); }
        }
        public ChiTietPhanQuyen_PMS_WEB(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}