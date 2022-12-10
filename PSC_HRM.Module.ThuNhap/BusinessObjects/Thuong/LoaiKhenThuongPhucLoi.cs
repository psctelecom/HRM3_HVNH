using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Thuong
{
    [DefaultClassOptions]
    [DefaultProperty("TenLoai")]
    [ImageName("BO_LoaiKhenThuong")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Loại thưởng")]
    public class LoaiKhenThuongPhucLoi : BaseObject
    {
        private string _TenLoai;
        private string _MaQuanLy;

        public LoaiKhenThuongPhucLoi(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên loại thưởng")]
        public string TenLoai
        {
            get
            {
                return _TenLoai;
            }
            set
            {
                SetPropertyValue("TenLoai", ref _TenLoai, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết công thức tính")]
        [Association("LoaiKhenThuongPhucLoi-CongThucKhenThuongPhucLoi")]
        public XPCollection<CongThucKhenThuongPhucLoi> CongThucList
        {
            get
            {
                return GetCollection<CongThucKhenThuongPhucLoi>("CongThucList");
            }
        }
    }
}
