using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption", "Hệ số khác(UEL)")]
    [DefaultProperty("DanhMucHeSoKhac")]
    public class HeSoKhac_UEL : BaseObject
    {
        private DanhMucHeSoKhac _DanhMucHeSoKhac;
        private decimal _HeSo;
        private QuanLyHeSo _QuanLyHeSo;


        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoKhac")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }


        [ModelDefault("Caption", "Danh mục hệ số khác")]
        public DanhMucHeSoKhac DanhMucHeSoKhac
        {
            get { return _DanhMucHeSoKhac; }
            set
            {
                SetPropertyValue("DanhMucHeSoKhac", ref _DanhMucHeSoKhac, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Hệ số")]
        public  decimal HeSo
        {
            get { return _HeSo; }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }
        public HeSoKhac_UEL(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
