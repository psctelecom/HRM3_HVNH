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
    [DefaultProperty("TenQuyDoi")]
    [ModelDefault("Caption", "Bảng quy đổi phần trăm KPI")]
    public class BangQuyDoiPhanTramKPI : BaseObject
    {
        private string _TenQuyDoi;
        private decimal _TuGiaTri;
        private decimal _DenGiaTri;
        private XepLoaiCanBo _XepLoaiCanBo;

        [ModelDefault("Caption", "Tên quy đổi")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public string TenQuyDoi
        {
            get
            {
                return _TenQuyDoi;
            }
            set
            {
                SetPropertyValue("TenQuyDoi", ref _TenQuyDoi, value);
            }
        }

        [ModelDefault("Caption", "Từ giá trị")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TuGiaTri
        {
            get
            {
                return _TuGiaTri;
            }
            set
            {
                SetPropertyValue("TuGiaTri", ref _TuGiaTri, value);
            }
        }

        [ModelDefault("Caption", "Đến giá trị")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal DenGiaTri
        {
            get
            {
                return _DenGiaTri;
            }
            set
            {
                SetPropertyValue("DenGiaTri", ref _DenGiaTri, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại cán bộ")]
        public XepLoaiCanBo XepLoaiCanBo
        {
            get
            {
                return _XepLoaiCanBo;
            }
            set
            {
                SetPropertyValue("XepLoaiCanBo", ref _XepLoaiCanBo, value);
            }
        }

        public BangQuyDoiPhanTramKPI(Session session) : base(session) { }
    }
}
