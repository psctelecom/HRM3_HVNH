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
    [ModelDefault("Caption", "Chi tiết 05A/BK-TNCN")]
    public class ChiTietBangKeThueTNCNNhanVien : BaseObject
    {
        // Fields...
        private decimal _TongThuNhapChiuThue;
        private decimal _TongThueTNCNTamTru;
        private decimal _TongGiamTruGiaCanh;
        private decimal _TongTuThienNhanDao;
        private decimal _TongBHXH;
        private decimal _TongBHYT;
        private decimal _TongBHTN;
        private decimal _TongThuNhapTinhThue;
        private decimal _TongTNCTLamCanCuGiamTru;
        private int _SoNguoiPhuThuoc;
        private KyTinhLuong _KyTinhLuong;
        private BangKeThueTNCNNhanVien _BangKeThueTNCNNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng kê 05A/BK-TNCN")]
        [Association("BangKeThueTNCNNhanVien-ListChiTietBangKeThueTNCNNhanVien")]
        public BangKeThueTNCNNhanVien BangKeThueTNCNNhanVien
        {
            get
            {
                return _BangKeThueTNCNNhanVien;
            }
            set
            {
                SetPropertyValue("BangKeThueTNCNNhanVien", ref _BangKeThueTNCNNhanVien, value);
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
        [ModelDefault("Caption", "Tổng TNCT")]
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

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng giảm trừ gia cảnh")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongGiamTruGiaCanh
        {
            get
            {
                return _TongGiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("TongGiamTruGiaCanh", ref _TongGiamTruGiaCanh, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng từ thiện nhân đạo")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongTuThienNhanDao
        {
            get
            {
                return _TongTuThienNhanDao;
            }
            set
            {
                SetPropertyValue("TongTuThienNhanDao", ref _TongTuThienNhanDao, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng BHXH")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongBHXH
        {
            get
            {
                return _TongBHXH;
            }
            set
            {
                SetPropertyValue("TongBHXH", ref _TongBHXH, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng BHYT")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongBHYT
        {
            get
            {
                return _TongBHYT;
            }
            set
            {
                SetPropertyValue("TongBHYT", ref _TongBHYT, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng BHTN")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongBHTN
        {
            get
            {
                return _TongBHTN;
            }
            set
            {
                SetPropertyValue("TongBHTN", ref _TongBHTN, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng thu nhập tính thuế")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongThuNhapTinhThue
        {
            get
            {
                return _TongThuNhapTinhThue;
            }
            set
            {
                SetPropertyValue("TongThuNhapTinhThue", ref _TongThuNhapTinhThue, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tổng thuế TNCN đã tạm thu")]
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

        //thu nhập là tiền công, tiền lương từ khu kinh tế
        //mình không sử dụng
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

        [ModelDefault("Caption", "Số người phụ thuộc")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int SoNguoiPhuThuoc
        {
            get
            {
                return _SoNguoiPhuThuoc;
            }
            set
            {
                SetPropertyValue("SoNguoiPhuThuoc", ref _SoNguoiPhuThuoc, value);
            }
        }


        public ChiTietBangKeThueTNCNNhanVien(Session session) : base(session) { }
    }

}
