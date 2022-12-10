using System;

using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Tạo danh sách thi")]
    public class TuyenDung_TaoDanhSachThi : BaseObject
    {
        // Fields...
        private HinhThucThiTuyenEnum _PhanLoai;

        [ModelDefault("Caption", "Phân loại")]
        public HinhThucThiTuyenEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        public TuyenDung_TaoDanhSachThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            PhanLoai = HinhThucThiTuyenEnum.ThiViet;
        }
    }

}
