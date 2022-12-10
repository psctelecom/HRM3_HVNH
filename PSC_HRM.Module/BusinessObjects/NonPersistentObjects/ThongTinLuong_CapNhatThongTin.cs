using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn loại hệ số")]
    public class ThongTinLuong_CapNhatThongTin : TruongBaseObject
    {
        private LoaiThongTinLuongEnum _LoaiThongTinLuong = LoaiThongTinLuongEnum.SoThangThanhToan;
        //

        [ModelDefault("Caption", "Loại thông tin lương")]
        public LoaiThongTinLuongEnum LoaiThongTinLuong
        {
            get
            {
                return _LoaiThongTinLuong;
            }
            set
            {
                SetPropertyValue("LoaiThongTinLuong", ref _LoaiThongTinLuong, value);
            }
        }


        public ThongTinLuong_CapNhatThongTin(Session session)
            : base(session)
        { }
    }

}
