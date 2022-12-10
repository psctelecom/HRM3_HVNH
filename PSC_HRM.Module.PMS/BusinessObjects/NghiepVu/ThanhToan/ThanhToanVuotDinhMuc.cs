using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.ThanhToan
{
    [ModelDefault("Caption", "Thanh toán tiền vượt ĐM")]
    
    public class ThanhToanVuotDinhMuc : BaseObject
    {
        #region key
        private QuanLyThanhToanThuLao _QuanLyThanhToanThuLao;
        [Association("QuanLyThanhToanThuLao-ListThanhToanVuotDinhMuc")]
        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        public QuanLyThanhToanThuLao QuanLyThanhToanThuLao
        {
            get
            {
                return _QuanLyThanhToanThuLao;
            }
            set
            {
                SetPropertyValue("QuanLyThanhToanThuLao", ref _QuanLyThanhToanThuLao, value);
            }
        }
        #endregion

        private NhanVien _NhanVien;
        private ChucVu _ChucVu;
        private NgachLuong _MaSoNgachLuong;
        private BoPhan _DonVi;
        //
        private decimal _HeSo_ChucDanh;
        private decimal _GioNghiaVu;
        private decimal _GioChuanQuyDoiCQ;
        private decimal _GioChuanQuyDoiKhongCQ;
        private decimal _GioChuanQuyDoiCaoHoc;
        private decimal _TongGio_ChamBai;
        private decimal _TongGioQuyDoi;
        private decimal _TongGioQuyDoi_SauGiamTru;
        private decimal _TongTienThanhToan;
        private decimal _TienGioCaoHoc;
        private decimal _TienGioDH_CD;
        //
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Chức vụ")]
        [Browsable(false)]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set { SetPropertyValue("ChucVu", ref _ChucVu, value); }
        }
        [ModelDefault("Caption", "Ngạch lương")]
        [Browsable(false)]
        public NgachLuong MaSoNgachLuong
        {
            get { return _MaSoNgachLuong; }
            set { SetPropertyValue("MaSoNgachLuong", ref _MaSoNgachLuong, value); }
        }
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan DonVi
        {
            get { return _DonVi; }
            set { SetPropertyValue("DonVi", ref _DonVi, value); }
        }
        //
        [ModelDefault("Caption", "Hệ số chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }
        [ModelDefault("Caption", "Giờ chuẩn ĐM")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioNghiaVu
        {
            get { return _GioNghiaVu; }
            set { SetPropertyValue("GioNghiaVu", ref _GioNghiaVu, value); }
        }
        [ModelDefault("Caption", "Giờ chuẩn quy đổi hệ CQ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioChuanQuyDoiCQ
        {
            get { return _GioChuanQuyDoiCQ; }
            set { SetPropertyValue("GioChuanQuyDoiCQ", ref _GioChuanQuyDoiCQ, value); }
        }
        [ModelDefault("Caption", "Giờ chuẩn quy đổi hệ không CQ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioChuanQuyDoiKhongCQ
        {
            get { return _GioChuanQuyDoiKhongCQ; }
            set { SetPropertyValue("GioChuanQuyDoiKhongCQ", ref _GioChuanQuyDoiKhongCQ, value); }
        }
        [ModelDefault("Caption", "Giờ chuẩn quy đổi hệ cao học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioChuanQuyDoiCaoHoc
        {
            get { return _GioChuanQuyDoiCaoHoc; }
            set { SetPropertyValue("GioChuanQuyDoiCaoHoc", ref _GioChuanQuyDoiCaoHoc, value); }
        }
        [ModelDefault("Caption", "Giờ chấm bài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio_ChamBai
        {
            get { return _TongGio_ChamBai; }
            set { SetPropertyValue("TongGio_ChamBai", ref _TongGio_ChamBai, value); }
        }
        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }
        [ModelDefault("Caption", "Tổng giờ quy đổi (Sau giảm trừ)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi_SauGiamTru
        {
            get { return _TongGioQuyDoi_SauGiamTru; }
            set { SetPropertyValue("TongGioQuyDoi_SauGiamTru", ref _TongGioQuyDoi_SauGiamTru, value); }
        }
        [ModelDefault("Caption", "Tổng tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongTienThanhToan
        {
            get { return _TongTienThanhToan; }
            set { SetPropertyValue("TongTienThanhToan", ref _TongTienThanhToan, value); }
        }
        [ModelDefault("Caption", "Tiền giờ cao học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienGioCaoHoc
        {
            get { return _TienGioCaoHoc; }
            set { SetPropertyValue("TienGioCaoHoc", ref _TienGioCaoHoc, value); }
        }
        [ModelDefault("Caption", "Tiền giờ ĐH_CĐ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienGioDH_CD
        {
            get { return _TienGioDH_CD; }
            set { SetPropertyValue("TienGioDH_CD", ref _TienGioDH_CD, value); }
        }
        

        public ThanhToanVuotDinhMuc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
