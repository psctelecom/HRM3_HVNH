using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChucVuDoan")]
    [ModelDefault("Caption", "Chức vụ Đoàn")]
    public class ChucVuDoan : BaseObject
    {
        private decimal _HSPCChucVuDoan;
        private decimal _PhuCapChucVu;
        private string _MaQuanLy;
        private string _TenChucVuDoan;
        private decimal _PhuCapDienThoai;
        private bool _LaQuanLy;

        [ModelDefault("Caption", "Mã Quản Lý")]
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

        [ModelDefault("Caption", "Tên Chức Vụ Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucVuDoan
        {
            get
            {
                return _TenChucVuDoan;
            }
            set
            {
                SetPropertyValue("TenChucVuDoan", ref _TenChucVuDoan, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ")]
        public decimal HSPCChucVuDoan
        {
            get
            {
                return _HSPCChucVuDoan;
            }
            set
            {
                SetPropertyValue("HSPCChucVuDoan", ref _HSPCChucVuDoan, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        public decimal PhuCapChucVu
        {
            get
            {
                return _PhuCapChucVu;
            }
            set
            {
                SetPropertyValue("PhuCapChucVu", ref _PhuCapChucVu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return _PhuCapDienThoai;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoai", ref _PhuCapDienThoai, value);
            }
        }

        [ModelDefault("Caption", "Là quản lý")]
        public bool LaQuanLy
        {
            get
            {
                return _LaQuanLy;
            }
            set
            {
                SetPropertyValue("LaQuanLy", ref _LaQuanLy, value);
            }
        }
        public ChucVuDoan(Session session) : base(session) { }
    }

}
