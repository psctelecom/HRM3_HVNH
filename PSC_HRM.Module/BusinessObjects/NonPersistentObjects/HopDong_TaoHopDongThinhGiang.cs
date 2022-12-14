using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Tạo hợp đồng")]
    public class HopDong_TaoHopDongThinhGiang : BaseObject
    {
        // Fields...
        private TaoHopDongThinhGiangEnum _LoaiHopDong = TaoHopDongThinhGiangEnum.HopDongThinhGiang;

        [ModelDefault("Caption", "Loại hợp đồng")]
        public TaoHopDongThinhGiangEnum LoaiHopDong
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

        public HopDong_TaoHopDongThinhGiang(Session session) : base(session) { }
    }

}
