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
    [ModelDefault("Caption", "Chọn thông tin cập nhật")]
    public class HoSo_CapNhatThongTinHoSoUTE : TruongBaseObject
    {
        private LoaiThongTinHoSoUTE _LoaiThongTinHoSo = LoaiThongTinHoSoUTE.ThongTinSucKhoe;
        //

        [ModelDefault("Caption", "Loại hệ số")]
        public LoaiThongTinHoSoUTE LoaiThongTinHoSo
        {
            get
            {
                return _LoaiThongTinHoSo;
            }
            set
            {
                SetPropertyValue("LoaiThongTinHoSo", ref _LoaiThongTinHoSo, value);
            }
        }


        public HoSo_CapNhatThongTinHoSoUTE(Session session)
            : base(session)
        { }
    }

    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn thông tin cập nhật")]
    public class HoSo_CapNhatThongTinHoSoDLU : TruongBaseObject
    {
        private LoaiThongTinHoSoDLU _LoaiThongTinHoSo = LoaiThongTinHoSoDLU.CongViecHienNay;
        //

        [ModelDefault("Caption", "Loại hệ số")]
        public LoaiThongTinHoSoDLU LoaiThongTinHoSo
        {
            get
            {
                return _LoaiThongTinHoSo;
            }
            set
            {
                SetPropertyValue("LoaiThongTinHoSo", ref _LoaiThongTinHoSo, value);
            }
        }


        public HoSo_CapNhatThongTinHoSoDLU(Session session)
            : base(session)
        { }
    }

   
}
