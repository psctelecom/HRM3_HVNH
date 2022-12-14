using System;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenLyDoNghi")]
    [ModelDefault("Caption", "Lý do nghỉ")]
    public class LyDoNghi : BaseObject
    {
        private LyDoNghi1Enum _PhanLoai;
        private string _MaQuanLy;
        private string _TenLyDoNghi;

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

        [ModelDefault("Caption", "Tên lý do nghỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLyDoNghi
        {
            get
            {
                return _TenLyDoNghi;
            }
            set
            {
                SetPropertyValue("TenLyDoNghi", ref _TenLyDoNghi, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public LyDoNghi1Enum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        public LyDoNghi(Session session) : base(session) { }
    }

}
