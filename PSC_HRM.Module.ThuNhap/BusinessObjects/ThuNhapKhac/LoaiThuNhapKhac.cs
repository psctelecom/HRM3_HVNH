using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.ThuNhapKhac
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Loại thu nhập khác")]
    [DefaultProperty("TenLoaiThuNhapKhac")]
    public class LoaiThuNhapKhac : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiThuNhapKhac;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên loại thu nhập khác")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenLoaiThuNhapKhac
        {
            get
            {
                return _TenLoaiThuNhapKhac;
            }
            set
            {
                SetPropertyValue("TenLoaiThuNhapKhac", ref _TenLoaiThuNhapKhac, value);
            }
        }

        public LoaiThuNhapKhac(Session session) : base(session) { }
    }

}
