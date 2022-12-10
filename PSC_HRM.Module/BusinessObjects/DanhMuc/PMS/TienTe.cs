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
    [DefaultProperty("TenTienTe")]
    [ModelDefault("Caption", "Tiền tệ")]
    public class TienTe : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTienTe;
        private decimal _TiLeHoiDoi;
        private string _KyHieu;
        
        [ModelDefault("Caption", "Mã quản lý")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên tiền tệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTienTe
        {
            get
            {
                return _TenTienTe;
            }
            set
            {
                SetPropertyValue("TenTienTe", ref _TenTienTe, value);
            }
        }

        [ModelDefault("Caption", "Tỉ lệ(VNĐ)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TiLeHoiDoi
        {
            get
            {
                return _TiLeHoiDoi;
            }
            set
            {
                SetPropertyValue("TiLeHoiDoi", ref _TiLeHoiDoi, value);
            }
        }

        [ModelDefault("Caption", "Ký hiệu")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string KyHieu
        {
            get
            {
                return _KyHieu;
            }
            set
            {
                SetPropertyValue("KyHieu", ref _KyHieu, value);
            }
        }

        public TienTe(Session session) : base(session) { }
    }

}
