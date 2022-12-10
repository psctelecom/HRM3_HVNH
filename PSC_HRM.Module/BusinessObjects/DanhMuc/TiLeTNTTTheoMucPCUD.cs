using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Tỉ lệ TNTT theo mức PCUD")]
    public class TiLeTNTTTheoMucPCUD : BaseObject
    {
        private string _TenUuDai;
        private decimal _PhuCapUuDai;
        private decimal _TiLeTangThem;

        [ModelDefault("Caption", "Tên ưu đãi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenUuDai
        {
            get
            {
                return _TenUuDai;
            }
            set
            {
                SetPropertyValue("TenUuDai", ref _TenUuDai, value);
            }
        }


        [ModelDefault("Caption", "Phụ cấp ưu đãi")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapUuDai
        {
            get
            {
                return _PhuCapUuDai;
            }
            set
            {
                SetPropertyValue("PhuCapUuDai", ref _PhuCapUuDai, value);
            }
        }

        [ModelDefault("Caption", "Tỉ lệ tăng thêm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TiLeTangThem
        {
            get
            {
                return _TiLeTangThem;
            }
            set
            {
                SetPropertyValue("TiLeTangThem", ref _TiLeTangThem, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Tỉ lệ")]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return String.Format("{0:n0}", TiLeTangThem);
            }
        }

        public TiLeTNTTTheoMucPCUD(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
