using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_HinhThucHopDong")]
    [DefaultProperty("TenHinhThucHopDong")]
    [ModelDefault("Caption", "Hình thức hợp đồng")]
    [Appearance("HinhThucHopDong", TargetItems = "SoThang;SoNgay", Visibility = ViewItemVisibility.Hide, Criteria = "!CoThoiHan")]
    public class HinhThucHopDong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHinhThucHopDong;
        private bool _CoThoiHan;
        private int _SoThang;
        private int _SoNgay;

        public HinhThucHopDong(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên hình thức hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucHopDong
        {
            get
            {
                return _TenHinhThucHopDong;
            }
            set
            {
                SetPropertyValue("TenHinhThucHopDong", ref _TenHinhThucHopDong, value);
            }
        }

        [ModelDefault("Caption", "Có thời hạn")]
        [ImmediatePostData()]
        public bool CoThoiHan
        {
            get
            {
                return _CoThoiHan;
            }
            set
            {
                SetPropertyValue("CoThoiHan", ref _CoThoiHan, value);
                if (!IsLoading)
                {
                    if (value == true)
                        SoThang = 36;
                    else
                        SoThang = 0;
                }
            }
        }

        [ModelDefault("Caption", "Thời hạn hợp đồng (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        [ModelDefault("Caption", "Số ngày lẻ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            SoThang = 0;
            SoNgay = 0;
        }
    }

}
