using System;
using System.Linq;
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
    [DefaultProperty("TenVongDanhGia")]
    [ModelDefault("Caption", "Vòng đánh giá")]
    public class VongDanhGia : BaseObject
    {
        private string _MaQuanLy;
        private string _TenVongDanhGia;
        private int _STT;

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

        [ModelDefault("Caption", "Tên vòng đánh giá")]
        [RuleRequiredField("",DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string TenVongDanhGia
        {
            get
            {
                return _TenVongDanhGia;
            }
            set
            {
                SetPropertyValue("TenVongDanhGia", ref _TenVongDanhGia, value);
            }
        }

        [ModelDefault("Caption", "STT")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        public VongDanhGia(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            XPCollection<VongDanhGia> vongDGList = new XPCollection<VongDanhGia>(Session);
            STT = 1;
            if(vongDGList.Count > 0)
                STT = (from d in vongDGList select d.STT).Max() + 1;

        }
    }
}
