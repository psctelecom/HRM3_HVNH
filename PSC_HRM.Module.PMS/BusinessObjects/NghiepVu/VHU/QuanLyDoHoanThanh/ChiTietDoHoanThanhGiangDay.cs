using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang
{
    [ModelDefault("Caption", "Chi tiết độ hoàng thành giảng dạy")]
    [Appearance("ChiTietDoHoanThanhGiangDay_Khoa", TargetItems = "*", Enabled = false, Criteria = "QuanLyDoHoanThanhGiangDay is not null")]
    public class ChiTietDoHoanThanhGiangDay : BaseObject
    {
        #region key
        private QuanLyDoHoanThanhGiangDay _QuanLyDoHoanThanhGiangDay;
        [Association("QuanLyDoHoanThanhGiangDay-ListChiTietDoHoanThanhGiangDay")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public QuanLyDoHoanThanhGiangDay QuanLyDoHoanThanhGiangDay
        {
            get
            {
                return _QuanLyDoHoanThanhGiangDay;
            }
            set
            {
                SetPropertyValue("QuanLyDoHoanThanhGiangDay", ref _QuanLyDoHoanThanhGiangDay, value);             
            }
        }
        #endregion
        private NhanVien _NhanVien;
        private decimal _GioThucHien;
        private decimal _DinhMucHocKy;
        private decimal _TyLeHoanThanh;

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Giờ thực hiện")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioThucHien
        {
            get
            {
                return _GioThucHien;
            }
            set
            {
                SetPropertyValue("GioThucHien", ref _GioThucHien, value);
            }
        }

        [ModelDefault("Caption", "Định mức thực hiện")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMucHocKy
        {
            get
            {
                return _DinhMucHocKy;
            }
            set
            {
                SetPropertyValue("DinhMucHocKy", ref _DinhMucHocKy, value);
            }
        }

        [ModelDefault("Caption", "Tỷ lệ hoàn thành")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TyLeHoanThanh
        {
            get
            {
                return _TyLeHoanThanh;
            }
            set
            {
                SetPropertyValue("TyLeHoanThanh", ref _TyLeHoanThanh, value);
            }
        }

        public ChiTietDoHoanThanhGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}