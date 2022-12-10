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
    public class HoSo_CapNhatHeSoIUH : TruongBaseObject
    {
        private LoaiHeSoEnumIUH _LoaiHeSo = LoaiHeSoEnumIUH.PhuCapUuDai;
        //

        [ModelDefault("Caption", "Loại hệ số")]
        public LoaiHeSoEnumIUH LoaiHeSo
        {
            get
            {
                return _LoaiHeSo;
            }
            set
            {
                SetPropertyValue("LoaiHeSo", ref _LoaiHeSo, value);
            }
        }


        public HoSo_CapNhatHeSoIUH(Session session)
            : base(session)
        { }
    }

    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn loại hệ số")]
    public class HoSo_CapNhatHeSoUTE : TruongBaseObject
    {
        private LoaiHeSoEnumUTE _LoaiHeSo = LoaiHeSoEnumUTE.PhuCapUuDai;
        //

        [ModelDefault("Caption", "Loại hệ số")]
        public LoaiHeSoEnumUTE LoaiHeSo
        {
            get
            {
                return _LoaiHeSo;
            }
            set
            {
                SetPropertyValue("LoaiHeSo", ref _LoaiHeSo, value);
            }
        }


        public HoSo_CapNhatHeSoUTE(Session session)
            : base(session)
        { }
    }


    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn loại hệ số")]
    public class HoSo_CapNhatHeSoDLU : TruongBaseObject
    {
        private LoaiHeSoEnumDLU _LoaiHeSo = LoaiHeSoEnumDLU.HSPCDocHai;
        //

        [ModelDefault("Caption", "Loại hệ số")]
        public LoaiHeSoEnumDLU LoaiHeSo
        {
            get
            {
                return _LoaiHeSo;
            }
            set
            {
                SetPropertyValue("LoaiHeSo", ref _LoaiHeSo, value);
            }
        }


        public HoSo_CapNhatHeSoDLU(Session session)
            : base(session)
        { }
    }
}
