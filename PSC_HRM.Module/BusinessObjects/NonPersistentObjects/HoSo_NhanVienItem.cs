using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn cán bộ")]
    public class HoSo_NhanVienItem : BaseObject
    {
        // Fields...
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _Chon;

        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        public HoSo_NhanVienItem(Session session)
            : base(session)
        { }
    }

}
