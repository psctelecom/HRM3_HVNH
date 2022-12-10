using System;
using System.ComponentModel;
using DevExpress.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ThuNhap.TruyThu
{
    [ModelDefault("Caption", "Chi tiết truy thu khác")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietTruyThuKhac.Unique", DefaultContexts.Save, "TruyThuKhac;MaChiTiet")]
    public class ChiTietTruyThuKhac : ThuNhapBaseObject
    {
        private TruyThuKhac _TruyThuKhac;
        private string _MaChiTiet;
        private string _DienGiai;
        private decimal _SoTienTruyThu;
        private CongTruEnum _CongTru;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Truy thu khác")]
        [Association("TruyThuKhac-ListChiTietTruyThuKhac")]
        public TruyThuKhac TruyThuKhac
        {
            get
            {
                return _TruyThuKhac;
            }
            set
            {
                SetPropertyValue("TruyThuKhac", ref _TruyThuKhac, value);
            }
        }
        
        
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Số tiền truy thu")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienTruyThu
        {
            get
            {
                return _SoTienTruyThu;
            }
            set
            {
                SetPropertyValue("SoTienTruyThu", ref _SoTienTruyThu, value);
            }
        }

        public ChiTietTruyThuKhac(Session session) : base(session) { }
    }

}
 