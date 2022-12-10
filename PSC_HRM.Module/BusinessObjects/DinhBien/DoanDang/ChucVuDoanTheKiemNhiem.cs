using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.DoanDang
{
    [ImageName("BO_NgoaiNguKhac")]
    [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
    public class ChucVuDoanTheKiemNhiem : BaseObject
    {
        private DoanThe _DoanThe;
        private ChucVuDoanThe _ChucVuDoanThe;
        private DateTime _NgayBoNhiem;
       
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("DoanThe-ListChucVuDoanTheKiemNhiem")]
        public DoanThe DoanThe
        {
            get
            {
                return _DoanThe;
            }
            set
            {
                SetPropertyValue("DoanThe", ref _DoanThe, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get
            {
                return _ChucVuDoanThe;
            }
            set
            {
                SetPropertyValue("ChucVuDoanThe", ref _ChucVuDoanThe, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }


        public ChucVuDoanTheKiemNhiem(Session session) : base(session) { }

        
    }

}

