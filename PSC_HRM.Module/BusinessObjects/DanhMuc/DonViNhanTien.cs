using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenDonViNhanTien")]
    [ModelDefault("Caption", "Đơn vị nhận tiền")]
    [RuleCombinationOfPropertiesIsUnique("DonViNhanTien.Unique", DefaultContexts.Save, "LoaiThanhToan;NgungSuDung")]
    public class DonViNhanTien : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDonViNhanTien;
        private string _MaDVQHNS;
        private string _DiaChi;
        private string _SoTaiKhoan;
        private NganHang _NganHang;
        private LoaiThanhToanEnum _LoaiThanhToan;
        private bool _NgungSuDung;
        
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

        [ModelDefault("Caption", "Tên đơn vị nhận tiền")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDonViNhanTien
        {
            get
            {
                return _TenDonViNhanTien;
            }
            set
            {
                SetPropertyValue("TenDonViNhanTien", ref _TenDonViNhanTien, value);
            }
        }

        [ModelDefault("Caption", "Mã ĐVQHNS")]
        public string MaDVQHNS
        {
            get
            {
                return _MaDVQHNS;
            }
            set
            {
                SetPropertyValue("MaDVQHNS", ref _MaDVQHNS, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        public string DiaChi
        {
            get
            {
                return _DiaChi;
            }
            set
            {
                SetPropertyValue("DiaChi", ref _DiaChi, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        public string SoTaiKhoan
        {
            get
            {
                return _SoTaiKhoan;
            }
            set
            {
                SetPropertyValue("SoTaiKhoan", ref _SoTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Ngân hàng")]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
            }
        }

        [ModelDefault("Caption", "Loại thanh toán")]
        public LoaiThanhToanEnum LoaiThanhToan
        {
            get
            {
                return _LoaiThanhToan;
            }
            set
            {
                SetPropertyValue("LoaiThanhToan", ref _LoaiThanhToan, value);
            }
        }

        [ModelDefault("Caption", "Ngừng sử dụng")]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }

        public DonViNhanTien(Session session) : base(session) { }
    }

}
