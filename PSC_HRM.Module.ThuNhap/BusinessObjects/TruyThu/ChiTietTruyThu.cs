using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.TruyThu
{
    [ModelDefault("Caption", "Chi tiết truy thu")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietTruyThu.Unique", DefaultContexts.Save, "TruyThuNhanVien;MaChiTiet")]
    public class ChiTietTruyThu : ThuNhapBaseObject
    {
        private string _MaChiTiet;
        private string _DienGiai;
        private TruyThuNhanVien _TruyThuNhanVien;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private CongTruEnum _CongTru;

        [Browsable(false)]
        [ModelDefault("Caption", "Truy thu nhân viên")]
        [Association("TruyThuNhanVien-ListChiTietTruyThu")]
        public TruyThuNhanVien TruyThuNhanVien
        {
            get
            {
                return _TruyThuNhanVien;
            }
            set
            {
                SetPropertyValue("TruyThuNhanVien", ref _TruyThuNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mã chi tiết")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        public ChiTietTruyThu(Session session) : base(session) { }
    }

}
