using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_HoaDon")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowDelete", "False")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Chi tiết 05B/BK-TNCN")]
    public class ChiTietBangKeThueTNCNNgoai : BaseObject
    {
        //Fields...
        private decimal _TongThuNhapChiuThue;
        private decimal _TongTNCTLamCanCuGiamTru;
        private decimal _TongThueTNCNTamTru;
        private KyTinhLuong _KyTinhLuong;
        private BangKeThueTNCNNgoai _BangKeThueTNCNNgoai;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng kê 05B/BK-TNCN")]
        [Association("BangKeThueTNCNNgoai-ListChiTietBangKeThueTNCNNgoai")]
        public BangKeThueTNCNNgoai BangKeThueTNCNNgoai
        {
            get
            {
                return _BangKeThueTNCNNgoai;
            }
            set
            {
                SetPropertyValue("BangKeThueTNCNNgoai", ref _BangKeThueTNCNNgoai, value);
            }
        }

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng thu nhập chịu thuế")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThuNhapChiuThue
        {
            get
            {
                return _TongThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("TongThuNhapChiuThue", ref _TongThuNhapChiuThue, value);
            }
        }

        //thu nhập từ tiền lương, tiền công ở khu kinh tế
        //mình không dùng
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng TNCT làm căn cứ giảm trừ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongTNCTLamCanCuGiamTru
        {
            get
            {
                return _TongTNCTLamCanCuGiamTru;
            }
            set
            {
                SetPropertyValue("TongTNCTLamCanCuGiamTru", ref _TongTNCTLamCanCuGiamTru, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng thuế TNCN đã tạm trừ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThueTNCNTamTru
        {
            get
            {
                return _TongThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("TongThueTNCNTamTru", ref _TongThueTNCNTamTru, value);
            }
        }

        public ChiTietBangKeThueTNCNNgoai(Session session) : base(session) { }
    }

}
