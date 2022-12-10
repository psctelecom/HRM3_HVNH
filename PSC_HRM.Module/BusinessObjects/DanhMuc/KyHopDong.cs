using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("TenKyHopDong")]
    [ModelDefault("Caption", "Kỳ hợp đồng")]
    [ImageName("BO_Position")]
    public class KyHopDong : BaseObject
    {
     
        private string _MaQuanLy;
        private string _TenKyHopDong;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

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
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (value != DateTime.MinValue)
                {
                    GetTenKyHopDong();
                }
            }
        }
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (value != DateTime.MinValue)
                {
                    GetTenKyHopDong();
                }
            }
        }
        [ModelDefault("Caption", "Tên kỳ hợp đồng")]
        [ModelDefault("AllowEdit", "False")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenKyHopDong
        {
            get
            {
                return _TenKyHopDong;
            }
            set
            {
                SetPropertyValue("TenKyHopDong", ref _TenKyHopDong, value);
            }
        }
        public KyHopDong(Session session) : base(session) { }

        private void GetTenKyHopDong()
        {
            TenKyHopDong = string.Empty;
            //
            if (TuNgay.Year == HamDungChung.GetServerTime().Year && DenNgay.Year == HamDungChung.GetServerTime().Year)
            {
                if (TuNgay.Month == 1 && DenNgay.Month == 6)
                {
                    TenKyHopDong = string.Format("6 tháng đầu năm {0} ", HamDungChung.GetServerTime().Year);
                }
                if (TuNgay.Month ==7 && DenNgay.Month == 12)
                {
                    TenKyHopDong = string.Format("6 tháng cuối năm {0} ", HamDungChung.GetServerTime().Year);
                }
            }        
        }
    }

}
