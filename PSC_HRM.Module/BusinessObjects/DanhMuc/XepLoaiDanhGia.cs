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
    [DefaultProperty ("TenXepLoai")]
    [ModelDefault("Caption", "Xếp loại đánh giá")]
    public class XepLoaiDanhGia : BaseObject
    {
        private string _MaQuanLy;
        private string _TenXepLoai;
        private decimal _TiLe;


        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("",DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên xếp loại")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenXepLoai
        {
            get
            {
                return _TenXepLoai;
            }
            set
            {
                SetPropertyValue("TenXepLoai", ref _TenXepLoai, value);
            }
        }

        [ModelDefault("Caption", "Tỉ lệ (%)")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TiLe
        {
            get
            {
                return _TiLe;
            }
            set
            {
                SetPropertyValue("TiLe", ref _TiLe, value);
            }
        }


        public XepLoaiDanhGia(Session session) : base(session) { }
    }

}
