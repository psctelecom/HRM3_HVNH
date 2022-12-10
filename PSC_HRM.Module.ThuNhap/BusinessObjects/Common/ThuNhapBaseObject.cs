using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ThuNhap
{
    [NonPersistent]
    [DesignTimeVisible(false)]
    public abstract class ThuNhapBaseObject : BaseObject
    {
        private bool _NguoiDungNhap;

        [Browsable(false)]
        public bool NguoiDungNhap
        {
            get
            {
                return _NguoiDungNhap;
            }
            set
            {
                SetPropertyValue("NguoiDungNhap", ref _NguoiDungNhap, value);
            }
        }

        public ThuNhapBaseObject(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NguoiDungNhap = true;
        }
    }

}
