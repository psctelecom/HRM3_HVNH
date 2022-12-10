using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.MailMerge
{
    [DefaultProperty("TenTaiLieu")]
    [ModelDefault("Caption", "Mẫu mail merge")]
    public class MailMergeTemplate : BaseObject
    {
        // Fields...
        private bool _SuDungMacDinh;
        private byte[] _LuuTru;
        private string _TenTaiLieu;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
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

        [ModelDefault("Caption", "Tên tài liệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTaiLieu
        {
            get
            {
                return _TenTaiLieu;
            }
            set
            {
                SetPropertyValue("TenTaiLieu", ref _TenTaiLieu, value);
            }
        }

        [ModelDefault("Caption", "Sử dụng mặc định")]
        public bool SuDungMacDinh
        {
            get
            {
                return _SuDungMacDinh;
            }
            set
            {
                SetPropertyValue("SuDungMacDinh", ref _SuDungMacDinh, value);
            }
        }


        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        public byte[] LuuTru
        {
            get
            {
                return _LuuTru;
            }
            set
            {
                SetPropertyValue("LuuTru", ref _LuuTru, value);
            }
        }

        public MailMergeTemplate(Session session) : base(session) { }
    }

}
