using System;
using System.ComponentModel;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.ThuNhap.TruyLuong
{
    [ModelDefault("Caption", "Chi tiết truy lĩnh")]
    [DefaultProperty("TruyLuongNhanVien")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietTruyLuong.Unique", DefaultContexts.Save, "TruyLuongNhanVien;MaChiTiet")]
    public class ChiTietTruyLuong : ThuNhapBaseObject
    {
        private CongTruEnum _CongTru;
        private string _MaChiTiet;
        private string _DienGiai;
        private TruyLuongNhanVien _TruyLuongNhanVien;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhTNCT;
        private decimal _MucLuongCu;
        private decimal _MucLuongMoi;
        private KyTinhLuong _KyTinhLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Truy lĩnh lương nhân viên")]
        [Association("TruyLuongNhanVien-ListChiTietTruyLuong")]
        public TruyLuongNhanVien TruyLuongNhanVien
        {
            get
            {
                return _TruyLuongNhanVien;
            }
            set
            {
                SetPropertyValue("TruyLuongNhanVien", ref _TruyLuongNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mã chi tiết")]
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
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

        [ModelDefault("Caption", "Cộng/Trừ")]
        public CongTruEnum CongTru
        {
            get
            {
                return _CongTru;
            }
            set
            {
                SetPropertyValue("CongTru", ref _CongTru, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức lương cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal MucLuongCu
        {
            get
            {
                return _MucLuongCu;
            }
            set
            {
                SetPropertyValue("MucLuongCu", ref _MucLuongCu, value);
                if (!IsLoading && value != null)
                {
                    SoTien = MucLuongMoi - MucLuongCu;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức lương mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal MucLuongMoi
        {
            get
            {
                return _MucLuongMoi;
            }
            set
            {
                SetPropertyValue("MucLuongMoi", ref _MucLuongMoi, value);
                if (!IsLoading && value != null)
                {
                    SoTien = MucLuongMoi - MucLuongCu;
                }
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Kỳ tính lương")]
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

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính số tiền nhận")]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính thu nhập chịu thuế")]
        public string CongThucTinhTNCT
        {
            get
            {
                return _CongThucTinhTNCT;
            }
            set
            {
                SetPropertyValue("CongThucTinhTNCT", ref _CongThucTinhTNCT, value);
            }
        }

        public ChiTietTruyLuong(Session session) : base(session) { }
        
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && TruyLuongNhanVien != null)
                TruyLuongNhanVien.XuLy();
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
            if (!IsSaving && TruyLuongNhanVien != null)
                TruyLuongNhanVien.XuLy();
        }
    }

}
