using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ImageName("BO_HoaDon")]
    [ModelDefault("Caption", "Chi tiết thanh toán")]
    //[Appearance("UyNhiemChi.KhoaSo", TargetItems = "*", Enabled = false,
    //    Criteria = "KyTinhLuong is not null and KyTinhLuong.KhoaSo")]
    public class ChiTietThanhToan : BaseObject
    {
        private ThanhToan _ThanhToan;
        private string _NoiDungTT;
        private string _MaNDKT;
        private string _MaChuong;
        private string _MaNganhKT;
        private string _MaNguonNSNN;
        private decimal _SoTien;
        private decimal _NopThue;
        private decimal _TTChoDVHuong;

        [ModelDefault("Caption", "Nội dung thanh toán")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string NoiDungTT
        {
            get
            {
                return _NoiDungTT;
            }
            set
            {
                SetPropertyValue("NoiDungTT", ref _NoiDungTT, value);
            }
        }

        [ModelDefault("Caption", "Mã NDKT")]
        public string MaNDKT
        {
            get
            {
                return _MaNDKT;
            }
            set
            {
                SetPropertyValue("MaNDKT", ref _MaNDKT, value);
            }
        }

        [ModelDefault("Caption", "Mã chương")]
        public string MaChuong
        {
            get
            {
                return _MaChuong;
            }
            set
            {
                SetPropertyValue("MaChuong", ref _MaChuong, value);
            }
        }

        [ModelDefault("Caption", "Mã ngành")]
        public string MaNganhKT
        {
            get
            {
                return _MaNganhKT;
            }
            set
            {
                SetPropertyValue("MaNganhKT", ref _MaNganhKT, value);
            }
        }

        [ModelDefault("Caption", "Mã nguồn NSNN")]
        public string MaNguonNSNN
        {
            get
            {
                return _MaNguonNSNN;
            }
            set
            {
                SetPropertyValue("MaNguonNSNN", ref _MaNguonNSNN, value);
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

        [ModelDefault("Caption", "Nộp thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal NopThue
        {
            get
            {
                return _NopThue;
            }
            set
            {
                SetPropertyValue("NopThue", ref _NopThue, value);
            }
        }

        [ModelDefault("Caption", "TT cho ĐV hưởng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TTChoDVHuong
        {
            get
            {
                return _TTChoDVHuong;
            }
            set
            {
                SetPropertyValue("TTChoDVHuong", ref _TTChoDVHuong, value);
            }
        }

        [Browsable(false)]
        [Association("ThanhToan-ChiTietThanhToan")]
        public ThanhToan ThanhToan
        {
            get
            {
                return _ThanhToan;
            }
            set
            {
                SetPropertyValue("ThanhToan", ref _ThanhToan, value);
            }
        }

        public ChiTietThanhToan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            XPCollection<ThanhToan> thanhToanList = new XPCollection<ThanhToan>(Session);
            thanhToanList.Criteria = CriteriaOperator.Parse("ChungTu.KyTinhLuong = ? and MauThanhToan = 0", ThanhToan.ChungTu.KyTinhLuong.Oid);
            ThanhToan.NganSachConLai = ThanhToan.ChungTu.KyTinhLuong.ThongTinChung.NganSachNhaNuoc - thanhToanList.Sum(a => a.SoTien);
        }
    }

}
