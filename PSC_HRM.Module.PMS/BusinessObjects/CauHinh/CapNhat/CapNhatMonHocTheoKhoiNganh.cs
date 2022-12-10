using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.PMS.CauHinh
{
    [DefaultClassOptions]
    [DefaultProperty("TenHoatDong")]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Danh mục NCKH")]
    public class CapNhatMonHocTheoKhoiNganh : BaseObject
    {        
        private BoPhan _BoPhan;     
        
        public CapNhatMonHocTheoKhoiNganh(Session session) : base(session) { }
    }

}
