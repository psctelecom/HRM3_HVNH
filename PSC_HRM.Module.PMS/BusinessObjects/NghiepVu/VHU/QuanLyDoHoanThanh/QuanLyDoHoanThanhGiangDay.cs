using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang
{
    [ModelDefault("Caption", "Quản lý độ hoàng thành giảng dạy")]
    public class QuanLyDoHoanThanhGiangDay : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private bool _Khoa;

        [ModelDefault("Caption", "Năm học")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
            
        }

        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [Aggregated]
        [Association("QuanLyDoHoanThanhGiangDay-ListChiTietDoHoanThanhGiangDay")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietDoHoanThanhGiangDay> ListChiTietDoHoanThanhGiangDay
        {
            get
            {
                return GetCollection<ChiTietDoHoanThanhGiangDay>("ListChiTietDoHoanThanhGiangDay");
            }
        }
      
        public QuanLyDoHoanThanhGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
       
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }
}