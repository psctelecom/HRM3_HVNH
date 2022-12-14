using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.Luong
{
    [DefaultProperty("DienGiai")]
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi tiết lương cán bộ")]
    [Appearance("ChiTietLuongNhanVien.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "LuongNhanVien is not null and LuongNhanVien.BangLuongNhanVien is not null and LuongNhanVien.BangLuongNhanVien.KyTinhLuong is not null and LuongNhanVien.BangLuongNhanVien.KyTinhLuong.KhoaSo")]
    public class ChiTietLuongNhanVien : ThuNhapBaseObject
    {
        private LuongNhanVien _LuongNhanVien;
        private string _DienGiai;
        private string _MaChiTiet;
        private decimal _TienLuong;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private CongTruEnum _CongTru;
        private string _GhiChu;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;

        public ChiTietLuongNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Lương nhân viên")]
        [Association("LuongNhanVien-ListChiTietLuongNhanVien")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LuongNhanVien LuongNhanVien
        {
            get
            {
                return _LuongNhanVien;
            }
            set
            {
                SetPropertyValue("LuongNhanVien", ref _LuongNhanVien, value);
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

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
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

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [Size(500)]
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
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
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

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && (Session is NestedUnitOfWork))
            {
                //Khi lưu ChiTietLuongNhanVien, Tổng tiền bang LuongNhanVien thay đổi
                LuongNhanVien.XuLy();
            }

        }
    }

}
