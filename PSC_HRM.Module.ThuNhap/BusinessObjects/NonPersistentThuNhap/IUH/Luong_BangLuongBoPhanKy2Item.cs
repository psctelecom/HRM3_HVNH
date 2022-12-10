using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;


namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng lương theo bộ phận kỳ 2")]
    public class Luong_BangLuongBoPhanKy2Item : BaseObject, ISupportController
    {
        private string _BoPhan;
        private int _SoNguoi;
        private decimal _SoTien;
        private decimal _TruNDC;
        private decimal _TruCongDoan;
        private decimal _TruTamUng;
        private decimal _TruKhac;
        private decimal _ThuNhapChiuThue;
        private decimal _ThueThuNhap;
        private decimal _TruUngHo;
        private decimal _ThucLinh;

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Số người")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoNguoi
        {
            get
            {
                return _SoNguoi;
            }
            set
            {
                SetPropertyValue("SoNguoi", ref _SoNguoi, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Trừ BT + NĐC")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruNDC
        {
            get
            {
                return _TruNDC;
            }
            set
            {
                SetPropertyValue("TruNDC", ref _TruNDC, value);
            }
        }

        [ModelDefault("Caption", "Trừ công đoàn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruCongDoan
        {
            get
            {
                return _TruCongDoan;
            }
            set
            {
                SetPropertyValue("TruCongDoan", ref _TruCongDoan, value);
            }
        }

        [ModelDefault("Caption", "Trừ tạm ứng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruTamUng
        {
            get
            {
                return _TruTamUng;
            }
            set
            {
                SetPropertyValue("TruTamUng", ref _TruTamUng, value);
            }
        }

        [ModelDefault("Caption", "Trừ khác")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruKhac
        {
            get
            {
                return _TruKhac;
            }
            set
            {
                SetPropertyValue("TruKhac", ref _TruKhac, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuNhapChiuThue
        {
            get
            {
                return _ThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế thu nhập")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueThuNhap
        {
            get
            {
                return _ThueThuNhap;
            }
            set
            {
                SetPropertyValue("ThueThuNhap", ref _ThueThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Trừ ủng hộ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruUngHo
        {
            get
            {
                return _TruUngHo;
            }
            set
            {
                SetPropertyValue("TruUngHo", ref _TruUngHo, value);
            }
        }

        [ModelDefault("Caption", "Thực lĩnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucLinh
        {
            get
            {
                return _ThucLinh;
            }
            set
            {
                SetPropertyValue("ThucLinh", ref _ThucLinh, value);
            }
        }

        public Luong_BangLuongBoPhanKy2Item(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
