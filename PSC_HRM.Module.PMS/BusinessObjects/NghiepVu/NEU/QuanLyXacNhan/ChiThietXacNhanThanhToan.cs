using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.PMS.NghiepVu.NEU.QuanLyXacNhan
{
    [ModelDefault("Caption","Chi tiết xác nhận thanh toán")]
    public class ChiThietXacNhanThanhToan : BaseObject
    {
        private QuanLyXacNhanThanhToan _QuanLyXacNhanThanhToan;
        [ModelDefault("Caption", "Quản lý xác nhận thanh toán")]
        [Association("QuanLyXacNhanThanhToan-ListChiTiet")]
        [Browsable(false)]
        public QuanLyXacNhanThanhToan QuanLyXacNhanThanhToan
        {
            get { return _QuanLyXacNhanThanhToan; }
            set { SetPropertyValue("QuanLyXacNhanThanhToan", ref _QuanLyXacNhanThanhToan, value); }
        }

        private NhanVien _NhanVien;
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { _NhanVien = value; }
        }

        private bool _XacNhan;
        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { _XacNhan = value; }
        }

        private string _YKien;
        [ModelDefault("Caption", "Ý kiến")]
        [Size(-1)]
        public string YKien
        {
            get { return _YKien; }
            set { _YKien = value; }
        }
        public ChiThietXacNhanThanhToan(Session session)
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