using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Tạo hợp đồng")]
    public class HopDong_TaoHopDong : BaseObject
    {
        // Fields...
        private TaoHopDongEnum _LoaiHopDong = TaoHopDongEnum.HopDongLamViec;

        [ModelDefault("Caption", "Loại hợp đồng")]
        public TaoHopDongEnum LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }

        public HopDong_TaoHopDong(Session session) : base(session) { }
    }

}
