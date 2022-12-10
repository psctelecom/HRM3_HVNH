using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
namespace PSC_HRM.Module.PMS.NghiepVu.NEU.QuanLyXacNhan
{
    [ModelDefault("Caption","Quản lý xác nhận thanh toán")]
    public class QuanLyXacNhanThanhToan : BaseObject
    {

        private ThongTinTruong _ThongTinTruong;
        [ModelDefault("Caption", "Trường")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { _ThongTinTruong = value; }
        }

        private NamHoc _NamHoc;
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { _NamHoc = value; }
        }
        private HocKy _HocKy;
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { _HocKy = value; }
        }

        [Aggregated]
        [Association("QuanLyXacNhanThanhToan-ListChiTiet")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiThietXacNhanThanhToan> ListChiTiet
        {
            get
            {
                return GetCollection<ChiThietXacNhanThanhToan>("ListChiTiet");
            }
        }

        public QuanLyXacNhanThanhToan(Session session)
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