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
    //[RuleCombinationOfPropertiesIsUnique("ChiTietTruyLuongNew.Unique", DefaultContexts.Save, "TruyLuongNhanVien;MaChiTiet")]
    public class ChiTietTruyLuongNew : ThuNhapBaseObject
    {
        private CongTruEnum _CongTru;
        private string _MaChiTiet;
        private string _DienGiai;
        private TruyLuongNhanVienNew _TruyLuongNhanVien;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhTNCT;
        private decimal _MucLuongCu;
        private decimal _MucLuongMoi;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Truy lĩnh lương nhân viên")]
        [Association("TruyLuongNhanVienNew-ListChiTietTruyLuongNew")]
        public TruyLuongNhanVienNew TruyLuongNhanVien
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
                if (!IsLoading && MucLuongCu != null && MucLuongMoi != null)
                { SoTien = MucLuongMoi - MucLuongCu; }
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
                if (!IsLoading && MucLuongCu != null && MucLuongMoi != null)
                { SoTien = MucLuongMoi - MucLuongCu; }
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

        [Size(-1)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietTruyLuongNew(Session session) : base(session) { }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                TruyLuongNhanVien.SoTien = TruyLuongNhanVien.SoTien - SoTien;
            }
 	         base.OnDeleting();
        }

        
    }

}
