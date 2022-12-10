using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_HinhThucNghi")]
    [DefaultProperty("TenHinhThucNghi")]
    [ModelDefault("Caption", "Hình thức nghỉ")]
    public class HinhThucNghi : TruongBaseObject
    {
        private string _KyHieu;
        private string _MaQuanLy;
        private HinhThucNghiEnum _PhanLoai;
        private string _TenHinhThucNghi;
        private decimal _GiaTri;
        private decimal _SoNgayToiDa;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên hình thức nghỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucNghi
        {
            get
            {
                return _TenHinhThucNghi;
            }
            set
            {
                SetPropertyValue("TenHinhThucNghi", ref _TenHinhThucNghi, value);
            }
        }

        [ModelDefault("Caption", "Ký hiệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string KyHieu
        {
            get
            {
                return _KyHieu;
            }
            set
            {
                SetPropertyValue("KyHieu", ref _KyHieu, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public HinhThucNghiEnum PhanLoai
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

        [ModelDefault("Caption", "Giá trị")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal GiaTri
        {
            get
            {
                return _GiaTri;
            }
            set
            {
                SetPropertyValue("GiaTri", ref _GiaTri, value);
            }
        }

        [ModelDefault("Caption", "Số ngày tối đa")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgayToiDa
        {
            get
            {
                return _SoNgayToiDa;
            }
            set
            {
                SetPropertyValue("SoNgayToiDa", ref _SoNgayToiDa, value);
                
            }
        }

        public HinhThucNghi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
