using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Danh sách hệ số chức danh")]
    [DefaultProperty("NhanVien")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "HeSoChucDanh_NhanVien;NhanVien", "Hệ số cho nhân viên đã tồn tại.")]
    public class DanhSachHeSoChucDanh_NhanVien : BaseObject
    {
        private HeSoChucDanh_NhanVien _HeSoChucDanh_NhanVien;
        private NhanVien _NhanVien;
        private decimal _HeSo_ChucDanh;

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_ChucDanh", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }

        [Association("HeSoChucDanh_NhanVien-ListDanhSachHeSoChucDanh_NhanVien")]
        [ModelDefault("Caption", "HeSoChucDanh_NhanVien")]
        [Browsable(false)]
        public HeSoChucDanh_NhanVien HeSoChucDanh_NhanVien
        {
            get
            {
                return _HeSoChucDanh_NhanVien;
            }
            set
            {
                SetPropertyValue("HeSoChucDanh_NhanVien", ref _HeSoChucDanh_NhanVien, value);
            }
        }
        public DanhSachHeSoChucDanh_NhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}