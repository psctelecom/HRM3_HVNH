using System;

using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Ưng viên")]
    public class TuyenDung_ThiSinh : BaseObject
    {
        // Fields...
        private bool _Chon;
        private UngVien _UngVien;

        [ModelDefault("Caption", "Chọn")]
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

        [ModelDefault("Caption", "Ứng viên")]
        public UngVien UngVien
        {
            get
            {
                return _UngVien;
            }
            set
            {
                SetPropertyValue("UngVien", ref _UngVien, value);
            }
        }

        public TuyenDung_ThiSinh(Session session) : base(session) { }
    }

}
