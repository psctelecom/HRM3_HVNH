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

    [ModelDefault("Caption", "Chi tiết NCKH(Non)")]
    [NonPersistent]
    public class ChiTietNCKH_Non : BaseObject
    {
        private bool _Chon;
        private Guid _OidKey;
        private NhanVien _NhanVien;
        private DanhSachChiTietHDKhac _DanhMucNCKH;
        private string _TenDeTai;
        private decimal _SoTiet;
        private DateTime _NgayNhap;
        private decimal _GioQuyDoiNCKH;
        private int _SoLuongTV;
        private VaiTroNCKH _VaiTro;
        private bool _DaThanhToan;
        private bool _XacNhan;
        private string _GhiChu;
        private bool _TuChoi;


        [ModelDefault("Caption", "Chọn")]
        [ImmediatePostData]
        public bool Chon
        {
            get { return _Chon; }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Key")]
        [Browsable(false)]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public Guid OidKey
        {
            get { return _OidKey; }
            set { SetPropertyValue("OidKey", ref _OidKey, value); }
        }

        [ModelDefault("Caption","Giảng viên")]
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

        [ModelDefault("Caption", "Tên đề tài")]
        public string TenDeTai
        {
            get { return _TenDeTai; }
            set { SetPropertyValue("TenDeTai", ref _TenDeTai, value); }
        }

        [ModelDefault("Caption", "Số giờ hành chính")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet
        {
            get { return _SoTiet; }
            set { SetPropertyValue("SoTiet", ref _SoTiet, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiNCKH
        {
            get { return _GioQuyDoiNCKH; }
            set { SetPropertyValue("GioQuyDoiNCKH", ref _GioQuyDoiNCKH, value); }
        }

        [ModelDefault("Caption", "Số lượng TV")]
        public int SoLuongTV
        {
            get { return _SoLuongTV; }
            set { SetPropertyValue("SoLuongTV", ref _SoLuongTV, value); }
        }

        [ModelDefault("Caption", "Vai trò")]
        public VaiTroNCKH VaiTro
        {
            get { return _VaiTro; }
            set { SetPropertyValue("VaiTro", ref _VaiTro, value); }
        }


        [ModelDefault("Caption", "Đã thanh toán")]
        public bool DaThanhToan
        {
            get { return _DaThanhToan; }
            set { SetPropertyValue("DaThanhToan", ref _DaThanhToan, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }

        [ModelDefault("Caption", "Ngày nhập")]
        public DateTime NgayNhap
        {
            get { return _NgayNhap; }
            set { SetPropertyValue("NgayNhap", ref _NgayNhap, value); }
        }

        [ModelDefault("Caption", "Từ chối")]
        public bool TuChoi
        {
            get { return _TuChoi; }
            set { SetPropertyValue("TuChoi", ref _TuChoi, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        public ChiTietNCKH_Non(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
