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
    public class ChucVuDoanKiemNhiem : BaseObject
    {
        private DoanVien _DoanVien;
        private ChucVuDoan _ChucVuDoan;
        private DateTime _NgayBoNhiem;
       
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("DoanVien-ListChucVuDoanKiemNhiem")]
        public DoanVien DoanVien
        {
            get
            {
                return _DoanVien;
            }
            set
            {
                SetPropertyValue("DoanVien", ref _DoanVien, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                return _ChucVuDoan;
            }
            set
            {
                SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value);
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


        public ChucVuDoanKiemNhiem(Session session) : base(session) { }

        
    }

}

