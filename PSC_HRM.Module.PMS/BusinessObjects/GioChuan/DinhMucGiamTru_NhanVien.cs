using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.CauHinh;

namespace PSC_HRM.Module.PMS.GioChuan
{
    [ImageName("BO_ChuyenNgach")]
    
    public class DinhMucGiamTru_NhanVien : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;

        private NhanVien _NhanVien;
        private decimal _GiamTruChucVu;
        private decimal _GiamTruTamNghi;
        private decimal _GiamTruNCKH;
        private decimal _GiamTruHDQL;
        private string _GhiChu;

        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucGiamTruNhanVien")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Giảm trừ chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal GiamTruChucVu
        {
            get { return _GiamTruChucVu; }
            set
            {
                SetPropertyValue("GiamTruChucVu", ref _GiamTruChucVu, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ tạm nghĩ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal GiamTruTamNghi
        {
            get { return _GiamTruTamNghi; }
            set
            {
                SetPropertyValue("GiamTruTamNghi", ref _GiamTruTamNghi, value);
            }
        }
        [ModelDefault("Caption", "Định mức giảm NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GiamTruNCKH
        {
            get { return _GiamTruNCKH; }
            set { SetPropertyValue("GiamTruNCKH", ref _GiamTruNCKH, value); }
        }

        [ModelDefault("Caption", "Định mức giảm HDQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GiamTruHDQL
        {
            get { return _GiamTruHDQL; }
            set { SetPropertyValue("GiamTruHDQL", ref _GiamTruHDQL, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        //[Browsable(false)]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public DinhMucGiamTru_NhanVien(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}