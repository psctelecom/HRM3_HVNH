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
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;


namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn nhân viên")]
    
    public class ChonNhanVien_PhanQuyenPMS : BaseObject
    {
        private string _TimKiem;
        private ThongTinNhanVien _NhanVien;
        private string _BoPhanImport;
        private string _BoPhanXacNhan;


        [NonPersistent]
        [ModelDefault("Caption", "Tìm kiếm")]
        [VisibleInListView(false)]
        public string TimKiem
        {
            get { return _TimKiem; }
            set
            {
                SetPropertyValue("TimKiem", ref _TimKiem, value);
                if (!IsLoading)
                {
                    if (!string.IsNullOrWhiteSpace(TimKiem))
                    {
                        NhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy =? or SoHieuCongChuc =? or HoTen =?", TimKiem, TimKiem, TimKiem));
                    }
                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Đơn vị - Import")]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.chkComboxEdit_DonVi")]
        public string BoPhanImport
        {
            get { return _BoPhanImport; }
            set { SetPropertyValue("BoPhanImport", ref _BoPhanImport, value); }
        }

        [ModelDefault("Caption", "Đơn vị - Xác nhận")]
        [Size(-1)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.chkComboxEdit_ChonBoPhan_WEBPMS")]
        public string BoPhanXacNhan
        {
            get { return _BoPhanXacNhan; }
            set { SetPropertyValue("BoPhanXacNhan", ref _BoPhanXacNhan, value); }
        }
        public ChonNhanVien_PhanQuyenPMS(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();     
        }
    }

}
