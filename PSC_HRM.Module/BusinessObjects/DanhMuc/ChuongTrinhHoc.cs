using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    /// <summary>
    /// sử dụng trong trình độ nhân viên
    /// </summary>
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChuongTrinhHoc")]
    [ModelDefault("Caption", "Đang theo học")]
    public class ChuongTrinhHoc : BaseObject
    {
        public ChuongTrinhHoc(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenChuongTrinhHoc;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chương trình học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChuongTrinhHoc
        {
            get
            {
                return _TenChuongTrinhHoc;
            }
            set
            {
                SetPropertyValue("TenChuongTrinhHoc", ref _TenChuongTrinhHoc, value);
            }
        }

    }

}
