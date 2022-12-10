using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_CreditCard")]
    [DefaultProperty("SoTaiKhoan")]
    [ModelDefault("Caption", "Tài khoản ngân hàng")]
    public class TaiKhoanNganHang : TruongBaseObject
    {
        public TaiKhoanNganHang(Session session) : base(session) { }

        private bool _TaiKhoanChinh;
        private ThongTinTruong _ThongTinTruong;
        private NhanVien _NhanVien;

        private string _SoTaiKhoan;
        private NganHang _NganHang;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("NhanVien-ListTaiKhoanNganHang")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _NhanVien, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin trường")]
        [Association("ThongTinTruong-ListTaiKhoanNganHang")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue(DefaultContexts.Save, TargetCriteria = "MaTruong == 'HBU'")]
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
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tài khoản chính")]
        public bool TaiKhoanChinh
        {
            get
            {
                return _TaiKhoanChinh;
            }
            set
            {
                SetPropertyValue("TaiKhoanChinh", ref _TaiKhoanChinh, value);
            }
        }

        protected override void OnSaving()
        {
 	        base.OnSaving();
            if (this.NhanVien != null && TruongConfig.MaTruong == "HBU")
            {
                TaiKhoanNganHang tkNganHang = Session.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("NhanVien =? && TaiKhoanChinh =?", NhanVien.Oid, true));
                if (tkNganHang == null)
                {
                    this.TaiKhoanChinh = true;
                }
            }
        }
    }
}
