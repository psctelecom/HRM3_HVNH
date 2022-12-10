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
    [DefaultProperty("TenLyLuanChinhTri")]
    [ModelDefault("Caption", "Lý luận chính trị")]
    public class LyLuanChinhTri : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenLyLuanChinhTri;

        public LyLuanChinhTri(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên Lý Luận Chính Trị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLyLuanChinhTri
        {
            get
            {
                return _TenLyLuanChinhTri;
            }
            set
            {
                SetPropertyValue("TenLyLuanChinhTri", ref _TenLyLuanChinhTri, value);
            }
        }

        private decimal _CapDo;
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }
    }

}
