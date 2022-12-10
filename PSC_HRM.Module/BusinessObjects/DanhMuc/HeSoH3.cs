using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Hệ số H3")]
    public class HeSoH3 : TruongBaseObject
    {
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private NgachLuong _NgachLuong;
        //private LoaiNhanSu _LoaiNhanSu;
        private decimal _HeSo;

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        //[ModelDefault("Caption", "Loại nhân sự")]
        //public LoaiNhanSu LoaiNhanSu
        //{
        //    get
        //    {
        //        return _LoaiNhanSu;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
        //    }
        //}

        [ModelDefault("Caption", "Hệ số H3")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get
            {
                return _HeSo;
            }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Hệ số")]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return String.Format("{0:n2}", HeSo);
            }
        }

        public HeSoH3(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
