using System;

using DevExpress.Xpo;
using System.ComponentModel;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DinhBien
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Định biên")]
    [DefaultProperty("Ten")]
    [ImageName("BO_QuanLyKhenThuong_32x32")]
    [NonPersistent]
    public class DinhBienCongViec : BaseObject
    {

        public DinhBienCongViec(Session session) : 
            base(session) 
        { }
    }

}
