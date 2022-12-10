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


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Chi tiết thanh toán bổ sung")]
    [DefaultProperty("Caption")]
    
    public class ChiTietThanhToanBoSung : BaseObject
    {
        #region key
        private QuanLyThanhToanBoSung _QuanLyThanhToanBoSung;
        [Association("QuanLyThanhToanBoSung-listChiTiet")]
        [ModelDefault("Caption", "Bảng thù lao")]
        [Browsable(false)]
        public QuanLyThanhToanBoSung QuanLyThanhToanBoSung
        {
            get
            {
                return _QuanLyThanhToanBoSung;
            }
            set
            {
                SetPropertyValue("QuanLyThanhToanBoSung", ref _QuanLyThanhToanBoSung, value);
            }
        }
        #endregion
        private BoPhan _BoPhan;
        private string _MaQuanLy;
        private NhanVien _NhanVien;
        private BacDaoTao _BacDaoTao;
        private LoaiHoatDongEnum _LoaiHoatDong;

        private string _LopHocPhan;
        private string _TenMonHoc;
        private string _TenHoatDong;
        private decimal _TongGioA1;
        private decimal _TongGioA2;
        private decimal _TongGio;
        private decimal _SoTienThanhToan;
        private CongTruPMSEnum _CongTru = 0;
        private string _GhiChu;

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [NonPersistent]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Giảng viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading)
                    if (NhanVien != null)
                        BoPhan = NhanVien.BoPhan;
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Loại hoạt động")]
        public LoaiHoatDongEnum LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]        
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        } 

        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Hoạt động")]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit","false")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        [ModelDefault("Caption", "Tổng giờ A1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA1
        {
            get { return _TongGioA1; }
            set { SetPropertyValue("TongGioA1", ref _TongGioA1, value); }
        }

        [ModelDefault("Caption", "Tổng giờ A2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA2
        {
            get { return _TongGioA2; }
            set { SetPropertyValue("TongGioA2", ref _TongGioA2, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }


        [ModelDefault("Caption", "Cộng trừ")]
        public CongTruPMSEnum CongTru
        {
            get { return _CongTru; }
            set { SetPropertyValue("CongTru", ref _CongTru, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        public ChiTietThanhToanBoSung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TongGioA1 = 0;
            TongGioA2 = 0;
        }
       void TinhGio()
        {
            TongGio = TongGioA1 + TongGioA2;
        }
    }
}
