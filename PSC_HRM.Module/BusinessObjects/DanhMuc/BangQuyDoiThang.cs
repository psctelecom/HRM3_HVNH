using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("LoaiNhanSu")]
    [ModelDefault("Caption", "Bảng quy đổi theo tháng")]
    public class BangQuyDoiThang : BaseObject
    {
        // Fields...
        private int _DenSoLan;
        private LoaiNhanSu _LoaiNhanSu;
        private XepLoaiDanhGiaEnum _XepLoai;
        private int _TuSoLan;
        private HinhThucViPham _HinhThucViPham;

        [ModelDefault("Caption", "Loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Hình thức vi phạm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HinhThucViPham HinhThucViPham
        {
            get
            {
                return _HinhThucViPham;
            }
            set
            {
                SetPropertyValue("HinhThucViPham", ref _HinhThucViPham, value);
            }
        }

        [ModelDefault("Caption", "Từ số lần/số tiết")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public int TuSoLan
        {
            get
            {
                return _TuSoLan;
            }
            set
            {
                SetPropertyValue("TuSoLan", ref _TuSoLan, value);
            }
        }

        [ModelDefault("Caption", "Đến số lần/số tiết")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public int DenSoLan
        {
            get
            {
                return _DenSoLan;
            }
            set
            {
                SetPropertyValue("DenSoLan", ref _DenSoLan, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiDanhGiaEnum XepLoai
        {
            get
            {
                return _XepLoai;
            }
            set
            {
                SetPropertyValue("XepLoai", ref _XepLoai, value);
            }
        }

        public BangQuyDoiThang(Session session) : base(session) { }
    }

}
