using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Copy hoạt động khác")]
    public class CopyDanhMucHoatDongKhac : BaseObject
    {
        private NamHoc _TuNamHoc;
        private NamHoc _DenNamHoc;

        [ModelDefault("Caption", "Từ năm học")]
        [ImmediatePostData]
        public NamHoc TuNamHoc
        {
            get { return _TuNamHoc; }
            set
            {
                SetPropertyValue("TuNamHoc", ref _TuNamHoc, value);     
            }
        }

        [ModelDefault("Caption", "Đến năm học")]
        [ImmediatePostData]
        public NamHoc DenNamHoc
        {
            get { return _DenNamHoc; }
            set
            {
                SetPropertyValue("DenNamHoc", ref _DenNamHoc, value);
            }
        }
        public CopyDanhMucHoatDongKhac(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}