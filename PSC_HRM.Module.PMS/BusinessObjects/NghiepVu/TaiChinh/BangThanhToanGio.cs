using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Bảng thanh toán giờ")]
    [DefaultProperty("Caption")]
    public class BangThanhToanGio : BaseObject
    {
        #region key
        private QuanlyThanhToan _QuanlyThanhToan;
         [Association("QuanlyThanhToan-ListBangThanhToanGio")]
        [ModelDefault("Caption", "Quản lý thanh toán")]
        [Browsable(false)]
        public QuanlyThanhToan QuanlyThanhToan
        {
            get
            {
                return _QuanlyThanhToan;
            }
            set
            {
                SetPropertyValue("QuanlyThanhToan", ref _QuanlyThanhToan, value);
            }
        }
        #endregion
        #region KhaiBao
        private NhanVien _NhanVien;
        private decimal _HSLuong;
        private decimal _HSChucVu;
        private decimal _HSThamNienVK;
        private decimal _HSThamNienNghe_PhanTram;
        private decimal _HSThamNienNghe_So;
        private decimal _PhanTramPhuCap;
        private decimal _TongLuong;
        private decimal _SoTietDinhMuc;
        private decimal _TyLe;
        private decimal _SoTietMotGio_PhuTroi;
        private decimal _SoTietMotGio_HopDong;
        private decimal _SoTietPhuTroi;
        private decimal _SoTietHopDong;
        private decimal _ThanhTien_PhuTroi;
        private decimal _ThanhTien_HopDong;
        private string _GhiChu;
        private decimal _Thue;
        private decimal _TamUng;
        #endregion

        [ModelDefault("Caption", "Giảng viên")]
        [ModelDefault("AllowEdit", "False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);             
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSLuong
        {
            get { return _HSLuong; }
            set { SetPropertyValue("HSLuong", ref _HSLuong, value); }
        }

        [ModelDefault("Caption", "Hệ số chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSChucVu
        {
            get { return _HSChucVu; }
            set { SetPropertyValue("HSChucVu", ref _HSChucVu, value); }
        }

        [ModelDefault("Caption", "Hệ số thâm niên VK")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSThamNienVK
        {
            get { return _HSThamNienVK; }
            set { SetPropertyValue("HSThamNienVK", ref _HSThamNienVK, value); }
        }

        [ModelDefault("Caption", "Hệ số thâm nghề(%)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSThamNienNghe_PhanTram
        {
            get { return _HSThamNienNghe_PhanTram; }
            set { SetPropertyValue("HSThamNienNghe_PhanTram", ref _HSThamNienNghe_PhanTram, value); }
        }

        [ModelDefault("Caption", "Hệ số thâm nghề(số)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSThamNienNghe_So
        {
            get { return _HSThamNienNghe_So; }
            set { SetPropertyValue("HSThamNienNghe_So", ref _HSThamNienNghe_So, value); }
        }

        [ModelDefault("Caption", "Phân trăm phụ cấp")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhanTramPhuCap
        {
            get { return _PhanTramPhuCap; }
            set { SetPropertyValue("PhanTramPhuCap", ref _PhanTramPhuCap, value); }
        }

        [ModelDefault("Caption", "Tổng lương")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongLuong
        {
            get { return _TongLuong; }
            set { SetPropertyValue("TongLuong", ref _TongLuong, value); }
        }

        [ModelDefault("Caption", "Số tiết định mức")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietDinhMuc
        {
            get { return _SoTietDinhMuc; }
            set { SetPropertyValue("SoTietDinhMuc", ref _SoTietDinhMuc, value); }
        }

        [ModelDefault("Caption", "Tỷ lệ (%)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TyLe
        {
            get { return _TyLe; }
            set { SetPropertyValue("TyLe", ref _TyLe, value); }
        }

        [ModelDefault("Caption", "Số tiền 1 giờ(phụ trội)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietMotGio_PhuTroi
        {
            get { return _SoTietMotGio_PhuTroi; }
            set { SetPropertyValue("SoTietMotGio_PhuTroi", ref _SoTietMotGio_PhuTroi, value); }
        }

        [ModelDefault("Caption", "Số tiền 1 giờ(hợp đồng)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietMotGio_HopDong
        {
            get { return _SoTietMotGio_HopDong; }
            set { SetPropertyValue("SoTietMotGio_HopDong", ref _SoTietMotGio_HopDong, value); }
        }

        [ModelDefault("Caption", "Số tiết phụ trội")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietPhuTroi
        {
            get { return _SoTietPhuTroi; }
            set { SetPropertyValue("SoTietPhuTroi", ref _SoTietPhuTroi, value); }
        }

        [ModelDefault("Caption", "Số tiết hợp đồng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietHopDong
        {
            get { return _SoTietHopDong; }
            set { SetPropertyValue("SoTietHopDong", ref _SoTietHopDong, value); }
        }

        [ModelDefault("Caption", "Thành tiền phụ trội")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThanhTien_PhuTroi
        {
            get { return _ThanhTien_PhuTroi; }
            set { SetPropertyValue("ThanhTien_PhuTroi", ref _ThanhTien_PhuTroi, value); }
        }


        [ModelDefault("Caption", "Thành tiền hợp đồng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThanhTien_HopDong
        {
            get { return _ThanhTien_HopDong; }
            set { SetPropertyValue("ThanhTien_HopDong", ref _ThanhTien_HopDong, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Thue
        {
            get { return _Thue; }
            set { SetPropertyValue("Thue", ref _Thue, value); }
        }

        [ModelDefault("Caption", "Tam ứng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TamUng
        {
            get { return _TamUng; }
            set { SetPropertyValue("TamUng", ref _TamUng, value); }
        }
        public BangThanhToanGio(Session session) : base(session) { }
    }
}
