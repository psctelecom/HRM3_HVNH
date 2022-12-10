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
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ImageName("BO_ChuyenNgach")]
    [ModelDefault("Caption", "Chi tiết bảo lưu")]
    public class ChiTietBaoLuu : BaseObject
    {
        private QuanLyBaoLuu _QuanLyBaoLuu;

        private NhanVien _NhanVien;
        private decimal _SoGioBaoLuuGiangDay;
        private decimal _SoGioBaoLuuNCKH;
        private decimal _SoGioBaoLuuHDQL;
        private string _GhiChu;


        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyBaoLuu-ListChiTietBaoLuu")]
        [Browsable(false)]
        public QuanLyBaoLuu QuanLyBaoLuu
        {
            get
            {
                return _QuanLyBaoLuu;
            }
            set
            {
                SetPropertyValue("QuanLyBaoLuu", ref _QuanLyBaoLuu, value);
            }
        }
        [ModelDefault("Caption", "Cán bộ")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "Bảo lưu giảng dạy")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioBaoLuuGiangDay
        {
            get { return _SoGioBaoLuuGiangDay; }
            set { SetPropertyValue("SoGioBaoLuuGiangDay", ref _SoGioBaoLuuGiangDay, value); }
        }
        [ModelDefault("Caption", "Bảo lưu NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioBaoLuuNCKH
        {
            get { return _SoGioBaoLuuNCKH; }
            set { SetPropertyValue("SoGioBaoLuuNCKH", ref _SoGioBaoLuuNCKH, value); }
        }
        [ModelDefault("Caption", "Bảo lưu TGQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioBaoLuuHDQL
        {
            get { return _SoGioBaoLuuHDQL; }
            set { SetPropertyValue("SoGioBaoLuuHDQL", ref _SoGioBaoLuuHDQL, value); }
        }
      
        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        public ChiTietBaoLuu(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();           
        }
    }
}