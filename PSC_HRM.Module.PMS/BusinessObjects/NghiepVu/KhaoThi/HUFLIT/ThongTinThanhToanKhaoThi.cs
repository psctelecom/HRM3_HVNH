using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    //[Appearance("Hide_QuanLyThanhToanKhaoThi_LoaiGV", TargetItems = "HocKy"
    //                                      , Visibility = ViewItemVisibility.Hide, Criteria = "LoaiGiangVien = 0")]

    [ModelDefault("Caption", "Thông tin thanh toán khảo thí")]
    public class ThongTinThanhToanKhaoThi : BaseObject
    {
        private QuanLyThanhToanKhaoThi _QuanLyThanhToanKhaoThi;
        private NhanVien _NhanVien;
        private decimal _ThanhTien;
        private decimal _ThueTNCN;
        private decimal _ThucLanh;
        #region HUFLIT
        private decimal _TienThucLanhTL;
        private decimal _TienThucLanhVD;
        private decimal _TienThucLanhChamThi;
        #endregion
        [ModelDefault("Caption", "Quản lý thanh toán khảo thí")]
        [Association("QuanLyThanhToanKhaoThi-ListThongTinThanhToanKhaoThi")]
        [ImmediatePostData]
        [Browsable(false)]
        public QuanLyThanhToanKhaoThi QuanLyThanhToanKhaoThi
        {
            get { return _QuanLyThanhToanKhaoThi; }
            set { SetPropertyValue("QuanLyThanhToanKhaoThi", ref _QuanLyThanhToanKhaoThi, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThueTNCN
        {
            get { return _ThueTNCN; }
            set { SetPropertyValue("ThueTNCN", ref _ThueTNCN, value); }
        }

        [ModelDefault("Caption", "Thực lãnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThucLanh
        {
            get { return _ThucLanh; }
            set { SetPropertyValue("ThucLanh", ref _ThucLanh, value); }
        }

        [ModelDefault("Caption", "Tiền thực lãnh tự luận")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TienThucLanhTL
        {
            get { return _TienThucLanhTL; }
            set { SetPropertyValue("TienThucLanhTL", ref _TienThucLanhTL, value); }
        }

        [ModelDefault("Caption", "Tiền thực lãnh vấn đáp")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TienThucLanhVD
        {
            get { return _TienThucLanhVD; }
            set { SetPropertyValue("TienThucLanhVD", ref _TienThucLanhVD, value); }
        }


        [ModelDefault("Caption", "Tiền thực lãnh chấm thi")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TienThucLanhChamThi
        {
            get { return _TienThucLanhChamThi; }
            set { SetPropertyValue("TienThucLanhChamThi", ref _TienThucLanhChamThi, value); }
        }


        public ThongTinThanhToanKhaoThi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
