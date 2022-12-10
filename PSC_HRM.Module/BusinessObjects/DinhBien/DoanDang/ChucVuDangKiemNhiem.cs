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
    public class ChucVuDangKiemNhiem : BaseObject
    {
        private DangVien _DangVien;
        private ChucVuDang _ChucVuDang;
        private DateTime _NgayBoNhiem;
       
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("DangVien-ListChucVuDangKiemNhiem")]
        public DangVien DangVien
        {
            get
            {
                return _DangVien;
            }
            set
            {
                SetPropertyValue("DangVien", ref _DangVien, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVuDang ChucVuDang
        {
            get
            {
                return _ChucVuDang;
            }
            set
            {
                SetPropertyValue("ChucVuDang", ref _ChucVuDang, value);
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


        public ChucVuDangKiemNhiem(Session session) : base(session) { }

        
    }

}

