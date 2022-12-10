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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.NCKH
{

    [ModelDefault("Caption", "Chi tiết NCKH")]
    public class ChiTietNCKH : BaseObject
    {
        #region key
        private QuanLyNCKH _QuanLyNCKH;
        [Association("QuanLyNCKH-ListChiTietNCKH")]
        [ModelDefault("Caption", "Quản lý nghiên cứu khoa học")]
        [Browsable(false)]
        public QuanLyNCKH QuanLyNCKH
        {
            get
            {
                return _QuanLyNCKH;
            }
            set
            {
                SetPropertyValue("QuanLyNCKH", ref _QuanLyNCKH, value);
            }
        }
        #endregion

        private NhanVien _NhanVien;
        private DanhSachChiTietHDKhac _DanhMucNCKH;
        private decimal _SoTiet;
        private DateTime _NgayNhap;
        private decimal _GioQuyDoiNCKH;
        private int _SoLuongTV;
        private VaiTroNCKH _VaiTro;
        private bool _DuKien;
        private bool _XacNhan;
        private DateTime _NgayXacNhan;
        private DateTime _NgayCapNhat;
        private string _NguoiCapNhat;
        private decimal _DinhMuc;
        private string _TenNCKH;
        private string _GhiChu;
        private bool _TuChoi;
        private bool _DaThanhToan;



        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Tên hoạt động")]
        public DanhSachChiTietHDKhac DanhMucNCKH
        {
            get { return _DanhMucNCKH; }
            set { SetPropertyValue("DanhMucNCKH", ref _DanhMucNCKH, value); }
        }

        [ModelDefault("Caption", "Tên NCKH")]
        public string TenNCKH
        {
            get { return _TenNCKH; }
            set { SetPropertyValue("TenNCKH", ref _TenNCKH, value); }
        }

        [ModelDefault("Caption", "Số giờ hành chính")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal SoTiet
        {
            get { return _SoTiet; }
            set
            {
                SetPropertyValue("SoTiet", ref _SoTiet, value);
                if (!IsLoading)
                {
                    LoadGioQuyDoi();
                }
            }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal GioQuyDoiNCKH
        {
            get { return _GioQuyDoiNCKH; }
            set { SetPropertyValue("GioQuyDoiNCKH", ref _GioQuyDoiNCKH, value); }
        }

        [ModelDefault("Caption", "Dịnh mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMuc
        {
            get { return _DinhMuc; }
            set { SetPropertyValue("DinhMuc", ref _DinhMuc, value); }
        }

        [ModelDefault("Caption", "Số lượng TV")]
        [ImmediatePostData]
        public int SoLuongTV
        {
            get { return _SoLuongTV; }
            set
            {
                SetPropertyValue("SoLuongTV", ref _SoLuongTV, value);
                if (!IsLoading)
                {
                    LoadGioQuyDoi();
                }
            }
        }

        [ModelDefault("Caption", "Vai trò")]
        [ImmediatePostData]
        public VaiTroNCKH VaiTro
        {
            get { return _VaiTro; }
            set
            {
                SetPropertyValue("VaiTro", ref _VaiTro, value);
                if(!IsLoading)
                {
                    LoadGioQuyDoi();
                }
            }
        }


        [ModelDefault("Caption", "Dự kiến")]
        public bool DuKien
        {
            get { return _DuKien; }
            set { SetPropertyValue("DuKien", ref _DuKien, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }

        [ModelDefault("Caption", "Ngày nhập")]
        [Browsable(false)]
        public DateTime NgayNhap
        {
            get { return _NgayNhap; }
            set { SetPropertyValue("NgayNhap", ref _NgayNhap, value); }
        }

        [ModelDefault("Caption", "Ngày xác nhận")]
        [Browsable(false)]
        public DateTime NgayXacNhan
        {
            get { return _NgayXacNhan; }
            set { SetPropertyValue("NgayXacNhan", ref _NgayXacNhan, value); }
        }

        [ModelDefault("Caption", "Ngày cập nhật")]
        [Browsable(false)]
        public DateTime NgayCapNhat
        {
            get { return _NgayCapNhat; }
            set { SetPropertyValue("NgayCapNhat", ref _NgayCapNhat, value); }
        }

        [ModelDefault("Caption", "Người cập nhật")]
        [Browsable(false)]
        public string NguoiCapNhat
        {
            get { return _NguoiCapNhat; }
            set { SetPropertyValue("NguoiCapNhat", ref _NguoiCapNhat, value); }
        }

        [ModelDefault("Caption", "Từ chối")]
        public bool TuChoi
        {
            get { return _TuChoi; }
            set { SetPropertyValue("TuChoi", ref _TuChoi, value); }
        }

        [ModelDefault("Caption", "Đã thanh toán")]
        public bool DaThanhToan
        {
            get { return _DaThanhToan; }
            set { SetPropertyValue("DaThanhToan", ref _DaThanhToan, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        public ChiTietNCKH(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public void LoadGioQuyDoi()
        {
            if (VaiTro != null && SoLuongTV != 0 && SoTiet != 0)
            {
                if (VaiTro.Chia)
                {
                    GioQuyDoiNCKH = (SoTiet * (VaiTro.MucHuong / 100)) / SoLuongTV;
                }
                else
                {
                    GioQuyDoiNCKH = SoTiet * (VaiTro.MucHuong / 100) + (SoTiet * ((100 - VaiTro.MucHuong) / 100)) / SoLuongTV;
                }
            }
            else
            {
                GioQuyDoiNCKH = SoTiet;
            }
        }
    }

}
