using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.NonPersistentObjects.DanhMuc_View
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chung")]
    public class BoPhan_NhanVien : BaseObject
    {
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Cán bộ/Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        public BoPhan_NhanVien(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initializaion code.
        }
    }

}