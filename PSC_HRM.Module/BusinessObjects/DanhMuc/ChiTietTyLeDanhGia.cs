using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Chi tiết tỷ lệ đánh giá")]
    public class ChiTietTyLeDanhGia : BaseObject
    {
        private string _DienGiai;
        private decimal _TuGiaTri;
        private decimal _DenGiaTri;
        private int _CapDo;
        private TyLeDanhGia _TyLeDanhGia;

        [ModelDefault("Caption", "Tỷ lệ đánh giá")]
        [Association("TyLeDanhGia-ListChiTietTyLeDanhGia")]
        public TyLeDanhGia TyLeDanhGia
        {
            get
            {
                return _TyLeDanhGia;
            }
            set
            {
                SetPropertyValue("TyLeDanhGia", ref _TyLeDanhGia, value);
                if (!IsLoading && value != null)
                {
                    int temp = 1;
                    foreach (ChiTietTyLeDanhGia item in value.ListChiTietTyLeDanhGia)
                    {
                        temp++;
                    }
                    CapDo = temp;
                }
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Từ giá trị")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TuGiaTri
        {
            get
            {
                return _TuGiaTri;
            }
            set
            {
                SetPropertyValue("TuGiaTri", ref _TuGiaTri, value);
            }
        }

        [ModelDefault("Caption", "Đến giá trị")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal DenGiaTri
        {
            get
            {
                return _DenGiaTri;
            }
            set
            {
                SetPropertyValue("DenGiaTri", ref _DenGiaTri, value);
            }
        }

        [ModelDefault("Caption", "Cấp độ")]
        public int CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }

        public ChiTietTyLeDanhGia(Session session) : base(session) { }


        protected override void OnSaving()
        {
            base.OnSaving();
            TyLeDanhGia.CapDoCaoNhat = CapDo;
        }
    }
}
