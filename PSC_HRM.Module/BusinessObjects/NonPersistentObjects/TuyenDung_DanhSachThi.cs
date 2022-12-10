using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách ứng viên")]
    public class TuyenDung_DanhSachThi : BaseObject
    {
        // Fields...
        private bool _ChonTatCa;

        [ImmediatePostData]
        [ModelDefault("Caption", "Chọn tất cả")]
        public bool ChonTatCa
        {
            get
            {
                return _ChonTatCa;
            }
            set
            {
                SetPropertyValue("ChonTatCa", ref _ChonTatCa, value);
                if (!IsLoading)
                    CheckUngVien();
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ứng viên")]
        public XPCollection<TuyenDung_ThiSinh> ListUngVien { get; set; }

        public TuyenDung_DanhSachThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListUngVien = new XPCollection<TuyenDung_ThiSinh>(Session, false);
        }

        private void CheckUngVien()
        {
            foreach (TuyenDung_ThiSinh item in ListUngVien)
            {
                if (item.Chon != ChonTatCa)
                    item.Chon = ChonTatCa;
            }
        }
    }

}
